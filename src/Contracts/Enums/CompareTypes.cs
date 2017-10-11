using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Enums
{
    public enum CompareType
    {
        Equals,
        DoesNotEqual,
        BeginsWith,
        Contains,
        EndsWith,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual
    }

    public class CompareTypeUtils
    {
        public static IEnumerable<CompareType> AllCompareTypes
        {
            get { return Enum.GetValues(typeof(CompareType)).Cast<CompareType>(); }
        }

        public static IEnumerable<CompareType> StringCompareTypes
        {
            get => new List<CompareType>
            {
                CompareType.BeginsWith,
                CompareType.Contains,
                CompareType.EndsWith,
                CompareType.Equals
            };
        }

        public static IEnumerable<CompareType> NumericCompareTypes
        {
            get => new List<CompareType>
            {
                CompareType.Equals,
                CompareType.Greater,
                CompareType.GreaterOrEqual,
                CompareType.Less,
                CompareType.LessOrEqual
            };
        }

        public static IEnumerable<CompareType> ElementIdCompareTypes
        {
            get => new List<CompareType>
            {
                CompareType.Equals
            };
        }
    }
}
