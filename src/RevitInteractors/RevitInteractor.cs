using API;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using LightInject;
using Autodesk.Revit.ApplicationServices;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using System.Threading;
using Contracts.Query;

namespace RevitInteractors
{
    public class RevitInteractor
    {
        public static IController Register(UIControlledApplication application)
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterFrom<CompositionRoot>();

            application.Idling += Application_Idling;

            ExternalCommandDataHolder.Requests = new List<QueryRequest>();
            ExternalCommandDataHolder.Responses = new List<QueryResponse>();

            return serviceContainer.GetInstance<IController>();
        }

     public static void Unsubscribe(UIControlledApplication application)
        {
            application.Idling -= Application_Idling;
        }

        public static bool taskRunning = false;

        public static void ReleaseApplication()
        {
            taskRunning = false;
        }

        public static UIApplication UIApplication { get; set; }
        private static void Application_Idling(object sender, IdlingEventArgs e)
        {            
            if (!taskRunning && ExternalCommandDataHolder.Requests.Count > 0)
            {
                var request = ExternalCommandDataHolder.Requests.FirstOrDefault();

                UIApplication = sender as UIApplication;
                var result = request.Handler.Handle((dynamic)request.Query);

                var response = new QueryResponse(request.Id) { Result = result };
                ExternalCommandDataHolder.Responses.Add(response);
                ExternalCommandDataHolder.Requests.Remove(request);
            }
        }
    }
}
