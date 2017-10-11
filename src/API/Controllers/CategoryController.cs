using API.Models.Categories;
using Contracts.Enums;
using Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CategoryController
    {
        private readonly ICategoryService m_categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            m_categoryService = categoryService;
        }

        public async Task<IEnumerable<CW_Category>> Get(IEnumerable<string> documentTitles, IEnumerable<CW_CategoryType> categoryTypes, bool MustContainElements = false)
        {
            var result = await m_categoryService.Get(documentTitles, string.Empty, MustContainElements, categoryTypes);
            return result;
        }

        public async Task<IEnumerable<CW_Category>> Get(string documentTitle, IEnumerable<CW_CategoryType> categoryTypes, bool MustContainElements = false)
        {
            var result = await m_categoryService.Get(new List<string> { documentTitle }, string.Empty, MustContainElements, categoryTypes);
            return result;
        }

        public async Task<IEnumerable<CW_Category>> Get(string documentTitle, bool MustContainElements = false)
        {
            var result = await m_categoryService.Get(new List<string> { documentTitle }, string.Empty, MustContainElements, null);
            return result;
        }

        public async Task<IEnumerable<CW_Category>> Get(IEnumerable<string> documentTitles, bool MustContainElements = false)
        {
            var result = await m_categoryService.Get(documentTitles, string.Empty, MustContainElements, null);
            return result;
        }

        public async Task<CW_Category> Get(string documentTitle, string categoryName)
        {
            var result = await m_categoryService.Get(new List<string> { documentTitle }, categoryName, false, null);
            return result.FirstOrDefault();
        }

        public async Task<CW_Category> Get(IEnumerable<string> documentTitles, string categoryName)
        {
            var result = await m_categoryService.Get(documentTitles, categoryName, false, null);
            return result.FirstOrDefault();
        }

        public async Task<CW_Category> Get(string documentTitle, int id)
        {
            var result = await m_categoryService.Get(new List<string> { documentTitle }, id.ToString(), false, null);
            return result.FirstOrDefault();
        }

        public async Task<CW_Category> Get(IEnumerable<string> documentTitles, int id)
        {
            var result = await m_categoryService.Get(documentTitles, id.ToString(), false, null);
            return result.FirstOrDefault();
        }
    }
}
