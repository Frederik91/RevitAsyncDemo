﻿using API.Controllers;
using API.Models.Categories;
using API.Models.Documents;
using API.Models.Elements;
using API.Models.Filters;
using API.Models.Parameters;
using Contracts.Events;

namespace API
{
    public class Controller : IController
    {
        public CategoryController CategoryController { get; }
        public DocumentController DocumentController { get; }
        public ElementController ElementController { get; }
        public FilterController FilterController { get; }
        public ParameterController ParameterController { get; }
        public DocumentChangedEvent DocumentChangedEvent { get; }

        public Controller(ICategoryService categoryService, IDocumentService documentService, IElementService elementService, IFilterService filterService, IParameterService parameterService, DocumentChangedEvent documentChangedEvent)
        {
            CategoryController = new CategoryController(categoryService);
            DocumentController = new DocumentController(documentService);
            ElementController = new ElementController(elementService);
            FilterController = new FilterController(filterService);
            ParameterController = new ParameterController(parameterService);
            DocumentChangedEvent = documentChangedEvent;
        }
    }
}
