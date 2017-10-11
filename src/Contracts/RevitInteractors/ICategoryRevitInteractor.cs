using Contracts.Enums;
using Contracts.Models;
using System.Collections.Generic;

namespace Contracts.RevitInteractors
{
    public interface ICategoryRevitInteractor
    {
        IEnumerable<CW_Category> Get(IEnumerable<string> documentTitles, string idOrName, bool mustContainElements, IEnumerable<CW_CategoryType> categoryTypes);
    }
}
