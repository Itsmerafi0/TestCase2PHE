using System.Collections.Generic;
using System.Linq;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Utilities;

namespace TestCase2PHE.Repository
{
    public class UserRepository : GeneralRepository<User>, IUserRepository
    {
        private readonly PHEDbContext _context;

        public UserRepository(PHEDbContext context) : base(context)
        {
            _context = context;
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<Role> GetRolesByUserGuid(string userGuid)
        {
            var user = _context.Users.FirstOrDefault(u => u.Guid == userGuid);

            if (user != null)
            {
                return _context.Roles.Where(role => role.Guid == user.RoleGuid).ToList();
            }

            return Enumerable.Empty<Role>();
        }

    }

}
