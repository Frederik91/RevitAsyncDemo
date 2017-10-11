using Contracts.Enums;
using Contracts.Filtering;
using Contracts.MinimalModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Filters
{
    public interface IFilterService
    {
        Task<IEnumerable<CW_ElementMinimal>> GetMinimal(IEnumerable<string> documentTitles, IEnumerable<Filter> filters, IEnumerable<string> categoryNames, FilterType filterType);
    }
}
