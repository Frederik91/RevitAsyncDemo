using Contracts.RevitInteractors;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitInteractors
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDocumentRevitInteractor, DocumentRevitInteractor>(new PerScopeLifetime());
        }
    }
}
