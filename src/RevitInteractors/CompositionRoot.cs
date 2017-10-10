using Contracts.RevitInteractors;
using LightInject;

namespace RevitInteractors
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDocumentRevitInteractor, DocumentRevitInteractor>(new PerScopeLifetime());
            serviceRegistry.RegisterFrom<API.CompositionRoot>();
        }
    }
}
