using System;
namespace ConsoleApp1
{
    public class VendorsCategories : TableInfo
    {
        public Guid? MainVendorsCategoriesID { get; set; }
        public VendorsCategories MainVendorsCategories { get; set; }
    }
}