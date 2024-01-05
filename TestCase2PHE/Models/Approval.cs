using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestCase2PHE.Models
{
    [Table("tr_approval")]
    public class Approval
    {
        [Key]
        public string Guid { get; set; }
        public string CompanyGuid { get; set; }

        public virtual ICollection<Company> CompanyApproval { get; set; }

    }
}