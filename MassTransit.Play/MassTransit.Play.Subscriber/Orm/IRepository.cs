namespace MassTransit.Play.Subscriber.Orm
{
    using System;
    using System.Collections.Generic;

    public interface IRepository : IDisposable
    {
    }

    /// <summary>
    /// The interface to define the methods needed to encapsulate data access.
    /// </summary>
    /// <remarks>
    /// Generally, you should inherit from <c>AbstractNHibernateDao</c> instead of creating your
    /// own implementation of this interface.
    /// </remarks>
    /// <typeparam name="T">The type that will be persisted using the class that implements this interface.</typeparam>
    /// <typeparam name="TId">The type of the database key for the class that implements this interface.</typeparam>
    public interface IRepository<T, TId> : IRepository
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets the specified id from the database using the key value
        /// </summary>
        /// <remarks>
        /// Use this instead of get by id when you are not sure if a value exists. <c>GetById</c> will
        /// thrown and exception when the item does not exist. In contrast, This method (Get) will 
        /// return <see langword="null"/>.
        /// </remarks>
        /// <param name="id">The id.</param>
        /// <returns>An instance of the object, returns <see langword="null"/> if the object does not exist in the database.</returns>
        T Get(TId id);

        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        /// <returns>A list of all the instances in the database.</returns>
        /// <remarks>
        /// Keep in mind that this method will load all data from the table
        /// underlying this class. If this table has a large number of rows
        /// (more than 100, depending upon your usage), you probably should use
        /// a Filter.
        /// </remarks>
        List<T> GetAll();

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The saved entity. This allows chaining method calls.</returns>
        /// <remarks>
        /// For entities that have assigned id's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </remarks>
        T Save(T entity);

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
        T SaveOrUpdate(T entity);

        /// <summary>
        /// Updates the session.
        /// </summary>
        void Update(T entity);
    }

    public interface IRepository<T> : IRepository<T, long>
    {
    }
}