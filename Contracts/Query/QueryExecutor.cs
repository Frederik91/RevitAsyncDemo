using Contracts.Logging;
using Contracts.Query;
using LightInject;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Contracts.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IServiceFactory factory;
        private readonly ILogger m_logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryExecutor"/> class.
        /// </summary>
        /// <param name="factory">The <see cref="IServiceFactory"/> used to resolve the 
        /// <see cref="IQueryHandler{TQuery,TResult}"/> to be executed.</param>
        public QueryExecutor(IServiceFactory factory, ILogger logger)
        {
            this.factory = factory;
            m_logger = logger;
        }

        /// <summary>
        /// Executes the given <paramref name="query"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of result returned by the query.</typeparam>
        /// <param name="query">The query to be executed.</param>
        /// <returns>The result from the query.</returns>
        public async Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
        {
            Type queryType = query.GetType();
            Type queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
            dynamic queryHandler = factory.GetInstance(queryHandlerType);


            TResult result;
            try
            {
                result = await queryHandler.HandleAsync((dynamic)query);
                return result;
            }
            catch (Exception exception)
            {
                LogException<TResult>(exception, queryHandlerType, query);
                throw exception;
            }
        }

        private void LogException<TResult>(Exception exception, Type queryHandlerType, IQuery<TResult> query)
        {
            Type myType = query.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            string queryString = "\n";
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(query);
                string propValueString = "";
                if (propValue is string[] stringarrayValues)
                {
                    foreach (var item in stringarrayValues)
                    {
                        propValueString += $"{item}, ";
                    }
                }
                else
                {
                    propValueString = $"{propValue}";
                }
                queryString += $"{prop}: {propValueString}\n";
            }

            m_logger.Log(queryString);
        }
    }
}
