using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hal.PlayAround.Models;
using Hal.PlayAround.Repositories;
using WebApi.Hal.Attributes;
using WebApi.Hal.Dtos;
using WebApi.Hal.Interfaces;

namespace Hal.PlayAround.Controllers
{
    public class PersonController : ApiController, IHalAwareController
    {
        public static class Resource
        {
            public const string Self = WebApi.Hal.Hal.Resource.Self;
        }
        public Link GetLinkForResource(string resourceId, object o)
        {
            var p = o as Person;
            if (p == null)
                return null;

            switch (resourceId)
            {
                case Resource.Self:
                    return new Link(resourceId, Url.Link("Api", new {Controller = "Person", p.Id}));
            }
            return null;
        }
        
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET /person
        [LinkedResource(Resource.Self)]
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
            return Request.CreateResponse(HttpStatusCode.Created, person);
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