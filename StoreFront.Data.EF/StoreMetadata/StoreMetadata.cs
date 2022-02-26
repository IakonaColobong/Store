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
    public class AuthorTablesMetadata
    {
        public int AuthorID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
    [MetadataType(typeof(AuthorTablesMetadata))]
    public partial class AuthorTables { } 
    #endregion


    #region BooksTableMetadata
    public class BooksTableMetadata
    {

        public int BookID { get; set; }
        public string BooksTitle { get; set; }
        public int GenreID { get; set; }
        public int AuthorID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> UnitsSold { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public Nullable<int> Publisher { get; set; }
        public int StockID { get; set; }
        public Nullable<int> CategoryID { get; set; }

        public virtual AuthorTable AuthorTable { get; set; }
        public virtual Category Category { get; set; }
        public virtual GenreIDTable GenreIDTable { get; set; }
        public virtual Stock Stock { get; set; }
    }

    [MetadataType(typeof(BooksTableMetadata))]
    public partial class BooksTable { }

    #endregion



    #region CategoryMetadata


    public class CategoryMetadata
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { } 
    #endregion


    #region EmployeeMetadata
    public class EmployeeMetadata
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<int> DirectReportID { get; set; }


    }
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { } 
    #endregion


    #region GenreMetadata
    public class GenreIDMetadata
    {
        public int GenreID { get; set; }
        public string GenreType { get; set; }

    }

    [MetadataType(typeof(GenreIDMetadata))]
    public partial class GenreID { } 
    #endregion



    #region StockMetadata
    public class StockMetadata
    {

        public int StockID { get; set; }
        public string StockType { get; set; }

    }

    [MetadataType(typeof(StockMetadata))]
    public partial class Stock { } 
    #endregion
}
