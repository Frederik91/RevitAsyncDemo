using API.Models.Documents;
using Contracts.Command;
using Contracts.Logging;
using Contracts.Models;
using Contracts.Query;
using LightInject;

namespace API
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            var logger = new Logger();
            serviceRegistry.Register<ILogger>(factory => logger);
            serviceRegistry.Register<IQueryExecutor>(factory => new QueryExecutor((IServiceFactory)serviceRegistry, logger));
            serviceRegistry.Register<ICommandExecutor>(factory => new CommandExecutor((IServiceFactory)serviceRegistry, logger));

            // CW_Documents
            serviceRegistry.Register<IDocumentService, DocumentService>(new PerContainerLifetime());
            serviceRegistry.Register<IQueryHandler<DocumentQuery, CW_Document>, DocumentQueryHandler>(new PerContainerLifetime());
        }
    }
}
