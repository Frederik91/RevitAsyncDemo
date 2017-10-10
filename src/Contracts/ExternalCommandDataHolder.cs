using Contracts.Query;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public class ExternalCommandDataHolder
    {
        public static List<QueryRequest> Requests { get; set; }
        public static List<QueryResponse> Responses { get; set; }
    }
}
