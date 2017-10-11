using System.Collections.Generic;
using Contracts.Command;
using Contracts.Query;
using Contracts.Models;
using Contracts.Enums;
using System.Threading.Tasks;

namespace API.Models.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IQueryExecutor m_queryExecutor;
        private readonly ICommandExecutor m_commandExecutor;

        public CategoryService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }

        public async Task<IEnumerable<CW_Category>> Get(IEnumerable<string> documentTitles, string idOrName, bool mustContainElements, IEnumerable<CW_CategoryType> categoryTypes)
        {
            var result = await m_queryExecutor.HandleAsync(new CategoryQuery { DocumentTitles = documentTitles, IdOrName = idOrName, MustContainElements = mustContainElements, categoryTypes = categoryTypes });
            return result;
        }
    }
}
