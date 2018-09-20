namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Objetivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movimentacao", "Objetivo_Codigo", c => c.Int());
            AlterColumn("dbo.Objetivo", "Descricao", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Movimentacao", "Objetivo_Codigo");
            AddForeignKey("dbo.Movimentacao", "Objetivo_Codigo", "dbo.Objetivo", "Codigo");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimentacao", "Objetivo_Codigo", "dbo.Objetivo");
            DropIndex("dbo.Movimentacao", new[] { "Objetivo_Codigo" });
            AlterColumn("dbo.Objetivo", "Descricao", c => c.String());
            DropColumn("dbo.Movimentacao", "Objetivo_Codigo");
        }
    }
}
