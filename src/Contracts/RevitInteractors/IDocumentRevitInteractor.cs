using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts.RevitInteractors
{
    public interface IDocumentRevitInteractor
    {
        Task<CW_Document> Get(string documentTitle);
    }
}
