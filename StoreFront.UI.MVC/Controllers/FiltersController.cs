using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StoreFront.Data.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class FiltersController : Controller
    {
        private StoreEntities db = new StoreEntities();

        // GET: Filters

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientSide()
        {
            var booksTables = db.BooksTables.Include(b => b.AuthorTable).Include(b => b.Category).Include(b => b.GenreIDTable).Include(b => b.Stock);
            return View(booksTables.ToList());
        }

        public ActionResult BooksQS(string searchFilter, int genreId = 0)//set genreId to 0 initially -> if a selection is changed, we will
                                                                         //use that value instead
        {
            //ViewBag object used to populate the Html.DropDownList in the View
            ViewBag.GenreID = new SelectList(db.BooksTables, "GenreID", "GenreName");

            //initial instance of the books collection.  This will be filtered down based on DDL selection and/or search filter
            var books = db.BooksTables.ToList();


            if (!String.IsNullOrEmpty(searchFilter))
            {
                books = db.BooksTables
                //.Where(b => b.BooksTitle.ToLower().Contains(searchFilter.ToLower()) ||
                //b.Description.ToLower().Contains(searchFilter.ToLower()))
               .Include(b => b.Stock).Include(b => b.GenreID).Include(b => b.AuthorID)
                .ToList();

            }

            if (genreId != 0)
            {
                books = books.Where(b => b.GenreID == genreId).ToList();
            }

            return View(books);

        }

        //The parameter below receives the default value of 1 if nothing is passed to it
        public ActionResult BooksMVCPaging(string searchString, int page = 1)
        {
            int pageSize = 5; //allows us to set how many records/objects are shown per "page"

            var books = db.BooksTables.OrderBy(b => b.BooksTitle).ToList(); //this is where our paged list collection will get its data from

            if (!String.IsNullOrEmpty(searchString))
            {
                books = (
                            from b in books
                            where b.BooksTitle.ToLower().Contains(searchString.ToLower())
                            select b
                    ).ToList();
            }

            ViewBag.SearchString = searchString;

            return View(books.ToPagedList(page, pageSize));
        }

       
    }
}
