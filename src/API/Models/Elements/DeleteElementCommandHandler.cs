using Contracts.Command;
using Contracts.RevitInteractors;

namespace API.Models.Elements
{
    public class DeleteElementCommandHandler : ICommandHandler<DeleteElementCommand>
    {
        private readonly IElementRevitInteractor m_elementRevitInteractor;

        public DeleteElementCommandHandler(IElementRevitInteractor elementRevitInteractor)
        {
            m_elementRevitInteractor = elementRevitInteractor;
        }

        public void Handle(DeleteElementCommand command)
        {
            m_elementRevitInteractor.Delete(command.Requests);
        }
    }
}
