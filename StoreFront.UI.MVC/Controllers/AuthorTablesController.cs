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
    public class AuthorTablesController : Controller
    {
        private StoreEntities db = new StoreEntities();

        // GET: AuthorTables
        public ActionResult Index()
        {
            return View(db.AuthorTables.ToList());
        }

        // GET: AuthorTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorTable authorTable = db.AuthorTables.Find(id);
            if (authorTable == null)
            {
                return HttpNotFound();
            }
            return View(authorTable);
        }

        // GET: AuthorTables/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuthorID,FName,LName")] AuthorTable authorTable)
        {
            if (ModelState.IsValid)
            {
                db.AuthorTables.Add(authorTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authorTable);
        }

        // GET: AuthorTables/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorTable authorTable = db.AuthorTables.Find(id);
            if (authorTable == null)
            {
                return HttpNotFound();
            }
            return View(authorTable);
        }

        // POST: AuthorTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorID,FName,LName")] AuthorTable authorTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authorTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authorTable);
        }

        // GET: AuthorTables/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorTable authorTable = db.AuthorTables.Find(id);
            if (authorTable == null)
            {
                return HttpNotFound();
            }
            return View(authorTable);
        }

        // POST: AuthorTables/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuthorTable authorTable = db.AuthorTables.Find(id);
            db.AuthorTables.Remove(authorTable);
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
