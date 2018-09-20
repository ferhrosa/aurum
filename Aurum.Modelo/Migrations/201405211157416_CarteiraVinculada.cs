namespace Aurum.Modelo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarteiraVinculada : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carteira", "Descricao", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Categoria", "Descricao", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categoria", "Descricao", c => c.String());
            AlterColumn("dbo.Carteira", "Descricao", c => c.String());
        }
    }
}
