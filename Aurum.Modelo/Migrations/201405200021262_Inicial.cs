namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carteira",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Tipo = c.Int(nullable: false),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        DiaVencimentoFatura = c.Int(),
                        CodCarteiraMae = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Movimentacao",
                c => new
                    {
                        Codigo = c.Long(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Consolidado = c.Boolean(nullable: false),
                        MesAno = c.DateTime(nullable: false),
                        Valor = c.Int(nullable: false),
                        NumeroParcela = c.Short(nullable: false),
                        TotalParcelas = c.Short(nullable: false),
                        Observacao = c.String(),
                        Carteira_Codigo = c.Int(),
                        Categoria_Codigo = c.Int(),
                        Descricao_Codigo = c.Int(),
                        PrimeiraParcela_Codigo = c.Long(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Carteira", t => t.Carteira_Codigo)
                .ForeignKey("dbo.Categoria", t => t.Categoria_Codigo)
                .ForeignKey("dbo.MovimentacaoDescricao", t => t.Descricao_Codigo)
                .ForeignKey("dbo.Movimentacao", t => t.PrimeiraParcela_Codigo)
                .Index(t => t.Carteira_Codigo)
                .Index(t => t.Categoria_Codigo)
                .Index(t => t.Descricao_Codigo)
                .Index(t => t.PrimeiraParcela_Codigo);
            
            CreateTable(
                "dbo.MovimentacaoDescricao",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Objetivo",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Concluido = c.Boolean(nullable: false),
                        Valor = c.Int(nullable: false),
                        Carteira_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Carteira", t => t.Carteira_Codigo)
                .Index(t => t.Carteira_Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Objetivo", "Carteira_Codigo", "dbo.Carteira");
            DropForeignKey("dbo.Movimentacao", "PrimeiraParcela_Codigo", "dbo.Movimentacao");
            DropForeignKey("dbo.Movimentacao", "Descricao_Codigo", "dbo.MovimentacaoDescricao");
            DropForeignKey("dbo.Movimentacao", "Categoria_Codigo", "dbo.Categoria");
            DropForeignKey("dbo.Movimentacao", "Carteira_Codigo", "dbo.Carteira");
            DropIndex("dbo.Objetivo", new[] { "Carteira_Codigo" });
            DropIndex("dbo.Movimentacao", new[] { "PrimeiraParcela_Codigo" });
            DropIndex("dbo.Movimentacao", new[] { "Descricao_Codigo" });
            DropIndex("dbo.Movimentacao", new[] { "Categoria_Codigo" });
            DropIndex("dbo.Movimentacao", new[] { "Carteira_Codigo" });
            DropTable("dbo.Objetivo");
            DropTable("dbo.MovimentacaoDescricao");
            DropTable("dbo.Movimentacao");
            DropTable("dbo.Categoria");
            DropTable("dbo.Carteira");
        }
    }
}
