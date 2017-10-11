using Autodesk.Revit.DB;
using Contracts.Enums;
using Contracts.Filtering;
using System.Collections.Generic;
using System.Linq;

namespace RevitInteractors.Filtering
{
    public class FilterExecutor
    {
        public static List<Element> RunFilters(Document document, IEnumerable<Document> documents, IEnumerable<Filter> filters, IEnumerable<string> categoryNames, FilterType filterType)
        {
            var elements = new List<Element>();
            foreach (var doc in documents)
            {
                var filter = FilterUtils.FiltersToElementFilter(doc, filters, categoryNames);
                var elementsInDocument = new List<Element>();
                if (filter != null)
                {
                    switch (filterType)
                    {
                        case FilterType.Both:
                            elementsInDocument = new FilteredElementCollector(doc).WherePasses(filter).ToList();
                            break;
                        case FilterType.Instance:
                            elementsInDocument = new FilteredElementCollector(doc).WherePasses(filter).WhereElementIsNotElementType().ToList();
                            break;
                        case FilterType.Type:
                            elementsInDocument = new FilteredElementCollector(doc).WherePasses(filter).WhereElementIsElementType().ToList();
                            break;
                    }
                   
                }
                elements.AddRange(elementsInDocument);
            }

            return elements;
        }
    }
}


