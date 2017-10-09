using API.Controllers;
using LightInject;

namespace MyProgram
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<DocumentController>();
            serviceRegistry.RegisterFrom<API.CompositionRoot>();
            serviceRegistry.RegisterFrom<RevitInteractors.CompositionRoot>();
        }
    }
}
