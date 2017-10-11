using Contracts.RevitInteractors;
using Contracts.Models;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace RevitInteractors.Interactors
{
    public class DocumentRevitInteractor : IDocumentRevitInteractor
    {
        public CW_Document Get(string documentTitle)
        {
            // Interaction with Revit can only my made synchronously
            // Interaction with Revit can only be made through DocumentIdle event
            Document document = RevitInteractor.UIApplication.ActiveUIDocument.Document;
            var cwDocument = new CW_Document { Title = document.Title };

            return cwDocument;
        }
    }
}
