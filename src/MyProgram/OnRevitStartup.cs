using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using MyProgram;
using MyRevitAddinCommand;
using RevitInteractors;
using System;
using System.Reflection;

namespace MyRevitAddin
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class OnRevitStartup : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            Globals.Controller = RevitInteractor.Register(application);
            #region Create panel
            // create electrical Ribbon panel
            var ribbon = application.CreateRibbonPanel("Revit Async Demo");
            ribbon.Title = "Revit Async Demo";
            ribbon.Visible = true;

            #region Create a split button for switchboard schematics

            //create push button for new schema
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var OpenAddinPush = new PushButtonData("Open window", "Open window", thisAssemblyPath, typeof(MyCommand).FullName);

            ribbon.AddItem(OpenAddinPush);
            #endregion

            #endregion

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            RevitInteractor.Unsubscribe(application);
            return Result.Succeeded;
        }
    }
}
