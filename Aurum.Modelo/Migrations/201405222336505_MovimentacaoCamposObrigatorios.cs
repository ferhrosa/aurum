namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimentacaoCamposObrigatorios : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movimentacao", "Carteira_Codigo", "dbo.Carteira");
            DropForeignKey("dbo.Movimentacao", "Categoria_Codigo", "dbo.Categoria");
            DropIndex("dbo.Movimentacao", new[] { "Categoria_Codigo" });
            DropIndex("dbo.Movimentacao", new[] { "Carteira_Codigo" });
            AlterColumn("dbo.Movimentacao", "Categoria_Codigo", c => c.Int(nullable: false));
            AlterColumn("dbo.Movimentacao", "Carteira_Codigo", c => c.Int(nullable: false));
            AlterColumn("dbo.Movimentacao", "NumeroParcela", c => c.Short());
            AlterColumn("dbo.Movimentacao", "TotalParcelas", c => c.Short());
            CreateIndex("dbo.Movimentacao", "Categoria_Codigo");
            CreateIndex("dbo.Movimentacao", "Carteira_Codigo");
            AddForeignKey("dbo.Movimentacao", "Carteira_Codigo", "dbo.Carteira", "Codigo", cascadeDelete: false);
            AddForeignKey("dbo.Movimentacao", "Categoria_Codigo", "dbo.Categoria", "Codigo", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimentacao", "Categoria_Codigo", "dbo.Categoria");
            DropForeignKey("dbo.Movimentacao", "Carteira_Codigo", "dbo.Carteira");
            DropIndex("dbo.Movimentacao", new[] { "Carteira_Codigo" });
            DropIndex("dbo.Movimentacao", new[] { "Categoria_Codigo" });
            AlterColumn("dbo.Movimentacao", "TotalParcelas", c => c.Short(nullable: false));
            AlterColumn("dbo.Movimentacao", "NumeroParcela", c => c.Short(nullable: false));
            AlterColumn("dbo.Movimentacao", "Carteira_Codigo", c => c.Int());
            AlterColumn("dbo.Movimentacao", "Categoria_Codigo", c => c.Int());
            CreateIndex("dbo.Movimentacao", "Carteira_Codigo");
            CreateIndex("dbo.Movimentacao", "Categoria_Codigo");
            AddForeignKey("dbo.Movimentacao", "Categoria_Codigo", "dbo.Categoria", "Codigo");
            AddForeignKey("dbo.Movimentacao", "Carteira_Codigo", "dbo.Carteira", "Codigo");
        }
    }
}
