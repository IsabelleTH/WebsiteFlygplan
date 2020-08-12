using System.Collections.Generic;
using System.Web.Http;

namespace WebsiteFlygplan.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET: WebApi
        public IEnumerable<string> Get()
        {
            return new string[] { "This is a GET http verb" };
        }

        // GET api/WebApi/5  
        public string Get(int id)
        {
            return "Hello this is a GET Api call with id: " + id;
        }

        // POST api/WebApi 
        public void Post([FromBody] string value)
        {

        }

        // PUT api/WebApi/5 
        public void Post(int id, [FromBody] string value)
        {

        }

        // DELETE api/WebApi/5 
        public void Delete(int id)
        {

        }
    }
}