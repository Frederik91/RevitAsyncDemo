using API.Models.Documents;
using API.Models.Filters;
using Contracts.Command;
using Contracts.Logging;
using Contracts.Models;
using Contracts.MinimalModels;
using Contracts.Query;
using LightInject;
using System.Collections.Generic;
using API.Models.Categories;
using API.Models.Elements;
using API.Models.Parameters;
using CW_Revit.Models.Parameters;

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

            // CW_Category
            serviceRegistry.Register<ICategoryService, CategoryService>(new PerContainerLifetime());
            serviceRegistry.Register<IQueryHandler<CategoryQuery, IEnumerable<CW_Category>>, CategoryQueryHandler>(new PerContainerLifetime());

            // Controller
            serviceRegistry.Register<IController, Controller>();

            // CW_Documents
            serviceRegistry.Register<IDocumentService, DocumentService>(new PerContainerLifetime());
            serviceRegistry.Register<IQueryHandler<DocumentQuery, CW_Document>, DocumentQueryHandler>(new PerContainerLifetime());

            // CW_Elements
            serviceRegistry.Register<IElementService, ElementService>(new PerContainerLifetime());
            serviceRegistry.Register<IQueryHandler<ElementQuery, IEnumerable<CW_Element>>, ElementQueryHandler>(new PerContainerLifetime());
            serviceRegistry.Register<ICommandHandler<DeleteElementCommand>, DeleteElementCommandHandler>(new PerContainerLifetime());

            // Filtering
            serviceRegistry.Register<IFilterService, FilterService>(new PerContainerLifetime());
            serviceRegistry.Register<IQueryHandler<FilterMinimalQuery, IEnumerable<CW_ElementMinimal>>, FilterMinimalQueryHandler>(new PerContainerLifetime());

            // Parameters
            serviceRegistry.Register<IParameterService, ParameterService>(new PerContainerLifetime());
            serviceRegistry.Register<ICommandHandler<ParameterCommand>, ParameterCommandHandler>(new PerContainerLifetime());
        }
    }
}
