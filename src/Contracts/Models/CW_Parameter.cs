using Contracts.Enums;
using System;

namespace Contracts.Models
{
    public class CW_Parameter
    {
        public CW_Definition Definition {get;set;}
        public Guid GUID { get; set; }
        public bool HasValue { get; set; }
        public bool IsShared { get; set; }
        public bool IsReadOnly { get; set; }
        public string OwnerId { get; set; }
        public int Id { get; set; }
        public CW_StorageType StorageType { get; set; }
        public string Value { get; set; }
        public string HumanReadableValue { get; set; }
    }
}
