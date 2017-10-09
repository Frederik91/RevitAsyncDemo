using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Query;
using Contracts.RevitInteractors;

namespace API.Models.Documents
{
    public class DocumentQueryHandler : IQueryHandler<DocumentQuery, CW_Document>
    {
        private readonly IDocumentRevitInteractor m_documentRevitInteractor;

        public DocumentQueryHandler(IDocumentRevitInteractor documentRevitInteractor)
        {
            m_documentRevitInteractor = documentRevitInteractor;
        }

        async Task<CW_Document> IQueryHandler<DocumentQuery, CW_Document>.HandleAsync(DocumentQuery query)
        {
            var result = await m_documentRevitInteractor.Get(query.Title);
            return result;
        }
    }
}
