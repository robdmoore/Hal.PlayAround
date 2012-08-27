using System.Collections.Generic;
using System.Web.Http;
using Hal.PlayAround.Models;
using Hal.PlayAround.Repositories;

namespace Hal.PlayAround.Controllers
{
    public class PersonController : ApiController
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET /person
        public IEnumerable<Person> Get()
        {
            return _personRepository.GetAll();
        }
        /*
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        */
    }
}