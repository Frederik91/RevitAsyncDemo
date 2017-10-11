using Contracts.Query;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Command;

namespace Contracts
{
    public class ExternalCommandDataHolder
    {
        public static List<QueryRequest> QueryRequests { get; set; }
        public static List<QueryResponse> Responses { get; set; }
        public static List<CommandRequest> CommandRequests { get; set; }
    }
}
