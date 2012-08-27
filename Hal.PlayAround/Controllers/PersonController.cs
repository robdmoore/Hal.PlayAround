using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        
        // GET /person/1
        public Person Get(int id)
        {
            return _personRepository.Get(id);
        }

        // POST /person
        public HttpResponseMessage Post(Person person)
        {
            _personRepository.Insert(person);

            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("Api", new { Controller = "Person", person.Id }));

            return response;
        }

        // PUT /person/1
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE /person/1
        public void Delete(int id)
        {
        }
    }
}