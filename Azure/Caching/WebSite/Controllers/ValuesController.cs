using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.ApplicationServer.Caching;

namespace WebSite.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly DataCache _cache = new DataCache();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return _cache.GetObjectsInRegion("").Select(x => x.Value.ToString());
        }

        // GET api/values/5
        public string Get(string id)
        {
            return _cache.Get(id).ToString();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            _cache.Add(Guid.NewGuid().ToString(), value);
        }

        // PUT api/values/5
        public void Put(string id, [FromBody]string value)
        {
            _cache.Put(id, value);
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            _cache.Remove(id);
        }
    }
}