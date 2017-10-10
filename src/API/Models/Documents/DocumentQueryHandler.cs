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

        public CW_Document Handle(DocumentQuery query)
        {
            var result = m_documentRevitInteractor.Get(query.Title);
            return result;
        }
    }
}
