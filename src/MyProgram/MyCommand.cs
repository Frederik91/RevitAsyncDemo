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
        private static MainWindow Window { get; set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Window = new MainWindow(Globals.Controller);
            WindowInteropHelper wih = new WindowInteropHelper(Window);
            wih.Owner = Autodesk.Windows.ComponentManager.ApplicationWindow;

            
            Window.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
                       

            Window.Show();

            return Result.Succeeded;
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Window?.Close();
            TaskDialog.Show("MyTestAddin crash", e.Exception.Message);
            e.Handled = true;
        }
    }
}
