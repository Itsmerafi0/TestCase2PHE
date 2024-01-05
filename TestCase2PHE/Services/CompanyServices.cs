using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Repository;
using TestCase2PHE.Utilities;

namespace TestCase2PHE.Services
{
    public class CompanyServices
    {
        private readonly CompanyRepository _companyRepository;
        private readonly PHEDbContext _context;

        public CompanyServices(PHEDbContext context)
        {
            _companyRepository = new CompanyRepository(context);
            _context = context;

        }

        public Company CreateCompany(Company companyDto)
        {
            var newCompany = new Company
            {
                Guid = Guid.NewGuid().ToString(),
                Name = companyDto.Name,
                Email = companyDto.Email,
                PhoneNumber = companyDto.PhoneNumber,
                CompanyPhoto = companyDto.CompanyPhoto,
                IsApproved = IsApprovedStatus.pending.ToString(), // Set default approval status to Pending
                // Populate other properties from companyDto
            };

            var createdCompany = _companyRepository.Add(newCompany);

            if (createdCompany == null) return null;

            var createdCompanyDto = new Company
            {
                Guid = createdCompany.Guid,
                Name = createdCompany.Name,
                Email = createdCompany.Email,
                PhoneNumber = createdCompany.PhoneNumber,
                CompanyPhoto = createdCompany.CompanyPhoto,
                IsApproved = IsApprovedStatus.pending.ToString(), // Set default approval status to Pending
                // Populate other properties from companyDto
            };

            return createdCompanyDto;
        }

        public Company GetCompanyByGuid(string guid)
        {
            var company = _companyRepository.GetByGuid(guid);

            return company;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAll();
        }


        // UPDATE
        public void UpdateCompany(Company updatedCompany)
        {
            _companyRepository.Update(updatedCompany);
        }

        // DELETE
        public void DeleteCompany(string guid)
        {
            try
            {
                var company = _companyRepository.GetByGuid(guid);
                if (company != null)
                {
                    _companyRepository.Delete(company);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
                // You may want to throw an exception or return a specific result here
            }
        }

        public int CompanyAccept(string guid)
        {
            using (var transactionScope = new TransactionScope())
            {
                var vendor = _companyRepository.GetByGuid(guid);
                if (vendor == null) return -1;
                if (vendor.IsApproved == IsApprovedStatus.proccess.ToString()) return -3;
                if (vendor.IsApproved == IsApprovedStatus.decline.ToString()) return -4;

                vendor.IsApproved = IsApprovedStatus.proccess.ToString();
                _companyRepository.Update(vendor);  // Remove the assignment here

                transactionScope.Complete();
                return 1;
            }
        }

        public int CompanyApprove(string guid)
        {
            using (var transactionScope = new TransactionScope())
            {
                var vendor = _companyRepository.GetByGuid(guid);
                if (vendor == null) return -1;
                if (vendor.IsApproved == IsApprovedStatus.approve.ToString()) return -3;
                if (vendor.IsApproved == IsApprovedStatus.decline.ToString()) return -4;

                vendor.IsApproved = IsApprovedStatus.approve.ToString();
                _companyRepository.Update(vendor);  // Remove the assignment here

                transactionScope.Complete();
                return 1;
            }
        }

        public IEnumerable<Company> GetPendingCompanies()
        {
            // Assuming your PHEDbContext is named _context
            using (var connection = _context.Database.Connection)
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetStatusPending";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var result = command.ExecuteReader())
                    {
                        var companies = new List<Company>();

                        while (result.Read())
                        {
                            var company = new Company
                            {
                                Guid = result["guid"].ToString(),
                                Name = result["name"].ToString(),
                                Email = result["email"].ToString(),
                                PhoneNumber = result["phone_number"].ToString(),
                                CompanyPhoto = (byte[])result["company_photo"],
                                IsApproved = result["is_approved"].ToString(),
                            };

                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }

        public IEnumerable<Company> GetProccessCompanies()
        {
            // Assuming your PHEDbContext is named _context
            using (var connection = _context.Database.Connection)
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetStatusProccess";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var result = command.ExecuteReader())
                    {
                        var companies = new List<Company>();

                        while (result.Read())
                        {
                            var company = new Company
                            {
                                Guid = result["guid"].ToString(),
                                Name = result["name"].ToString(),
                                Email = result["email"].ToString(),
                                PhoneNumber = result["phone_number"].ToString(),
                                IsApproved = result["is_approved"].ToString(),
                            };

                            // Check for DBNull before attempting to cast to byte[]
                            if (!(result["company_photo"] is DBNull) && result["company_photo"] != null)
                            {
                                company.CompanyPhoto = (byte[])result["company_photo"];
                            }

                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }

        public IEnumerable<Company> GetCompanies()
        {
            var companies = _context.Companys.ToList()
                .Select(c => new Company { Guid = c.Guid, Name = c.Name });

            return companies;
        }

    }
}