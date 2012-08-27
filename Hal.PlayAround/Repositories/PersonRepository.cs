using System.Collections.Generic;
using Hal.PlayAround.Models;

namespace Hal.PlayAround.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
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
    }
}