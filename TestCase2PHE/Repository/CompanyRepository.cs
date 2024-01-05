using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;

namespace TestCase2PHE.Repository
{
    public class CompanyRepository : GeneralRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(PHEDbContext context) : base(context) { }
    }
}