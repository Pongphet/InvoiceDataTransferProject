namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cretedb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceDataTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.String(maxLength: 50),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyCode = c.String(maxLength: 5),
                        TransactionDate = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InvoiceDataTransactions");
        }
    }
}
