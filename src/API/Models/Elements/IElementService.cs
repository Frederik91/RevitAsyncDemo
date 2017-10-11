using Contracts.Models;
using Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Elements
{
    public interface IElementService
    {
        Task<IEnumerable<CW_Element>> Get(string documentTitle, string uniqueId);
        Task Delete(IEnumerable<DeleteElementRequest> requests);
    }
}