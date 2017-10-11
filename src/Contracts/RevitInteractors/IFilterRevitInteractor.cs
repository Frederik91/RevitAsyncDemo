using Contracts.Enums;
using Contracts.Filtering;
using Contracts.MinimalModels;
using System.Collections.Generic;

namespace Contracts.RevitInteractors
{
    public interface IFilterRevitInteractor
    {
        IEnumerable<CW_ElementMinimal> GetMinimal(IEnumerable<string> documentTitles, IEnumerable<Filter> filters, IEnumerable<string> categoryNames, FilterType filterType);
    }
}
