using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Query
{
    public class QueryResponse
    {
        public Guid Id { get; }
        public dynamic Result { get; set; }

        public QueryResponse(Guid id)
        {
            Id = id;
        }
    }
}
