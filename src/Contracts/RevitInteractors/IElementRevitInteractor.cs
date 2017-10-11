using System.Collections.Generic;
using Contracts.Requests;
using Contracts.Models;

namespace Contracts.RevitInteractors
{
    public interface IElementRevitInteractor
    {
        IEnumerable<CW_Element> Get(string documentTitle, string uniqueId);
        void Delete(IEnumerable<DeleteElementRequest> requests);
    }
}