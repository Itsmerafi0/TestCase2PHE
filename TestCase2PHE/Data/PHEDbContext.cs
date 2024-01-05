using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestCase2PHE.Models;

namespace TestCase2PHE.Data
{
    public class PHEDbContext : DbContext
    {
        public PHEDbContext() : base("DefaultConnection") 
        { 
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasKey(e => e.Guid)
                .Property(e => e.Guid)
                .HasColumnName("guid")
                .HasColumnType("nvarchar")
                .HasMaxLength(36);

            modelBuilder.Entity<User>()
               .Property(e => e.Username)
               .HasColumnName("username")
               .HasColumnType("nvarchar")
               .HasMaxLength(50);

            modelBuilder.Entity<User>()
               .Property(e => e.Password)
               .HasColumnName("password")
               .HasColumnType("nvarchar")
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
               .Property(e => e.RoleGuid)
               .HasColumnName("role_guid")
               .HasColumnType("nvarchar")
               .HasMaxLength(36);
            modelBuilder.Entity<User>()
               .Property(e => e.CompanyGuid)
               .HasColumnName("company_guid")
               .HasColumnType("nvarchar")
               .HasMaxLength(36);

            modelBuilder.Entity<Role>()
                .HasKey(e => e.Guid)
                .Property(e => e.Guid)
                .HasColumnName("guid")
                .HasColumnType("nvarchar")
                .HasMaxLength(36);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            modelBuilder.Entity<Company>()
                 .HasKey(e => e.Guid)
                .Property(e => e.Guid)
                .HasColumnName("guid")
                .HasColumnType("nvarchar")
                .HasMaxLength(36);

            modelBuilder.Entity<Company>()
               .Property(e => e.Name)
               .HasColumnName("name")
               .HasColumnType("nvarchar")
               .HasMaxLength(50);

            modelBuilder.Entity<Company>()
               .Property(e => e.Email)
               .HasColumnName("email")
               .HasColumnType("nvarchar")
               .HasMaxLength(50);

            modelBuilder.Entity<Company>()
               .Property(e => e.PhoneNumber)
               .HasColumnName("phone_number")
               .HasColumnType("nvarchar")
               .HasMaxLength(13);

            modelBuilder.Entity<Company>()
               .Property(e => e.CompanyPhoto)
               .HasColumnName("company_photo")
               .HasColumnType("varbinary");

            modelBuilder.Entity<Company>()
               .Property(e => e.IsApproved)
               .HasColumnName("is_approved")
               .HasColumnType("nvarchar")
               .HasMaxLength(50);

            modelBuilder.Entity<Vendor>()
                 .HasKey(e => e.Guid)
                .Property(e => e.Guid)
                .HasColumnName("guid")
                .HasColumnType("nvarchar")
                .HasMaxLength(36);

            modelBuilder.Entity<Vendor>()
                .Property(e => e.BusinessField)
                .HasColumnName("business_field")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            modelBuilder.Entity<Vendor>()
               .Property(e => e.CompanyType)
               .HasColumnName("company_type")
               .HasColumnType("nvarchar")
               .HasMaxLength(50);

            modelBuilder.Entity<Vendor>()
               .Property(e => e.CompanyGuid)
               .HasColumnName("company_guid")
               .HasColumnType("nvarchar")
               .HasMaxLength(36);

            modelBuilder.Entity<Approval>()
                  .HasKey(e => e.Guid)
                .Property(e => e.Guid)
                .HasColumnName("guid")
                .HasColumnType("nvarchar")
                .HasMaxLength(36);

            modelBuilder.Entity<Approval>()
             .Property(e => e.CompanyGuid)
             .HasColumnName("company_guid")
             .HasColumnType("nvarchar")
             .HasMaxLength(36);

            modelBuilder.Entity<Role>()
                .HasMany(d => d.UserRole)
                .WithMany(p => p.RoleUser);

            modelBuilder.Entity<User>()
                .HasMany(d => d.CompanyUser)
                .WithMany(p => p.UserCompany);

            modelBuilder.Entity<Company>()
                .HasMany(d => d.VendorCompany)
                .WithMany(p => p.CompanyVendor);

            modelBuilder.Entity<Company>()
                .HasMany(d => d.ApprovalCompany)
                .WithMany(p => p.CompanyApproval);
        }
    }
}