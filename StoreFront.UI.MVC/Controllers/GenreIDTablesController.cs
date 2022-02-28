using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.Data.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class GenreIDTablesController : Controller
    {
        private StoreEntities db = new StoreEntities();

        // GET: GenreIDTables
        public ActionResult Index()
        {
            return View(db.GenreIDTables.ToList());
        }

        // GET: GenreIDTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreIDTable genreIDTable = db.GenreIDTables.Find(id);
            if (genreIDTable == null)
            {
                return HttpNotFound();
            }
            return View(genreIDTable);
        }

        [Authorize(Roles = "Admin")]
        // GET: GenreIDTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenreIDTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreID,GenreType")] GenreIDTable genreIDTable)
        {
            if (ModelState.IsValid)
            {
                db.GenreIDTables.Add(genreIDTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genreIDTable);
        }

        // GET: GenreIDTables/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreIDTable genreIDTable = db.GenreIDTables.Find(id);
            if (genreIDTable == null)
            {
                return HttpNotFound();
            }
            return View(genreIDTable);
        }

        // POST: GenreIDTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreID,GenreType")] GenreIDTable genreIDTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genreIDTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genreIDTable);
        }

        [Authorize(Roles = "Admin")]
        // GET: GenreIDTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreIDTable genreIDTable = db.GenreIDTables.Find(id);
            if (genreIDTable == null)
            {
                return HttpNotFound();
            }
            return View(genreIDTable);
        }
        [Authorize(Roles = "Admin")]
        // POST: GenreIDTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GenreIDTable genreIDTable = db.GenreIDTables.Find(id);
            db.GenreIDTables.Remove(genreIDTable);
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
