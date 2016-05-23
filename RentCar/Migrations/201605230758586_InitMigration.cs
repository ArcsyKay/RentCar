namespace RentCar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Mark = c.String(),
                        Model = c.String(),
                        ProdYear = c.Int(nullable: false),
                        CarTransmission = c.String(),
                        Fuel = c.String(),
                        PictureLink = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        RentId = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        DateOfRentFrom = c.DateTime(nullable: false),
                        DateOfRentTo = c.DateTime(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserLogin = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rents", "UserId", "dbo.Users");
            DropForeignKey("dbo.Rents", "CarId", "dbo.Cars");
            DropIndex("dbo.Rents", new[] { "UserId" });
            DropIndex("dbo.Rents", new[] { "CarId" });
            DropTable("dbo.Users");
            DropTable("dbo.Rents");
            DropTable("dbo.Cars");
        }
    }
}
