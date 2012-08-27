using System.Collections.Generic;
using System.Linq;
using Hal.PlayAround.Models;

namespace Hal.PlayAround.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person Get(int id);
        void Delete(int id);
        void Insert(Person person);
        void Update(Person person);
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> _people;

        public PersonRepository()
        {
            _people = new List<Person>();
        }

        public IEnumerable<Person> GetAll()
        {
            return _people;
        }

        public Person Get(int id)
        {
            return _people.SingleOrDefault(p => p.Id == id);
        }

        public void Delete(int id)
        {
            _people.Remove(_people.Single(p => p.Id == id));
        }

        public void Insert(Person person)
        {
            person.Id = _people.Count == 0 ? 1 : _people.Max(p => p.Id) + 1;
            _people.Add(person);
        }

        public void Update(Person person)
        {
            var index = _people.IndexOf(_people.Single(p => p.Id == person.Id));
            _people[index] = person;
        }
    }
}