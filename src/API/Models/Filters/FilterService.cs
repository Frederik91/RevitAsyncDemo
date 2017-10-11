using System.Collections.Generic;
using Contracts.Filtering;
using Contracts.Query;
using Contracts.Command;
using Contracts.MinimalModels;
using Contracts.Enums;
using System.Threading.Tasks;

namespace API.Models.Filters
{
    public class FilterService : IFilterService
    {
        private readonly ICommandExecutor m_commandExecutor;
        private readonly IQueryExecutor m_queryExecutor;

        public FilterService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }

        public async Task<IEnumerable<CW_ElementMinimal>> GetMinimal(IEnumerable<string> documentTitles, IEnumerable<Filter> filters, IEnumerable<string> categoryNames, FilterType filterType)
        {
            var result = await m_queryExecutor.HandleAsync(new FilterMinimalQuery { DocumentTitles = documentTitles, Filters = filters, CategoryNames = categoryNames, FilterType = filterType });
            return result;
        }
    }
}
