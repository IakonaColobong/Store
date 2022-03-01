using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.UI.MVC.Exceptions;

namespace StoreFront.UI.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        //Click 3rd button on Index (404 Error) and show what happens by default if page cannot be found

        // GET: Errors
        public ActionResult Index()//Errors Landing page
        {
            return View();
        }

        //Instructor Notes: Click 4th button on index
        public ActionResult Throw404()//we can manually throw 404 server error (like EF Scaffolding when no record is found)
        {
            return HttpNotFound();
        }

        //Click on 2nd button on index (Oh Pickles page)
        public ActionResult Unresolved()//basic "Default Custom Error Page" for unhandled errors
        {
            //You could redirect to this Custom Error page in an action on another controller using RedirectToAction("Unresolved", "Errors")
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        //The below will handle errors for more specific exception (run time errors)

        //Click 1st button on index (Specific Exception Error Page because of an exception that is built into framework)
        public ActionResult SampleError()//Generates an error so we can see how that resolves w/ different configs
        {
            //v1 - quick way to generate an error:
            //throw new Exception("Some error could be explained here on the quick for user or devs");
            //just an on the fly error message generated using the generic exception type.

            //v2 - a bit more organic - compiler is not tracking our variable values
            int x = 0;
            int y = 42;
            int z = y / x;
            //uses more specific, built in exception (DivideByZero)

            return View();
        }

        //Click last button on index
        public ActionResult DBTest()
        {
            //Simulates testing for DB connectivity before processing.
            //-If fail, throw the custom error for logging purposes. 
            //then catch it & redirect to a nice specific custom error page for this error.
            //-If succeed, "do the stuff"
            try
            {
                bool dbCheck = false;//toggle option for setting test to fail or succeed.

                if (dbCheck)
                {
                    return View();//db works - could do cool stuff with it now.
                }
                else
                {
                    //db failed - throw our custom exception for that event
                    throw new DBOfflineException();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("DBOffline");//handle error nicely: send them to a page for these errors
            }
        }

        public ActionResult DBOffline()
        {
            return View();
        }
    }
}