using Contracts.Command;
using Contracts.Query;
using Contracts.Requests;
using CW_Revit.Models.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Parameters
{
    public class ParameterService : IParameterService
    {
        private readonly ICommandExecutor m_commandExecutor;
        private readonly IQueryExecutor m_queryExecutor;

        public ParameterService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }

        public async Task SetParameters(IEnumerable<SetParameterRequest> setParameterRequests)
        {
            await m_commandExecutor.Execute(new ParameterCommand { SetParameterRequests = setParameterRequests});
        }
    }
}
