using System.Collections.Generic;
using Contracts.Requests;
using System.Threading.Tasks;

namespace API.Models.Parameters
{
    public interface IParameterService
    {
        Task SetParameters(IEnumerable<SetParameterRequest> setParameterRequests);
    }
}
