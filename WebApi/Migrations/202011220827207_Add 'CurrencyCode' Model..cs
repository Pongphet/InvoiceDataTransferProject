namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrencyCodeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrencyCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(maxLength: 50),
                        Currency = c.String(maxLength: 10),
                        Code = c.String(maxLength: 5),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurrencyCodes");
        }
    }
}
