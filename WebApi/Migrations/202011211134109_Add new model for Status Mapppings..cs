namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewmodelforStatusMapppings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CsvStatus = c.String(maxLength: 10),
                        XmlStatus = c.String(maxLength: 10),
                        Status = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StatusMappings");
        }
    }
}
