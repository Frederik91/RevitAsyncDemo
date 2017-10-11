using Contracts.Query;
using Contracts.RevitInteractors;
using System.Collections.Generic;
using Contracts.MinimalModels;

namespace API.Models.Filters
{
    public class FilterMinimalQueryHandler : IQueryHandler<FilterMinimalQuery, IEnumerable<CW_ElementMinimal>>
    {
        private readonly IFilterRevitInteractor m_parameterRevitInteractor;

        public FilterMinimalQueryHandler(IFilterRevitInteractor parameterRevitInteractor)
        {
            m_parameterRevitInteractor = parameterRevitInteractor;
        }

        public IEnumerable<CW_ElementMinimal> Handle(FilterMinimalQuery query)
        {
            var result = m_parameterRevitInteractor.GetMinimal(query.DocumentTitles, query.Filters, query.CategoryNames, query.FilterType);
            return result;
        }
    }
}