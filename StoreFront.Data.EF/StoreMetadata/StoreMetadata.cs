using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data.EF//.StoreMetadata
{
    #region AuthorsTableMetadata
    public class AuthorTableMetadata
    {

        //public int AuthorID { get; set; }
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        public string FName { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        public string LName { get; set; }
    }
    [MetadataType(typeof(AuthorTableMetadata))]
    public partial class AuthorTable
    {
       // custom property
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return $"{FName} {LName}"; }
        }

    }


    #endregion
    
    #region BooksTableMetadata
    public class BooksTableMetadata
    {

        //public int BookID { get; set; }
        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Must be 50 characters or less")]
        public string BooksTitle { get; set; }


        //public int GenreID { get; set; }


        //public int AuthorID { get; set; }
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Range(0, double.MaxValue, ErrorMessage = "* Value must be a valid number, 0 or larger.")]
      
        public Nullable<decimal> Price { get; set; }


        //public Nullable<int> UnitsSold { get; set; }
      
        [Display(Name = "Publish Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-N/A-")]
        public Nullable<System.DateTime> PublishDate { get; set; }


        [Display(Name = "Book")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string BoookImage { get; set; }
        //public Nullable<int> Publisher { get; set; }


        //public int StockID { get; set; }


        //public Nullable<int> CategoryID { get; set; }

        //public virtual AuthorTable AuthorTable { get; set; }
        //public virtual Category Category { get; set; }
        //public virtual GenreIDTable GenreIDTable { get; set; }
        //public virtual Stock Stock { get; set; }
    }

    [MetadataType(typeof(BooksTableMetadata))]
    public partial class BooksTable { }

    #endregion
       
    #region CategoryMetadata
    public class CategoryMetadata
    {
        //public int CategoryID { get; set; }
        [Required(ErrorMessage = "Required Entry")]
        [StringLength(50, ErrorMessage = "Must be less than 50 characters.")]
        public string CategoryName { get; set; }
    }
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    #endregion
    
    #region EmployeeMetadata
    public class EmployeeMetadata
    {
        //public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Required Entry")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "20 character maximum")]
        public string FirstName { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required Entry")]
        [StringLength(20, ErrorMessage = "20 character maximum")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Employee Title")]
        [StringLength(30, ErrorMessage = "30 character maximum")]
        public string Title { get; set; }

        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-N/A-")]
        public Nullable<System.DateTime> HireDate { get; set; }

        //[Display(Name = "Reportable To")]

        //public Nullable<int> DirectReportID { get; set; }


    }
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }
    #endregion
    
    #region GenreMetadata
    public class GenreIDMetadata
    {
        //public int GenreID { get; set; }
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Display(Name = "Genre")]
        [StringLength(20, ErrorMessage = "20 character maximum")]
        public string GenreType { get; set; }

    }

    [MetadataType(typeof(GenreIDMetadata))]
    public partial class GenreID { }
    #endregion
       
    #region StockMetadata
    public class StockMetadata
    {
        [Display(Name = "In Stock?")]
        //public int StockID { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [StringLength(20, ErrorMessage = "Maximum of 20 characters")]
        public string StockType { get; set; }

    }

    [MetadataType(typeof(StockMetadata))]
    public partial class Stock { }
    #endregion
}
