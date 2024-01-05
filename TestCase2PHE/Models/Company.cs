using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestCase2PHE.Models
{
    [Table("tb_company")]
    public class Company
    {
        [Key]
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] CompanyPhoto { get; set; }

        public string IsApproved { get; set; }
        public virtual ICollection<Vendor> VendorCompany { get; set; }
        public virtual ICollection<Approval> ApprovalCompany { get; set; }
        public virtual ICollection<User> UserCompany { get; set; }
    }
}