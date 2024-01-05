using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;
using TestCase2PHE.Repository;

namespace TestCase2PHE.Services
{
    public class VendorServices
    {
        private readonly VendorRepository _vendorRepository;
    
        public VendorServices(PHEDbContext context)
        {
            _vendorRepository = new VendorRepository(context);
        }
        public Vendor CreateVendor(Vendor vendorDto)
        {
            var newVendor = new Vendor
            {
                Guid = Guid.NewGuid().ToString(),
                BusinessField= vendorDto.BusinessField,
                CompanyType= vendorDto.CompanyType,
                CompanyGuid= vendorDto.CompanyGuid,
            };

            var createdVendor = _vendorRepository.Add(newVendor);

            if (createdVendor == null) return null;

            var createdVendorDto = new Vendor
            {
                Guid = createdVendor.Guid,
                BusinessField= vendorDto.BusinessField,
                CompanyType= vendorDto.CompanyType,
                CompanyGuid= vendorDto.CompanyGuid,
            };

            return createdVendorDto;
        }

        public Vendor GetVendorByGuid(string guid)
        {
            var vendor = _vendorRepository.GetByGuid(guid);

            if (vendor != null)
            {
                return vendor;
            }

            return null;
        }

        public IEnumerable<Vendor> GetAllVendor()
        {
            return _vendorRepository.GetAll();
        }

        // UPDATE
        public void UpdateVendor(Vendor updatedVendor)
        {
            _vendorRepository.Update(updatedVendor);
        }

        // DELETE

        public void DeleteVendor(string guid)
        {
            try
            {
                var vendor = _vendorRepository.GetByGuid(guid);
                if (vendor != null)
                {
                    _vendorRepository.Delete(vendor);
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
