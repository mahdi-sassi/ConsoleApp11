using System;

namespace ConsoleApp1
{
    public class Vendors : TableInfo
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string AlternativeCode { get; set; }
        public Guid? VendorsCategoriesID { get; set; }
        public VendorsCategories VendorsCategories { get; set; }
    }
}
