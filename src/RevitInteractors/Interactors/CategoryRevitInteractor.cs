using System.Collections.Generic;
using Contracts.Models;
using Contracts.RevitInteractors;
using Autodesk.Revit.DB;
using System.Linq;
using Contracts.Enums;

namespace RevitInteractors.Interactors
{
    public class CategoryRevitInteractor : RevitInteractorBase, ICategoryRevitInteractor
    {
        public IEnumerable<CW_Category> Get(IEnumerable<string> documentTitles, string idOrName, bool mustContainElements, IEnumerable<CW_CategoryType> categoryTypes)
        {
            var result = new List<CW_Category>();

            foreach (var documentTitle in documentTitles)
            {
                var document = GetDocument(documentTitle);

                if (int.TryParse(idOrName, out int elementIdValue))
                {
                    var iterator = document.Settings.Categories.ForwardIterator();
                    while (iterator.MoveNext())
                    {
                        var category = iterator.Current as Category;
                        if (category.Id.IntegerValue == elementIdValue)
                        {
                            var cwCategory = RvtToCwElementConverter.SetCategoryData(new CW_Category(), category);
                            result.Add(cwCategory);
                            break;
                        }
                    }
                    break;
                }
                else if (idOrName != string.Empty)
                {
                    var category = document.Settings.Categories.get_Item(idOrName);
                    if (category != null)
                    {
                        var cwCategory = RvtToCwElementConverter.SetCategoryData(new CW_Category(), category);
                        result.Add(cwCategory);
                        break;
                    }
                }
                else
                {
                    foreach (Category category in document.Settings.Categories)
                    {
                        if (categoryTypes == null || categoryTypes.Any(x => (int)x == (int)category.CategoryType) || result.Any(x => x.Name == category.Name))
                        {
                            if (mustContainElements)
                            {
                                var categoryEvaluator = new FilterCategoryRule(new List<ElementId> { category.Id });
                                var categoryFilter = new ElementParameterFilter(categoryEvaluator);
                                var collector = new FilteredElementCollector(document).WherePasses(categoryFilter).WhereElementIsNotElementType();

                                if (collector.Any())
                                {
                                    var cwCategory = RvtToCwElementConverter.SetCategoryData(new CW_Category(), category);
                                    result.Add(cwCategory);
                                }
                            }
                            else
                            {
                                var cwCategory = RvtToCwElementConverter.SetCategoryData(new CW_Category(), category);
                                result.Add(cwCategory);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
