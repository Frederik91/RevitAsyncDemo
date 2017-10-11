using Contracts.Enums;
using System;

namespace Contracts.Models
{
    public class CW_InternalDefinition : CW_Definition
    {
        public int Id { get; set; }
        public int BuiltInParameter { get; set; }
        public CW_UnitType UnitType {get;set;}
    }
}
