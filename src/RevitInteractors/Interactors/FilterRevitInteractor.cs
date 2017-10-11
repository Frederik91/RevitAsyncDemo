using System.Collections.Generic;
using Autodesk.Revit.DB;
using RevitInteractors.Filtering;
using Contracts.RevitInteractors;
using Contracts.Filtering;
using Contracts.Enums;
using Contracts.MinimalModels;

namespace RevitInteractors.Interactors
{
    public class FilterRevitInteractor : RevitInteractorBase, IFilterRevitInteractor
    {
        public IEnumerable<CW_ElementMinimal> GetMinimal(IEnumerable<string> documentTitles, IEnumerable<Filter> filters, IEnumerable<string> categoryNames, FilterType filterType)
        {
            var documents = new List<Document>();
            var document = GetDocument();
            foreach (var documentTitle in documentTitles)
            {
                var doc = GetDocument(documentTitle);
                documents.Add(doc);
            }

            var filterResult = FilterExecutor.RunFilters(document, documents, filters, categoryNames, filterType);
            var cwElements = new List<CW_ElementMinimal>();
            foreach (var element in filterResult)
            {
                CW_ElementMinimal cwElement = new CW_ElementMinimal
                {
                    DocumentTitle = element.Document.Title,
                    Id = element.Id.IntegerValue,
                    Name = element.Name,
                    UniqueId = element.UniqueId
                };
                cwElements.Add(cwElement);
            }
            return cwElements;
        }
    }
}
