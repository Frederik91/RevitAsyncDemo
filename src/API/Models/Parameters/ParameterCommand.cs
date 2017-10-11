using Contracts.Requests;
using System.Collections.Generic;

namespace CW_Revit.Models.Parameters
{
    public class ParameterCommand
    {
        public IEnumerable<SetParameterRequest> SetParameterRequests { get; set; }
    }
}
