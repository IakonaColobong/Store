using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.UI.MVC;//brought in for access
using System.ComponentModel.DataAnnotations;//brought in for annotation ability

namespace StoreFront.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Your name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Your E-mail is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "* A subject heading is required")]
        public string Subject { get; set; }

        [UIHint("MultiLineText")]
        [Required(ErrorMessage = "* A message is required")]
        public string Message { get; set; }
    }
}