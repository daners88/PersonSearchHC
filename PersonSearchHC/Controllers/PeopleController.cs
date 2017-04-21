using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonSearchHC.Models;
using System.Threading.Tasks;

namespace PersonSearchHC.Controllers
{
    public class PeopleController : Controller
    {
        private PersonDBContext db { get; set; }
        public PeopleRepository repo { get; set; }

        public PeopleController(PeopleRepository r)
        {
            repo = r;
            db = new PersonDBContext();
        }
        public PeopleController()
        {
            db = new PersonDBContext();
            repo = new PeopleRepository();
        }
        // GET: People
        public ActionResult Index()
        {
            var sortedList = db.People.OrderBy(p => p.LastName).ToList();
            return View(sortedList);
        }

        [HttpGet]
        public ActionResult ShowNames(string name)
        {
            if (name != null && name != "")
            {
                name = name.ToUpper();
                List<Person> peeps = new List<Person>();

                foreach (Person p in db.People)
                {
                    bool foundFn = p.FirstName.ToUpper().StartsWith(name);
                    bool foundLn = p.LastName.ToUpper().StartsWith(name);
                    if (foundFn || foundLn)
                    {
                        peeps.Add(p);
                    }
                }
                var sortedList = peeps.OrderBy(p => p.LastName).ToList();
                return PartialView("_ShowNames", sortedList);
            }
            else
            {
                var sortedList = db.People.OrderBy(p => p.LastName).ToList();
                return PartialView("_ShowNames", sortedList);
            }
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,Address1,Address2,City,State,Zip,Interests")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                repo.addPerson(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,Address1,Address2,City,State,Zip,Interests")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                repo.editPerson(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            repo.removePerson(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
