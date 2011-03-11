namespace MassTransit.Play.Subscriber.Orm
{
    public interface IOrmRepository<T> : IOrmRepository<T, long>, IRepository<T>
    {

    }

    public interface IOrmRepository<T, TId> : IRepository<T, TId>
    {
        /// <summary>
        /// Saves the or update copy.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T SaveOrUpdateCopy(T entity);

        /// <summary>
        /// Merges the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        T Merge(T entity);

        /// <summary>
        /// Refreshes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Refresh(T entity);

        /// <summary>
        /// Gets the specified id from the database using the key value
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="shouldLock">if set to <c>true</c> [should lock].</param>
        /// <returns>
        /// An instance of the object, returns <see langword="null"/> if the object does not exist in the database.
        /// </returns>
        /// <remarks>
        /// Use this instead of get by id when you are not sure if a value exists. <c>GetById</c> will
        /// thrown and exception when the item does not exist. In contrast, This method (Get) will
        /// return <see langword="null"/>.
        /// Use this overload if you need to lock the object. The object will be locked
        /// as <c>LockMode.Upgrade</c>
        /// </remarks>
        T Get(TId id,
              bool shouldLock);
    }
}