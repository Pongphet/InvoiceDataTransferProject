namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixmaxlengthforCurrency : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CurrencyCodes", "Currency", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CurrencyCodes", "Currency", c => c.String(maxLength: 10));
        }
    }
}
