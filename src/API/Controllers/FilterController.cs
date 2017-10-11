using System.Collections.Generic;
using API.Models.Filters;
using Contracts.MinimalModels;
using Contracts.Filtering;
using Contracts.Enums;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class FilterController
    {
        private readonly IFilterService m_filterService;

        public FilterController(IFilterService filterService)
        {
            m_filterService = filterService;
        }
        
        public async Task<IEnumerable<CW_ElementMinimal>> GetMinimal(IEnumerable<string> documentTitles, IEnumerable<Filter> filters, IEnumerable<string> categoryNames)
        {
            var result = await m_filterService.GetMinimal(documentTitles, filters, categoryNames, FilterType.Both);
            return result;
        }

        public async Task<IEnumerable<CW_ElementMinimal>> GetMinimal(IEnumerable<string> documentTitles, IEnumerable<Filter> filters, IEnumerable<string> categoryNames, FilterType filterType)
        {
            var result = await m_filterService.GetMinimal(documentTitles, filters, categoryNames, filterType);
            return result;
        }
    }
}
