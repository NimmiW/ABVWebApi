using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ABVWebApi.Controllers
{
    public class BalanceController : ApiController
    {
        // GET: api/Balance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Balance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Balance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Balance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Balance/5
        public void Delete(int id)
        {
        }
    }
}
