using Contracts.Models;
using Contracts.Query;
using System.Threading.Tasks;

namespace API.Models.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly IQueryExecutor m_queryExecutor;

        public DocumentService(IQueryExecutor queryExecutor)
        {
            m_queryExecutor = queryExecutor;
        }

        public async Task<CW_Document> Get(string documentTitle)
        {
            var result = await m_queryExecutor.HandleAsync(new DocumentQuery { Title = documentTitle });
            return result;
        }
    }
}
