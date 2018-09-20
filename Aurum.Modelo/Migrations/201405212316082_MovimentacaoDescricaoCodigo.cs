namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovimentacaoDescricaoCodigo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movimentacao", "Descricao_Codigo", "dbo.MovimentacaoDescricao");
            DropIndex("dbo.Movimentacao", new[] { "Descricao_Codigo" });
            AlterColumn("dbo.Movimentacao", "Descricao_Codigo", c => c.Int(nullable: false));
            CreateIndex("dbo.Movimentacao", "Descricao_Codigo");
            AddForeignKey("dbo.Movimentacao", "Descricao_Codigo", "dbo.MovimentacaoDescricao", "Codigo", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimentacao", "Descricao_Codigo", "dbo.MovimentacaoDescricao");
            DropIndex("dbo.Movimentacao", new[] { "Descricao_Codigo" });
            AlterColumn("dbo.Movimentacao", "Descricao_Codigo", c => c.Int());
            CreateIndex("dbo.Movimentacao", "Descricao_Codigo");
            AddForeignKey("dbo.Movimentacao", "Descricao_Codigo", "dbo.MovimentacaoDescricao", "Codigo");
        }
    }
}
