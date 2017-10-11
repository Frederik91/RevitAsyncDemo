using Contracts.Command;
using Contracts.RevitInteractors;

namespace CW_Revit.Models.Parameters
{
    public class ParameterCommandHandler : ICommandHandler<ParameterCommand>
    {
        private readonly IParameterRevitInteractor m_parameterRevitInteractor;

        public ParameterCommandHandler(IParameterRevitInteractor parameterRevitInteractor)
        {
            m_parameterRevitInteractor = parameterRevitInteractor;
        }

        public void Handle(ParameterCommand command)
        {
            m_parameterRevitInteractor.SetParameters(command.SetParameterRequests);
        }
    }
}
