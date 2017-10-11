using Contracts.Enums;
using Contracts.Models;
using Contracts.Query;
using System.Collections.Generic;

namespace API.Models.Categories
{
    public class CategoryQuery : IQuery<IEnumerable<CW_Category>>
    {
        public IEnumerable<string> DocumentTitles { get; set; }
        public string IdOrName { get; set; }
        public bool MustContainElements { get; set; }
        public IEnumerable<CW_CategoryType> categoryTypes { get; set; }
    }
}
