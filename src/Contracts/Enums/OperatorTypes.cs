using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Enums
{
    public enum OperatorType
    {
        Or,
        Nor,
        And,
        Nand,
        Ignore
    }
    
    public class OperatorTypeUtils
    {
        public static IEnumerable<OperatorType> AllOperators
        {
            get { return Enum.GetValues(typeof(OperatorType)).Cast<OperatorType>(); }
        }
    }
}
