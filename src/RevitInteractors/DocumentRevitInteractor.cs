using Contracts.RevitInteractors;
using System;
using Contracts.Models;
using System.Threading.Tasks;
using Contracts;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace RevitInteractors
{
    public class DocumentRevitInteractor : RevitInteractorBase, IDocumentRevitInteractor
    {
        async Task<CW_Document> IDocumentRevitInteractor.Get(string documentTitle)
        {
            var app = await GetApplication();

            // Interaction with Revit can only my made synchronously
            // Interaction with Revit can only be made through DocumentIdle event
            Document document = null;
            foreach (Document doc in app.Documents)
            {
                if (doc.Title == InitializeRevitInteractor.ActiveDocumentTitle)
                {
                    document = doc;
                    break;
                }
            }

            var cwDocument = new CW_Document { Title = document.Title };

            ReleaseApplication();
            return cwDocument;
        }
    }
}
