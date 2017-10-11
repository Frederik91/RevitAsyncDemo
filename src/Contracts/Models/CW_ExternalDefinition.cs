using System;

namespace Contracts.Models
{
    public class CW_ExternalDefinition : CW_Definition
    {
        public Guid GUID { get; set; }
        public string Description { get; set; }
    }
}
