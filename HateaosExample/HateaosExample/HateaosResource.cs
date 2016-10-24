using System.Collections.Generic;

namespace HateaosExample
{
    public abstract class HateaosResource
    {
        private readonly List<Link> _links = new List<Link>();

        public IEnumerable<Link> Links => _links;

        public void AddLinks(IEnumerable<Link> links)
        {
            _links.AddRange(links);
        }
    }
}