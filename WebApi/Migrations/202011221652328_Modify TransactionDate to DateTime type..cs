namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTransactionDatetoDateTimetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceDataTransactions", "TransactionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceDataTransactions", "TransactionDate", c => c.String(maxLength: 50));
        }
    }
}
