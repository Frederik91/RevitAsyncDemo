using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Query
{
    public class QueryRequest
    {
        public Guid Id { get; }
        public dynamic Query { get; set; }
        public dynamic Handler { get; set; }

        public QueryRequest()
        {
            Id = Guid.NewGuid();
        }
    }
}
