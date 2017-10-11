using Contracts.Enums;
using System;

namespace Contracts.Filtering
{
    public class Filter : IEquatable<Filter>
    {
        public Guid Id { get; set; }
        public OperatorType Operator { get; set; }
        public CW_ParameterLevel FilterType { get; set; }
        public string ParameterName { get; set; }
        public CompareType CompareMethod { get; set; }
        public CW_StorageType StorageType { get; set; }
        public string Value { get; set; }

        public Filter()
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }

        public void CreateCopyFrom(Filter filter)
        {
            Id = Guid.NewGuid();
            Operator = filter.Operator;
            CompareMethod = filter.CompareMethod;
            ParameterName = filter.ParameterName;
            FilterType = filter.FilterType;
            Value = filter.Value;
        }

        public override bool Equals(Object obj)
        {
            var other = obj as Filter;
            if (other == null) return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Filter other)
        {
            try
            {
                if (Operator != other.Operator) return false;
                if (FilterType != other.FilterType) return false;
                if (ParameterName != other.ParameterName) return false;
                if (CompareMethod != other.CompareMethod) return false;
                if (Value != other.Value) return false;

                return true;
            }
            catch { return false; }
        }

    }
}
