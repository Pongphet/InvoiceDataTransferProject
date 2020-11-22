namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertDateTimetstringformatTransactioDatecolumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceDataTransactions", "TransactionDate", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceDataTransactions", "TransactionDate", c => c.DateTime(nullable: false));
        }
    }
}
