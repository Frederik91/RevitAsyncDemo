using Contracts.RevitInteractors;
using LightInject;
using RevitInteractors.Interactors;

namespace RevitInteractors
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ICategoryRevitInteractor, CategoryRevitInteractor>(new PerContainerLifetime());
            serviceRegistry.Register<IDocumentRevitInteractor, DocumentRevitInteractor>(new PerContainerLifetime());
            serviceRegistry.Register<IElementRevitInteractor, ElementRevitInteractor>(new PerContainerLifetime());
            serviceRegistry.Register<IFilterRevitInteractor, FilterRevitInteractor>(new PerContainerLifetime());
            serviceRegistry.Register<IParameterRevitInteractor, ParameterRevitInteractor>(new PerContainerLifetime());
            serviceRegistry.RegisterFrom<API.CompositionRoot>();
        }
    }
}
