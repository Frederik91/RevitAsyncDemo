using System.Collections.Generic;
using Contracts.Query;
using Contracts.Models;
using Contracts.Command;
using Contracts.Requests;
using System.Threading.Tasks;

namespace API.Models.Elements
{
    public class ElementService : IElementService
    {
        private readonly IQueryExecutor m_queryExecutor;
        private readonly ICommandExecutor m_commandExecutor;

        public ElementService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }

        public async Task<IEnumerable<CW_Element>> Get(string documentTitle, string uniqueId)
        {
            var cwElement = await m_queryExecutor.HandleAsync(new ElementQuery { UniqueId = uniqueId, DocumentTitle = documentTitle });
            return cwElement;
        }

        public async Task Delete(IEnumerable<DeleteElementRequest> requests)
        {
            await m_commandExecutor.Execute(new DeleteElementCommand { Requests = requests });
        }
    }
}
