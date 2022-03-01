using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreFront.UI.MVC.Exceptions
{
    public class DBOfflineException : Exception
    {
        public DBOfflineException()
        {

        }

        public DBOfflineException(string message) : base(message)
        {

        }

        public DBOfflineException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}