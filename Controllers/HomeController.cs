using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearnApp.Models;

namespace LearnApp.Controllers
{
    public class HomeController : Controller
    {
        private courseDBEntities db = new courseDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Table.ToList());
        }
        public ActionResult search(string formationGenre, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Table
                           orderby d.catégorie
                           select d.catégorie;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.formationGenre = new SelectList(GenreLst);
            var formation = from m in db.Table
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                formation = formation.Where(course => course.nomFormation.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(formationGenre))
            {
                formation = formation.Where(course => course.catégorie == formationGenre);
            }

            return View(formation.ToList());

        }
        public ActionResult informatique()
        {

            return View(db.Table.Where(course => course.catégorie.Contains("info")));
        }
        public ActionResult medecine()
        {

            return View(db.Table.Where(course => course.catégorie.Contains("medecine")));
        }
        public ActionResult sciences_humaines()
        {
            return View(db.Table.Where(course => course.catégorie.Contains("sc")));
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Table.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nomFormation,nomFormateur,duréeF,certifiée,descriptionF,catégorie")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Table.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Table.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Home/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nomFormation,nomFormateur,duréeF,certifiée,descriptionF,catégorie")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(course);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Table.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Table.Find(id);
            db.Table.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Contact(int? id)
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Home(int? id)
        {
           
            return View();
        }
        public ActionResult About(int? id)
        {

            return View();
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
