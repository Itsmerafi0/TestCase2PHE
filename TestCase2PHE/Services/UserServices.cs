using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Repository;
using TestCase2PHE.Utilities;

namespace TestCase2PHE.Services
{
    
    public class UserServices
    {
        private readonly UserRepository _userRepository;
        private readonly PHEDbContext _context;
        private readonly RoleRepository _roleRepository;
        public UserServices(PHEDbContext context)
        {
            _userRepository = new UserRepository(context);
            _roleRepository = new RoleRepository(context); // Add this line
        }

        public RegistrationResult RegisterAccount(User registerDto)
        {
            try
            {
                var userRoleGuid = "e216ea4c-aec9-4373-b80d-c9f556f828a5";

                // Create a new GUID for the employee
                var userGuid = Guid.NewGuid().ToString();

                // Create an Employee entity
                var user = new User
                {
                    Guid = userGuid,
                    Username = registerDto.Username,
                    Password = Hashing.HashPassword(registerDto.Password),
                    CompanyGuid = registerDto.CompanyGuid,
                    RoleGuid = userRoleGuid,

                };

                // Save the employee to the database
                _userRepository.Add(user);

                return RegistrationResult.Success;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
                return RegistrationResult.UnknownError;
            }
        }

        public enum RegistrationResult
        {
            Success = 1,
            EmailAlreadyExists = 2,
            UnknownError = 0
        }

        public User GetUserByGuid(string guid)
        {
            var user = _userRepository.GetByGuid(guid);

            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        // UPDATE
        public void UpdateUser(User updatedUser)
        {
            _userRepository.Update(updatedUser);
        }

        // DELETE
        public void DeleteUser(string guid)
        {
            try
            {
                var user = _userRepository.GetByGuid(guid);
                if (user != null)
                {
                    _userRepository.Delete(user);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
                // You may want to throw an exception or return a specific result here
            }
        }

        public bool ValidatePassword(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);

            if (user != null)
            {
                return Hashing.ValidatePassword(password, user.Password);
            }

            return false;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }


        public Role GetRoleNameByGuid(string roleGuid)
        {
            Console.WriteLine($"RoleGuid: {roleGuid}");
            return _roleRepository.GetRoleByGuid(roleGuid);
        }

        public IEnumerable<string> GetRoles(string guid)
        {
            var roles = _userRepository.GetRolesByUserGuid(guid);

            if (roles.Any())
            {
                return roles.Select(role => role.Name);
            }

            return Enumerable.Empty<string>();
        }

    }
}