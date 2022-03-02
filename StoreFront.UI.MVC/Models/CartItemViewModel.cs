using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.Data.EF; //using data from project
using System.ComponentModel.DataAnnotations; // added for validation/metadata


namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {
        //properties
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        public BooksTable Product { get; set; }

        //FQ CTOR
        public CartItemViewModel(int qty, BooksTable product)
        {
            Qty = qty;
            Product = product;
        }

    }
}