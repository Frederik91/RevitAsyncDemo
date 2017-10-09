using Contracts.RevitInteractors;
using System;
using Contracts.Models;
using System.Threading.Tasks;
using Contracts;
using Autodesk.Revit.UI;

namespace RevitInteractors
{
    public class DocumentRevitInteractor : IDocumentRevitInteractor
    {
        Task<CW_Document> IDocumentRevitInteractor.Get(string documentTitle)
        {
            // Interaction with Revit can only my made synchronously
            // Interaction with Revit can only be made through DocumentIdle event

            if (string.IsNullOrEmpty(documentTitle))
            {
                var externalCommandData = ExternalCommandDataHolder.ExternalCommand as ExternalCommandData;
                new CW_Document { Title = externalCommandData.Application.ActiveUIDocument.Document.Title };
            }

            throw new NotImplementedException();
        }
    }
}
