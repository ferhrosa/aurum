namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carteiras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carteira", "CarteiraMae_Codigo", c => c.Int());
            CreateIndex("dbo.Carteira", "CarteiraMae_Codigo");
            AddForeignKey("dbo.Carteira", "CarteiraMae_Codigo", "dbo.Carteira", "Codigo");
            DropColumn("dbo.Carteira", "CodCarteiraMae");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carteira", "CodCarteiraMae", c => c.Int());
            DropForeignKey("dbo.Carteira", "CarteiraMae_Codigo", "dbo.Carteira");
            DropIndex("dbo.Carteira", new[] { "CarteiraMae_Codigo" });
            DropColumn("dbo.Carteira", "CarteiraMae_Codigo");
        }
    }
}
