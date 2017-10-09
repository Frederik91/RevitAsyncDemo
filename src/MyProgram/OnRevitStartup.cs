using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;

namespace MyRevitAddin
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class OnRevitStartup : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            application.Idling += Application_Idling;
            return Result.Succeeded;
        }

        private void Application_Idling(object sender, IdlingEventArgs e)
        {            
            throw new NotImplementedException();
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }
    }
}
