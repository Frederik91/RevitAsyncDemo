using Contracts.Enums;
using Contracts.Models;
using System;

namespace Contracts.Filtering
{
    public interface IFilter
    {
        string DocumentName { get; set; }
        Guid Id { get; set; }
        OperatorType Operator { get; set; }
        CompareType CompareMethod { get; set; }
        bool IsInternal { get; set; }
        CW_Parameter CwParameter { get; set; }
        CW_ParameterType Type { get; set; }

        void CreateCopyFrom(Filter filter);
    }
}
