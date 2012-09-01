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
            var person = _personRepository.Get(id);
            if (person == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return person;
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
        public void Put(Person person)
        {
            if (_personRepository.Get(person.Id) == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _personRepository.Update(person);
        }

        // DELETE /person/1
        public void Delete(int id)
        {
            if (_personRepository.Get(id) == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _personRepository.Delete(id);
        }
    }
}