namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObjetivoCarteiraObrigatoria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Objetivo", "Carteira_Codigo", "dbo.Carteira");
            DropIndex("dbo.Objetivo", new[] { "Carteira_Codigo" });
            AlterColumn("dbo.Objetivo", "Carteira_Codigo", c => c.Int(nullable: false));
            CreateIndex("dbo.Objetivo", "Carteira_Codigo");
            AddForeignKey("dbo.Objetivo", "Carteira_Codigo", "dbo.Carteira", "Codigo", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Objetivo", "Carteira_Codigo", "dbo.Carteira");
            DropIndex("dbo.Objetivo", new[] { "Carteira_Codigo" });
            AlterColumn("dbo.Objetivo", "Carteira_Codigo", c => c.Int());
            CreateIndex("dbo.Objetivo", "Carteira_Codigo");
            AddForeignKey("dbo.Objetivo", "Carteira_Codigo", "dbo.Carteira", "Codigo");
        }
    }
}
