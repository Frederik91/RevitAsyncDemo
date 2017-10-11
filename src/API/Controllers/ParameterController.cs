using API.Models.Parameters;
using Contracts.Models;
using Contracts.Requests;
using CW_Revit.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ParameterController
    {
        private readonly IParameterService m_parameterService;

        public ParameterController(IParameterService parameterService)
        {
            m_parameterService = parameterService;
        }

        public async Task Set(CW_Parameter parameter, string value)
        {
            await m_parameterService.SetParameters(new List<SetParameterRequest> { new SetParameterRequest { ElementId = parameter.OwnerId, ParameterId = parameter.Id.ToString(), Value = parameter.Value } });
        }

        public async Task Set(string parameterId, string ownerId, string value)
        {
            await m_parameterService.SetParameters(new List<SetParameterRequest> { new SetParameterRequest { ElementId = ownerId, ParameterId = parameterId, Value = value } });
        }

        public async Task Set(SetParameterRequest setParameterRequest)
        {
            await m_parameterService.SetParameters(new List<SetParameterRequest> { setParameterRequest });
        }

        public async Task Set(IEnumerable<SetParameterRequest> setParameterRequests)
        {
            await m_parameterService.SetParameters(setParameterRequests);
        }       
    }
}
