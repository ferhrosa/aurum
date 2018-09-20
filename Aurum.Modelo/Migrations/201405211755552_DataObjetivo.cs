namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataObjetivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objetivo", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Objetivo", "Data");
        }
    }
}
