using API;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Contracts.Query;
using Contracts.Command;
using System.IO;
using Autodesk.Revit.DB;

namespace RevitInteractors
{
    public class RevitInteractor
    {
        private static IController controller;

        public static IController Register(UIControlledApplication application)
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterFrom<CompositionRoot>();
            controller = serviceContainer.GetInstance<IController>();

            application.Idling += Application_Idling;
            application.ControlledApplication.DocumentChanged += ControlledApplication_DocumentChanged;

            ExternalCommandDataHolder.QueryRequests = new List<QueryRequest>();
            ExternalCommandDataHolder.Responses = new List<QueryResponse>();
            ExternalCommandDataHolder.CommandRequests = new List<CommandRequest>();
            
            return controller;
        }

        private static void ControlledApplication_DocumentChanged(object sender, Autodesk.Revit.DB.Events.DocumentChangedEventArgs e)
        {
            var document = e.GetDocument();
            var documentTitle = document.Title;
            var transactionNames = e.GetTransactionNames();
            if (document.IsWorkshared)
            {
                var path = ModelPathUtils.ConvertModelPathToUserVisiblePath(document.GetWorksharingCentralModelPath());
                documentTitle = Path.GetFileNameWithoutExtension(path);
            }
            var addedIds = e.GetAddedElementIds().Select(x => x.IntegerValue);
            var modifiedIds = e.GetModifiedElementIds().Select(x => x.IntegerValue);
            var deletedIds = e.GetDeletedElementIds().Select(x => x.IntegerValue);

            controller.DocumentChangedEvent.DocumentChanged(transactionNames, documentTitle, addedIds, modifiedIds, deletedIds);
        }

        public static void Unsubscribe(UIControlledApplication application)
        {
            application.Idling -= Application_Idling;
        }

        public static UIApplication UIApplication { get; set; }
        private static void Application_Idling(object sender, IdlingEventArgs e)
        {
            UIApplication = sender as UIApplication;

            if (ExternalCommandDataHolder.QueryRequests.Count > 0)
            {
                try
                {
                    var request = ExternalCommandDataHolder.QueryRequests.First();

                    var result = request.Handler.Handle((dynamic)request.Query);

                    var response = new QueryResponse(request.Id) { Result = result };
                    ExternalCommandDataHolder.Responses.Add(response);
                    ExternalCommandDataHolder.QueryRequests.Remove(request);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (ExternalCommandDataHolder.CommandRequests.Count > 0)
            {
                try
                {
                    var request = ExternalCommandDataHolder.CommandRequests.First();

                    UIApplication = sender as UIApplication;
                    request.Handler.Handle((dynamic)request.Command);
                    ExternalCommandDataHolder.CommandRequests.Remove(request);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            UIApplication = null;
        }
    }
}
