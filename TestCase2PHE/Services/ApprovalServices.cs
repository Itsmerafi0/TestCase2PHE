using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Repository;

namespace TestCase2PHE.Services
{
    public class ApprovalServices
    {
        private readonly ApprovalRepository _approvalRepository;
        private readonly PHEDbContext _context;

        public ApprovalServices(PHEDbContext context)
        {
            _approvalRepository = new ApprovalRepository(context);
            _context = context;

        }

        public Approval CreateApproval(Approval approvalDto)
        {
            var newApproval = new Approval
            {
                Guid = Guid.NewGuid().ToString(),
                CompanyGuid = approvalDto.CompanyGuid,
                // Populate other properties from approvalDto
            };

            var createdApproval = _approvalRepository.Add(newApproval);

            if (createdApproval == null) return null;

            var createdApprovalDto = new Approval
            {
                Guid = createdApproval.Guid,
                CompanyGuid = createdApproval.CompanyGuid,
                // Populate other properties from approvalDto
            };

            return createdApprovalDto;
        }

        public Approval GetApprovalByGuid(string guid)
        {
            var approval = _approvalRepository.GetByGuid(guid);

            return approval;
        }

        public IEnumerable<Approval> GetAllApprovals()
        {
            return _approvalRepository.GetAll();
        }

        // UPDATE
        public void UpdateApproval(Approval updatedApproval)
        {
            _approvalRepository.Update(updatedApproval);
        }

        // DELETE
        public void DeleteApproval(string guid)
        {
            try
            {
                var approval = _approvalRepository.GetByGuid(guid);
                if (approval != null)
                {
                    _approvalRepository.Delete(approval);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine(ex.ToString());
                // You may want to throw an exception or return a specific result here
            }
        }
        public IEnumerable<ApprovalVM> GetTableApproval()
        {
            // Assuming your PHEDbContext is named _context
            using (var connection = _context.Database.Connection)
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "TableApproval";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var result = command.ExecuteReader())
                    {
                        var companies = new List<ApprovalVM>();

                        while (result.Read())
                        {
                            var company = new ApprovalVM
                            {
                                Guid = result["guid"].ToString(),
                                Name = result["name"].ToString(),
                                Email = result["email"].ToString(),
                                PhoneNumber = result["phone_number"].ToString(),
                                CompanyGuid = result["company_guid"].ToString(),
                                BusinessField = result["business_field"].ToString(),
                                CompanyType = result["company_type"].ToString(),
                            };

                            companies.Add(company);
                        }

                        return companies;
                    }
                }
            }
        }
    }
}