using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts.RevitInteractors
{
    public interface IDocumentRevitInteractor
    {
        CW_Document Get(string documentTitle);
    }
}
