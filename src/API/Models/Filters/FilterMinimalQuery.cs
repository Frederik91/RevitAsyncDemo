using System.Collections.Generic;
using Contracts.Query;
using Contracts.Filtering;
using Contracts.MinimalModels;
using Contracts.Enums;

namespace API.Models.Filters
{
    public class FilterMinimalQuery : IQuery<IEnumerable<CW_ElementMinimal>>
    {
        public IEnumerable<string> DocumentTitles { get; set; }
        public IEnumerable<Filter> Filters { get; set; }
        public IEnumerable<string> CategoryNames { get; set; }
        public FilterType FilterType { get; internal set; }
    }
}