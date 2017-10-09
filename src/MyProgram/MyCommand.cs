using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Contracts;
using LightInject;
using API.Controllers;
using MyRevitAddinCommand;

namespace MyProgram
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class MyCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ExternalCommandDataHolder.ExternalCommand = commandData;

            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterFrom<CompositionRoot>();

            var controller = serviceContainer.GetInstance<DocumentController>();

            var window = new MainWindow(controller);
            window.Show();

            return Result.Succeeded;
        }
    }
}
