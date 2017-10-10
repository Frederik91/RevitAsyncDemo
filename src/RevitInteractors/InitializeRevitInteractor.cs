using API;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using LightInject;
using Autodesk.Revit.ApplicationServices;
using System.Threading.Tasks;
using System;

namespace RevitInteractors
{
    public class InitializeRevitInteractor
    {
        public static IController Register(UIControlledApplication application)
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterFrom<CompositionRoot>();

            application.Idling += Application_Idling;

            return serviceContainer.GetInstance<IController>();
        }

     public static void Unsubscribe(UIControlledApplication application)
        {
            application.Idling -= Application_Idling;
        }

        public static string ActiveDocumentTitle { get; set; }

        public static bool taskRunning = false;
        public static async Task<Application> GetApplicationAsync()
        {
            while (taskRunning || application == null)
            {
                await Task.Delay(50);
            }
            taskRunning = true;            
            return application;
        }

        public static void ReleaseApplication()
        {
            taskRunning = false;
        }

        private static Application application;
        private static void Application_Idling(object sender, IdlingEventArgs e)
        {
            application = sender as Application;
        }
    }
}
