using Contracts.Query;
using Contracts.Models;
using System.Collections.Generic;

namespace API.Models.Elements
{
    public class ElementQuery : IQuery<IEnumerable<CW_Element>>
    {
        public string UniqueId { get; set; }
        public string DocumentTitle { get; set; }
    }
}
