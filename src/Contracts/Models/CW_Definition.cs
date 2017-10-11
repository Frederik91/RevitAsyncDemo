using Contracts.Enums;
using System;
using System.Xml.Serialization;

namespace Contracts.Models
{
    public class CW_Definition
    {
        public string Name { get; set; }
        public CW_ParameterType ParameterType { get; set; }
    }
}