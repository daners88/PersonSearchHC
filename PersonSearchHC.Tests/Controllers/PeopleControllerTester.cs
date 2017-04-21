using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using PersonSearchHC;
using PersonSearchHC.Controllers;
using System.Web.Mvc;
using PersonSearchHC.Models;

namespace PersonSearchHC.Tests.Controllers
{
    [TestClass]
    public class PeopleControllerTest
    {

        private PersonDBContext db = new PersonDBContext();
        Person Person1 = null;
        Person Person2 = null;
        Person Person3 = null;
        Person Person4 = null;
        Person Person5 = null;

        List<Person> people = null;

        PeopleController controller = null;
        PeopleRepository repo = null;

        public PeopleControllerTest()
        {
            Person1 = new Person { ID = 1, FirstName = "test1", LastName = "test1", Interests = "NA" };
            Person2 = new Person { ID = 2, FirstName = "test2", LastName = "test2", Interests = "NA" };
            Person3 = new Person { ID = 3, FirstName = "test3", LastName = "test3", Interests = "NA" };
            Person4 = new Person { ID = 4, FirstName = "test4", LastName = "test4", Interests = "NA" };
            Person5 = new Person { ID = 5, FirstName = "test5", LastName = "test5", Interests = "NA" };

            people = new List<Person>
            {
                Person1,
                Person2,
                Person3,
                Person4
            };
            repo = new PeopleRepository();
            repo.people = people;
            controller = new PeopleController(repo);
        }


        [TestMethod]
        public void Index()
        {
            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            ViewResult result = controller.Details(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create2()
        {
            Person newPerson = new Person { ID = 7, FirstName = "new", LastName = "new", Interests = "NA" };

            controller.Create(newPerson);

            List<Person> peeps = controller.repo.people.ToList();

            CollectionAssert.Contains(peeps, newPerson);
        }

        [TestMethod]
        public void Edit()
        {
            Person editedPerson = new Person { ID = 1, FirstName = "new", LastName = "new", Interests = "NA" };

            controller.Edit(editedPerson);

            List<Person> peeps = controller.repo.people.ToList();

            CollectionAssert.Contains(peeps, editedPerson);
        }

        [TestMethod]
        public void Delete()
        {
            controller.DeleteConfirmed(1);

            List<Person> peeps = controller.repo.people.ToList();

            CollectionAssert.DoesNotContain(peeps, Person1);
        }

        [TestMethod]
        public void DBExists()
        {
            Assert.IsTrue(db.Database.Exists());
        }

    }
}
