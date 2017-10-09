using Contracts.Models;
using System.Threading.Tasks;

namespace API.Models.Documents
{
    public interface IDocumentService
    {
        Task<CW_Document> Get(string documentTitle);
    }
}
