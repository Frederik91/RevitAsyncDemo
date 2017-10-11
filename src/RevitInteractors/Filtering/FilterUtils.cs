using Autodesk.Revit.DB;
using Contracts.Enums;
using Contracts.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitInteractors.Filtering
{
    public class FilterUtils
    {
        public static ElementFilter FiltersToElementFilter(Document document, IEnumerable<Filter> filters, IEnumerable<string> categoryNames)
        {
            var parameters = new List<Parameter>();

            var orElementFilters = new List<ElementFilter>();
            var andElementFilters = new List<ElementFilter>();

            var filtersWithParameterNames = filters.Where(x => !string.IsNullOrEmpty(x.ParameterName));

            ElementFilter combinedFilter = null;
            if (filtersWithParameterNames.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    if (string.IsNullOrEmpty(filter.Value))
                    {
                        continue;
                    }

                    ElementId parameterId;
                    if (GetParameterIdByName(document, filter.ParameterName) is ElementId id)
                    {
                        parameterId = id;
                    }
                    else
                    {
                        continue;
                    }

                    var pvp = new ParameterValueProvider(parameterId);
                    var filterRule = FilterRuleProvider.GetFilterRule(filter.Value, pvp, (StorageType)Enum.Parse(typeof(StorageType), filter.StorageType.ToString()), filter.CompareMethod);
                    var elementFilter = new ElementParameterFilter(filterRule, filter.Operator == OperatorType.Nand || filter.Operator == OperatorType.Nor);
                    if (filter.Operator == OperatorType.And || filter.Operator == OperatorType.Nand)
                    {
                        andElementFilters.Add(elementFilter);
                    }
                    if (filter.Operator == OperatorType.Or || filter.Operator == OperatorType.Nor)
                    {
                        orElementFilters.Add(elementFilter);
                    }
                }


                var filterList = new List<ElementFilter>();
                ElementFilter orFilter = null;

                if (orElementFilters.Count > 1)
                {
                    orFilter = new LogicalOrFilter(orElementFilters);
                }
                else if (orElementFilters.Count == 1)
                {
                    orFilter = orElementFilters.First();
                }

                if (andElementFilters.Count > 1)
                {
                    var excludeFilter = new LogicalAndFilter(andElementFilters);
                    filterList.Add(excludeFilter);
                }
                else if (andElementFilters.Count == 1)
                {
                    filterList.Add(andElementFilters.First());
                }

                if (filterList.Count > 1)
                {
                    combinedFilter = new LogicalAndFilter(filterList);
                }
                if (filterList.Count == 1)
                {
                    combinedFilter = filterList.First();
                }

                if (combinedFilter != null && orFilter != null)
                {
                    combinedFilter = new LogicalAndFilter(combinedFilter, orFilter);
                }

                if (filterList.Count == 0 && orFilter != null)
                {
                    combinedFilter = orFilter;
                }
            }

            ElementFilter categoriesFilter = null;
            if (categoryNames != null && categoryNames.Count() > 0)
            {
                var categoryIds = new List<ElementId>();
                foreach (var categoryName in categoryNames)
                {
                    var category = document.Settings.Categories.get_Item(categoryName);
                    if (category != null)
                    {
                        categoryIds.Add(category.Id);
                    }
                }

                var categoryFilters = new List<ElementFilter>();
                if (categoryIds.Count > 0)
                {
                    var categoryRule = new FilterCategoryRule(categoryIds);
                    var categoryFilter = new ElementParameterFilter(categoryRule);
                    categoryFilters.Add(categoryFilter);
                }
                if (categoryFilters.Count > 0)
                {
                    categoriesFilter = new LogicalOrFilter(categoryFilters);
                }
            }

            if (combinedFilter != null && categoriesFilter != null)
            {
                combinedFilter = new LogicalAndFilter(combinedFilter, categoriesFilter);
            }
            else if (combinedFilter == null && categoriesFilter != null)
            {
                combinedFilter = categoriesFilter;
            }
            
            return combinedFilter;
        }

        private static ElementId GetParameterIdByName(Document document, string parameterName)
        {
            if (Enum.TryParse(parameterName, out BuiltInParameter bip) && Enum.IsDefined(typeof(BuiltInParameter), bip))
            {
                return new ElementId(bip);
            }

            var iterator = document.ParameterBindings.ForwardIterator();

            while (iterator.MoveNext())
            {
                if (iterator.Key.Name == parameterName)
                {
                    if (iterator.Key is InternalDefinition internalDefinition)
                    {
                        return internalDefinition.Id;
                    }
                    else
                    {
                        var extDef = iterator.Key as ExternalDefinition;
                        ElementFilter filter;
                        if (iterator.Current is InstanceBinding instanceBinding)
                        {
                            var categoryFilters = new List<ElementFilter>();
                            foreach (Category category in instanceBinding.Categories)
                            {
                                categoryFilters.Add(new ElementCategoryFilter(category.Id));
                            }
                            var categoryFilter = new LogicalOrFilter(categoryFilters);
                            var classFilters = new List<ElementFilter>
                            {
                                new ElementClassFilter(typeof(SpatialElement)),
                                new ElementClassFilter(typeof(FamilyInstance)),
                                new ElementClassFilter(typeof(AssemblyInstance)),
                                new ElementClassFilter(typeof(Group)),
                            };
                            var classFilter = new LogicalOrFilter(classFilters);
                            filter = new LogicalAndFilter(classFilter, categoryFilter);
                        }
                        else
                        {
                            var typeBinding = iterator.Current as TypeBinding;
                            var categoryFilters = new List<ElementFilter>();
                            foreach (Category category in typeBinding.Categories)
                            {
                                categoryFilters.Add(new ElementCategoryFilter(category.Id));
                            }
                            var categoryFilter = new LogicalOrFilter(categoryFilters);
                            var classFilters = new List<ElementFilter>
                            {
                                new ElementClassFilter(typeof(FamilySymbol)),
                                new ElementClassFilter(typeof(AssemblyType)),
                                new ElementClassFilter(typeof(GroupType)),
                            };
                            var classFilter = new LogicalOrFilter(classFilters);
                            filter = new LogicalAndFilter(classFilter, categoryFilter);
                        }
                        var collector = new FilteredElementCollector(document).WherePasses(filter);
                        foreach (var element in collector)
                        {
                            if (element.GetParameters(parameterName).FirstOrDefault() is Parameter parameter)
                            {
                                return parameter.Id;
                            }
                        }
                    }

                }
            }
            return null;
        }
    }
}
