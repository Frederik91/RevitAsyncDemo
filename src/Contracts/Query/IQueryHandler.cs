using System.Threading.Tasks;

namespace Contracts.Query
{

    /// <summary>
    /// The QueryHandler interface.
    /// </summary>
    /// <typeparam name="TQuery">
    /// </typeparam>
    /// <typeparam name="TResult">
    /// </typeparam>
    public interface IQueryHandler<in TQuery, TResult>
    {
        /// <summary>
        /// The handle async.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TResult> HandleAsync(TQuery query);
    }
}
