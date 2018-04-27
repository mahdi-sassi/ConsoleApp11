using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    
    
    public class Table
    {
        public Guid ID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Deleted { get; set; }
        public Guid? DomainID { get; set; }
        public virtual Domains Domain { get; set; }
    }
    public class TableInfo : TableName
    {
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
    }
    public class TableName : Table
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
    public class TableNameShort : TableName
    {
        public string ShortNameEn { get; set; }
        public string ShortNameAr { get; set; }
    }
    public class TableInfoShort : TableNameShort
    {
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
    }
    public class Domains : Table
    {
    }
}
