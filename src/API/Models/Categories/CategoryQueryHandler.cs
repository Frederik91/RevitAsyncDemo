using Contracts.Query;
using Contracts.Models;
using Contracts.RevitInteractors;
using System.Collections.Generic;

namespace API.Models.Categories
{
    public class CategoryQueryHandler : IQueryHandler<CategoryQuery, IEnumerable<CW_Category>>
    {
        private readonly ICategoryRevitInteractor m_categoryRevitInteractor;

        public CategoryQueryHandler(ICategoryRevitInteractor categoryRevitInteractor)
        {
            m_categoryRevitInteractor = categoryRevitInteractor;
        }

        public IEnumerable<CW_Category> Handle(CategoryQuery query)
        {
            var result = m_categoryRevitInteractor.Get(query.DocumentTitles, query.IdOrName, query.MustContainElements, query.categoryTypes);
            return result;
        }
    }
}
