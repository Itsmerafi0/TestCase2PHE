namespace TestCase2PHE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tr_approval",
                c => new
                    {
                        guid = c.String(nullable: false, maxLength: 36),
                        company_guid = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.tb_company",
                c => new
                    {
                        guid = c.String(nullable: false, maxLength: 36),
                        name = c.String(maxLength: 50),
                        email = c.String(maxLength: 50),
                        phone_number = c.String(maxLength: 13),
                        company_photo = c.Binary(maxLength: 8000),
                        is_approved = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.tb_user",
                c => new
                    {
                        guid = c.String(nullable: false, maxLength: 36),
                        username = c.String(maxLength: 50),
                        password = c.String(maxLength: 255),
                        company_guid = c.String(maxLength: 36),
                        role_guid = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.tb_role",
                c => new
                    {
                        guid = c.String(nullable: false, maxLength: 36),
                        name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.tb_vendor",
                c => new
                    {
                        guid = c.String(nullable: false, maxLength: 36),
                        business_field = c.String(maxLength: 50),
                        company_type = c.String(maxLength: 50),
                        company_guid = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.guid);
            
            CreateTable(
                "dbo.CompanyApprovals",
                c => new
                    {
                        Company_Guid = c.String(nullable: false, maxLength: 36),
                        Approval_Guid = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => new { t.Company_Guid, t.Approval_Guid })
                .ForeignKey("dbo.tb_company", t => t.Company_Guid, cascadeDelete: true)
                .ForeignKey("dbo.tr_approval", t => t.Approval_Guid, cascadeDelete: true)
                .Index(t => t.Company_Guid)
                .Index(t => t.Approval_Guid);
            
            CreateTable(
                "dbo.UserCompanies",
                c => new
                    {
                        User_Guid = c.String(nullable: false, maxLength: 36),
                        Company_Guid = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => new { t.User_Guid, t.Company_Guid })
                .ForeignKey("dbo.tb_user", t => t.User_Guid, cascadeDelete: true)
                .ForeignKey("dbo.tb_company", t => t.Company_Guid, cascadeDelete: true)
                .Index(t => t.User_Guid)
                .Index(t => t.Company_Guid);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Guid = c.String(nullable: false, maxLength: 36),
                        User_Guid = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => new { t.Role_Guid, t.User_Guid })
                .ForeignKey("dbo.tb_role", t => t.Role_Guid, cascadeDelete: true)
                .ForeignKey("dbo.tb_user", t => t.User_Guid, cascadeDelete: true)
                .Index(t => t.Role_Guid)
                .Index(t => t.User_Guid);
            
            CreateTable(
                "dbo.CompanyVendors",
                c => new
                    {
                        Company_Guid = c.String(nullable: false, maxLength: 36),
                        Vendor_Guid = c.String(nullable: false, maxLength: 36),
                    })
                .PrimaryKey(t => new { t.Company_Guid, t.Vendor_Guid })
                .ForeignKey("dbo.tb_company", t => t.Company_Guid, cascadeDelete: true)
                .ForeignKey("dbo.tb_vendor", t => t.Vendor_Guid, cascadeDelete: true)
                .Index(t => t.Company_Guid)
                .Index(t => t.Vendor_Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyVendors", "Vendor_Guid", "dbo.tb_vendor");
            DropForeignKey("dbo.CompanyVendors", "Company_Guid", "dbo.tb_company");
            DropForeignKey("dbo.RoleUsers", "User_Guid", "dbo.tb_user");
            DropForeignKey("dbo.RoleUsers", "Role_Guid", "dbo.tb_role");
            DropForeignKey("dbo.UserCompanies", "Company_Guid", "dbo.tb_company");
            DropForeignKey("dbo.UserCompanies", "User_Guid", "dbo.tb_user");
            DropForeignKey("dbo.CompanyApprovals", "Approval_Guid", "dbo.tr_approval");
            DropForeignKey("dbo.CompanyApprovals", "Company_Guid", "dbo.tb_company");
            DropIndex("dbo.CompanyVendors", new[] { "Vendor_Guid" });
            DropIndex("dbo.CompanyVendors", new[] { "Company_Guid" });
            DropIndex("dbo.RoleUsers", new[] { "User_Guid" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Guid" });
            DropIndex("dbo.UserCompanies", new[] { "Company_Guid" });
            DropIndex("dbo.UserCompanies", new[] { "User_Guid" });
            DropIndex("dbo.CompanyApprovals", new[] { "Approval_Guid" });
            DropIndex("dbo.CompanyApprovals", new[] { "Company_Guid" });
            DropTable("dbo.CompanyVendors");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.UserCompanies");
            DropTable("dbo.CompanyApprovals");
            DropTable("dbo.tb_vendor");
            DropTable("dbo.tb_role");
            DropTable("dbo.tb_user");
            DropTable("dbo.tb_company");
            DropTable("dbo.tr_approval");
        }
    }
}
