using Contracts.Enums;
using Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CW_Category>> Get(IEnumerable<string> documentTitles, string idOrName, bool mustContainElements, IEnumerable<CW_CategoryType> categoryTypes);
    }
}
