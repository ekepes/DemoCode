namespace MassTransit.Play.Subscriber.Orm
{
    using NHibernate;

    /// <summary>
    /// Stores a fetch mode combination.
    /// </summary>
    public class CriteriaFetchMode
    {
        private readonly string _CollectionName;
        private readonly FetchMode _FetchModeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaFetchMode"/> class.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="fetchModeType">Type of the fetch mode.</param>
        public CriteriaFetchMode(string collectionName,
                                 FetchMode fetchModeType)
        {
            _CollectionName = collectionName;
            _FetchModeType = fetchModeType;
        }

        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        /// <value>The name of the collection.</value>
        public string CollectionName
        {
            get { return _CollectionName; }
        }

        /// <summary>
        /// Gets the type of the fetch mode.
        /// </summary>
        /// <value>The type of the fetch mode.</value>
        public FetchMode FetchModeType
        {
            get { return _FetchModeType; }
        }
    }
}