namespace MassTransit.Play.Subscriber.Orm
{
    using System;
    using System.Collections.Generic;

    using NHibernate;

    public class OrmRepository<T> : OrmRepository<T, long>, IOrmRepository<T>
    {
        public OrmRepository(ISession session)
            : base(session)
        {
        }
    }

    /// <summary>
    /// Provides a base for more specialized data providers that 
    /// do not provide the generic functionality provided by
    /// AbstractDataProvider.
    /// </summary>
    public class OrmRepository<T, TId> : IOrmRepository<T, TId>
    {
        protected readonly ISession _Session;

        private readonly bool _WeOwnTheTransaction;

        public OrmRepository(ISession session)
        {
            _Session = session;
            if(_Session.Transaction == null || 
                !_Session.Transaction.IsActive)
            {
                _Session.BeginTransaction();
                _WeOwnTheTransaction = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_WeOwnTheTransaction)
                {
                    _Session.Transaction.Commit();
                }
            }
        }

        private readonly Type _PersistentType = typeof(T);

        /// <summary>
        /// Merges the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Merge(T entity)
        {
            return (T)_Session.Merge(entity);
        }

        /// <summary>
        /// Saves the or update copy.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T SaveOrUpdateCopy(T entity)
        {
            return (T)_Session.SaveOrUpdateCopy(entity);
        }

        /// <summary>
        /// Gets the specified id from the database using the key value
        /// </summary>
        /// <remarks>
        /// Use this instead of get by id when you are not sure if a value exists. <c>GetById</c> will
        /// thrown and exception when the item does not exist. In contrast, This method (Get) will 
        /// return <c>null</c>. 
        /// </remarks>
        /// <param name="id">The id.</param>
        /// <returns>An instance of the object, returns <c>null</c> if the object does not exist in the database.</returns>
        public virtual T Get(TId id)
        {
            return Get(id,
                       false);
        }

        /// <summary>
        /// Gets the specified id from the database using the key value
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="shouldLock">if set to <c>true</c> [should lock].</param>
        /// <returns>
        /// An instance of the object, returns <c>null</c> if the object does not exist in the database.
        /// </returns>
        /// <remarks>
        /// Use this instead of get by id when you are not sure if a value exists. <c>GetById</c> will
        /// thrown and exception when the item does not exist. In contrast, This method (Get) will
        /// return <c>null</c>.
        /// Use this overload if you need to lock the object. The object will be locked
        /// as <c>LockMode.Upgrade</c>
        /// </remarks>
        public virtual T Get(TId id,
                             bool shouldLock)
        {
            // No logging - this can get called in tight loops

            T entity;

            if (shouldLock)
            {
                entity = (T)_Session.Get(_PersistentType,
                                                   id,
                                                   LockMode.Upgrade);
            }
            else
            {
                entity = (T)_Session.Get(_PersistentType,
                                                   id);
            }

            return entity;
        }

        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        /// <returns>A list of all the instances in the database.</returns>
        /// <remarks>
        /// Keep in mind that this method will load all data from the table
        /// underlying this class. If this table has a large number of rows
        /// (more than 100, depending upon your usage), you probably should
        /// use one of the filtered methods (<c>GetByCriteria</c>,
        /// <c>GetByHql</c>).
        /// </remarks>
        public virtual List<T> GetAll()
        {
            List<T> all = _Session.CreateCriteria(_PersistentType).List<T>() as List<T>;

            return all;
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The saved entity. This allows chaining method calls.</returns>
        /// <remarks>
        /// For entities that have assigned Id's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </remarks>
        public virtual T Save(T entity)
        {
            // No logging - this can get called in tight loops

            _Session.Save(entity);

            return entity;
        }

        /// <summary>
        /// Updates the session.
        /// </summary>
        public virtual void Update(T entity)
        {
            // No logging - this can get called in tight loops

            _Session.SaveOrUpdateCopy(entity);
        }

        /// <summary>
        /// Refreshes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Refresh(T entity)
        {
            // No logging - this can get called in tight loops

            _Session.Refresh(entity);
        }

        /// <summary>
        /// Saves or updates the entity to the database.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The saved entity. This allows chaining method calls.</returns>
        /// <remarks>
        /// For entities with automatically generated IDs, such as identity, SaveOrUpdate may
        /// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any
        /// entity, even if its ID is assigned.
        /// </remarks>
        public virtual T SaveOrUpdate(T entity)
        {
            // No logging - this can get called in tight loops

            _Session.SaveOrUpdate(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual void Delete(T entity)
        {
            // No logging - this can get called in tight loops

            _Session.Delete(entity);
        }

        /// <summary>
        /// Evicts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected void Evict(T entity)
        {
            // No logging - this can get called in tight loops
            _Session.Evict(entity);
        }

        /// <summary>
        /// Evicts the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        protected void Evict(List<T> entities)
        {
            // No logging - this can get called in tight loops
            _Session.Evict(entities);
        }

        private void AddFetchModes(ICriteria criteria, IList<CriteriaFetchMode> fetchModes)
        {
            if (fetchModes != null)
            {
                foreach (CriteriaFetchMode mode in fetchModes)
                {
                    criteria.SetFetchMode(mode.CollectionName,
                                          mode.FetchModeType);
                }
            }
        }
    }
}