namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustesTamanhos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carteira", "Descricao", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Categoria", "Descricao", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.MovimentacaoDescricao", "Descricao", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Objetivo", "Descricao", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Objetivo", "Descricao", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.MovimentacaoDescricao", "Descricao", c => c.String());
            AlterColumn("dbo.Categoria", "Descricao", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Carteira", "Descricao", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
