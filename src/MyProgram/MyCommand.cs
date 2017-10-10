using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using MyRevitAddinCommand;
using System.Diagnostics;
using System;
using System.Windows;
using System.Windows.Interop;

namespace MyProgram
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class MyCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var window = new MainWindow(Globals.Controller);
            WindowInteropHelper wih = new WindowInteropHelper(window);
            wih.Owner = Autodesk.Windows.ComponentManager.ApplicationWindow;

            window.Show();

            return Result.Succeeded;
        }
    }
}
