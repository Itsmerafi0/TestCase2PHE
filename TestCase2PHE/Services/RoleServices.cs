using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Repository;

namespace TestCase2PHE.Services
{
    public class RoleServices
    {
        private readonly RoleRepository _roleRepository;

        public RoleServices(PHEDbContext context)
        {
            _roleRepository = new RoleRepository(context);
        }

        public Role CreateRole(Role roleDto)
        {
            var newRole = new Role
            {
                Guid = Guid.NewGuid().ToString(),
                Name = roleDto.Name,
                // Populate other properties from roleDto
            };

            var createdRole = _roleRepository.Add(newRole);

            if (createdRole == null) return null;

            var createdRoleDto = new Role
            {
                Guid = createdRole.Guid,
                Name = createdRole.Name,
                // Populate other properties from roleDto
            };

            return createdRoleDto;
        }

        public Role GetRoleByGuid(string guid)
        {
            var role = _roleRepository.GetByGuid(guid);

            return role;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAll();
        }

        // UPDATE
        public void UpdateRole(Role updatedRole)
        {
            _roleRepository.Update(updatedRole);
        }

        // DELETE
        public void DeleteRole(string guid)
        {
            try
            {
                var role = _roleRepository.GetByGuid(guid);
                if (role != null)
                {
                    _roleRepository.Delete(role);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
                // You may want to throw an exception or return a specific result here
            }
        }
    }
}