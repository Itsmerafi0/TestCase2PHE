using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestCase2PHE.Models
{
    [Table("tb_vendor")]
    public class Vendor
    {
        [Key]
        public string Guid { get; set; }
        public string BusinessField { get; set; }
        public string CompanyType { get; set; }
        public string CompanyGuid { get; set; }

        public virtual ICollection<Company> CompanyVendor { get; set; }

    }
}
