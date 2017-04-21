using PersonSearchHC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonSearchHC
{
    public class PeopleRepository
    {
        public PeopleRepository()
        {
            people = new List<Person>();
        }

        public List<Person> getPeople()
        {
            return people;
        }

        public void addPerson(Person p)
        {
            people.Add(p);
        }

        public void editPerson(Person p)
        {
            List<Person> temp = people.Where(pe => pe.ID != p.ID).ToList();

            people.Clear();

            people = temp.ToList();
            people.Add(p);
        }

        public void removePerson(Person p)
        {
            List<Person> temp = people.Where(pe => pe.ID != p.ID).ToList();

            people.Clear();

            people = temp.ToList();
        }

        public List<Person> people { get; set; }
    }
}
