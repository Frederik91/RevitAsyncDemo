using API.Controllers;
using API.Models.Documents;

namespace API
{
    public class Controller : IController
    {
        public DocumentController DocumentController { get; }

        public Controller(IDocumentService documentService)
        {
            DocumentController = new DocumentController(documentService);
        }
    }
}
