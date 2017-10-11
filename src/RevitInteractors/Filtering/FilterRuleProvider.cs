using Autodesk.Revit.DB;
using Contracts.Enums;
using System.Globalization;

namespace RevitInteractors.Filtering
{
    public class FilterRuleProvider
    {
        public static FilterNumericRuleEvaluator GetNumericRuleEvaluator(CompareType compareType)
        {
            switch (compareType)
            {
                case CompareType.Equals:
                    return new FilterNumericEquals();
                case CompareType.Greater:
                    return new FilterNumericGreater();
                case CompareType.GreaterOrEqual:
                    return new FilterNumericGreaterOrEqual();
                case CompareType.Less:
                    return new FilterNumericLess();
                case CompareType.LessOrEqual:
                    return new FilterNumericLessOrEqual();
                default:
                    return new FilterNumericEquals();
            }
        }

        public static FilterStringRuleEvaluator GetStringRuleEvaluator(CompareType compareType)
        {
            switch (compareType)
            {
                case CompareType.Equals:
                    return new FilterStringEquals();
                case CompareType.BeginsWith:
                    return new FilterStringBeginsWith();
                case CompareType.Contains:
                    return new FilterStringContains();
                case CompareType.EndsWith:
                    return new FilterStringEndsWith();
                case CompareType.Greater:
                    return new FilterStringGreater();
                case CompareType.GreaterOrEqual:
                    return new FilterStringGreaterOrEqual();
                case CompareType.Less:
                    return new FilterStringLess();
                case CompareType.LessOrEqual:
                    return new FilterStringLessOrEqual();
                default:
                    return new FilterStringEquals();
            }
        }

        public static FilterRule GetFilterRule(string value, FilterableValueProvider fvp, StorageType storageType, CompareType compareType)
        {
            switch (storageType)
            {
                case StorageType.Integer:
                    if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int intValue))
                    {
                        return new FilterIntegerRule(fvp, GetNumericRuleEvaluator(compareType), intValue);
                    }
                    else
                    {
                        return new FilterIntegerRule(fvp, GetNumericRuleEvaluator(compareType), -1);
                    }
                case StorageType.Double:
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        return new FilterDoubleRule(fvp, GetNumericRuleEvaluator(compareType), doubleValue, double.Epsilon);
                    }
                    else
                    {
                        return new FilterDoubleRule(fvp, GetNumericRuleEvaluator(compareType), 0, double.Epsilon);
                    }
                case StorageType.String:
                    return new FilterStringRule(fvp, GetStringRuleEvaluator(compareType), value, false);
                case StorageType.ElementId:
                    if (int.TryParse(value, out int intVal))
                    {
                        return new FilterElementIdRule(fvp, GetNumericRuleEvaluator(compareType), new ElementId(intVal));
                    }
                    else
                    {
                        return new FilterElementIdRule(fvp, GetNumericRuleEvaluator(compareType), new ElementId(-1));
                    }                    
                default:
                    return null;

            }
        }
    }
}
