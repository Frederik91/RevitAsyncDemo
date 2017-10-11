using Contracts.Requests;
using System.Collections.Generic;

namespace Contracts.RevitInteractors
{
    public interface IParameterRevitInteractor
    {
        void SetParameters(IEnumerable<SetParameterRequest> setParameterRequests);
    }
}