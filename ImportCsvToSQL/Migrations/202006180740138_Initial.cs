namespace ImportCsvToSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Payroll_Number = c.String(),
                    Forenames = c.String(maxLength: 100),
                    Surname = c.String(maxLength: 50),
                    Date_of_Birth = c.DateTime(nullable: false),
                    Telephone = c.String(maxLength: 50),
                    Mobile = c.String(maxLength: 50),
                    Address = c.String(maxLength: 100),
                    Address_2 = c.String(maxLength: 50),
                    Postcode = c.String(maxLength: 25),
                    EMail_Home = c.String(maxLength: 100),
                    Start_Date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
