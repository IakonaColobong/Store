﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.Data.EF;
using StoreFront.UI.MVC.Utilities;
using PagedList; //used for paging features
using PagedList.Mvc;// brought in for paging features

namespace StoreFront.UI.MVC.Controllers
{
    public class BooksTablesController : Controller
    {
        private StoreEntities db = new StoreEntities();

        // GET: BooksTables
        public ActionResult Index()
        {
            var booksTables = db.BooksTables.Include(b => b.AuthorTable).Include(b => b.Category).Include(b => b.GenreIDTable).Include(b => b.Stock);
            return View(booksTables.ToList());
        }

        // GET: BooksTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return HttpNotFound();
            }
            return View(booksTable);
        }

        // GET: BooksTables/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.AuthorTables, "AuthorID", "FName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.GenreID = new SelectList(db.GenreIDTables, "GenreID", "GenreType");
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockType");
            return View();
        }

        // POST: BooksTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,BooksTitle,GenreID,AuthorID,Price,UnitsSold,StockID,BoookImage")] BooksTable booksTable, HttpPostedFileBase bookCover)
        {
            if (ModelState.IsValid)
            {

                //image upload...this areea is dense with code...annotation added for clarity
                string file = "NoImage.png";

                if (bookCover != null)
                {
                    file = bookCover.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", "wepb" };


                    //checks uploaded files for acceptable extensions and size
                    if (goodExts.Contains(ext.ToLower()) && bookCover.ContentLength <= 4194304)
                    {
                        //create a new file name using a GUID (Globally Unique Identifier)
                        file = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/imgstore/books/"); //this is where the images will be saved
                        Image convertedImage = Image.FromStream(bookCover.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                       
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                    }
                }
                booksTable.BoookImage = file;


                db.BooksTables.Add(booksTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.AuthorTables, "AuthorID", "FName", booksTable.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", booksTable.CategoryID);
            ViewBag.GenreID = new SelectList(db.GenreIDTables, "GenreID", "GenreType", booksTable.GenreID);
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockType", booksTable.StockID);
            return View(booksTable);
        }

        // GET: BooksTables/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.AuthorTables, "AuthorID", "FName", booksTable.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", booksTable.CategoryID);
            ViewBag.GenreID = new SelectList(db.GenreIDTables, "GenreID", "GenreType", booksTable.GenreID);
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockType", booksTable.StockID);
            return View(booksTable);
        }

     
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,BooksTitle,GenreID,AuthorID,Price,UnitsSold,PublishDate,Publisher,StockID,CategoryID,BoookImage")] BooksTable booksTable, HttpPostedFileBase bookCover)
        {
            if (ModelState.IsValid)
            {
                string file = booksTable.BoookImage;
                #region File Upload
                if (bookCover != null)
                {
                    //get file name
                     file = bookCover.FileName;

                    //get the file extension
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //create a list of good extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif", ".webp" };

                    //checks uploaded files for acceptable extensions and size
                    if (goodExts.Contains(ext.ToLower()) && bookCover.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        string savePath = Server.MapPath("~/Content/imgstore/books/");

                        //convert to type image so dimensions can be altered by Image utility
                        Image convertedImage = Image.FromStream(bookCover.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        if (booksTable.BoookImage != null && booksTable.BoookImage != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/imgstore/books/");
                            ImageUtility.Delete(path, booksTable.BoookImage);
                        }

                        booksTable.BoookImage = file; //this updates the book obj before it is saved to the DB with the latest file name
                    }
                }
                #endregion
                db.Entry(booksTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.AuthorTables, "AuthorID", "FName", booksTable.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", booksTable.CategoryID);
            ViewBag.GenreID = new SelectList(db.GenreIDTables, "GenreID", "GenreType", booksTable.GenreID);
            ViewBag.StockID = new SelectList(db.Stocks, "StockID", "StockType", booksTable.StockID);
            return View(booksTable);
        }

        // GET: BooksTables/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksTable booksTable = db.BooksTables.Find(id);
            if (booksTable == null)
            {
                return HttpNotFound();
            }
            return View(booksTable);
        }

        // POST: BooksTables/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksTable booksTable = db.BooksTables.Find(id);

            string path = Server.MapPath("~/Content/imgstore/books/");
            ImageUtility.Delete(path, booksTable.BoookImage);


            db.BooksTables.Remove(booksTable);
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
