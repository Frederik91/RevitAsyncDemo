using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contracts.Models
{
    public class CW_Element
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Name { get; set; }
        public CW_Document Document { get; set; }
        public CW_Category Category { get; set; }
        public List<CW_Parameter> Parameters { get; set; }
        public int LevelId { get; set; }
        public bool Pinned { get; set; }
        public int AssemblyInstanceId { get; set; }
    

        public CW_Parameter GetParameter(CW_Definition definition)
        {
            return Parameters.FirstOrDefault(x => x.Definition == definition);
        }

        public CW_Parameter GetParameter(string name)
        {
            return Parameters.FirstOrDefault(x => x.Definition.Name == name);
        }

        public CW_Parameter GetParameter(int Id)
        {
            return Parameters.FirstOrDefault(x => x.Id == Id);
        }

        public CW_Parameter GetParameter(Guid guid)
        {
            return Parameters.FirstOrDefault(x => x.GUID == guid);
        }
    }
}
