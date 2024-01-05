using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;

namespace TestCase2PHE.Repository
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        private readonly PHEDbContext _context;
        public RoleRepository(PHEDbContext context) : base(context) 
        {
            _context = context; 
        }

        public Role GetRoleByGuid(string roleGuid)
        {

            return _context.Roles.FirstOrDefault(r => r.Name == roleGuid);
        }
    }

}