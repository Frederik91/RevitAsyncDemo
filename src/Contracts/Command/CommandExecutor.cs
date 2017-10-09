using Contracts.Logging;
using LightInject;
using System;
using System.Threading.Tasks;

namespace Contracts.Command
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IServiceFactory factory;
        private readonly ILogger m_logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutor"/> class.
        /// </summary>
        /// <param name="factory">The <see cref="IServiceFactory"/> that is used to 
        /// resolve the <see cref="ICommandHandler{TCommand}"/> to be executed.</param>
        public CommandExecutor(IServiceFactory factory, ILogger logger)
        {
            this.factory = factory;
            m_logger = logger;
        }

        /// <summary>
        /// Executes the given <paramref name="command"/>.
        /// </summary>
        /// <typeparam name="TCommand">The type of command to be executed.</typeparam>
        /// <param name="command">The command to be executed.</param>
        /// <returns><see cref="Task"/>.</returns>
        public async Task Execute<TCommand>(TCommand command)
        {
            try
            {
                await factory.GetInstance<ICommandHandler<TCommand>>().Handle(command);
            }
            catch (Exception exception)
            {
                LogException(exception, command.GetType());
                throw exception;
            }
        }

        private void LogException(Exception exception, Type commandHandlerType)
        {
            var queryString = $"Message: {exception.Message}.\nStackTrace: {exception.StackTrace}.\nCommandHandlerType {commandHandlerType.Name}";
            m_logger.Log(queryString);
        }
    }
}
