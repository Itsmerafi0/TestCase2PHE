using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCase2PHE.Models;

namespace TestCase2PHE.Contracts
{
    public interface IRoleRepository : IGeneralRepository<Role>
    {
        Role GetRoleByGuid(string roleGuid);

    }
}
