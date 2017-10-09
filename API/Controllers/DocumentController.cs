using API.Models.Documents;
using Contracts.Models;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class DocumentController
    {
        private readonly IDocumentService m_documentService;

        public DocumentController(IDocumentService documentService)
        {
            m_documentService = documentService;
        }

        public async Task<CW_Document> Get(string documentTitle)
        {
            var result = await m_documentService.Get(documentTitle);
            return result;
        }
    }
}
