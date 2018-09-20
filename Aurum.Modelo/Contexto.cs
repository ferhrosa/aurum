using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aurum.Modelo
{
    public class Contexto : DbContext
    {

        #region Tabelas

        public DbSet<Carteira> Carteira { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }
        public DbSet<MovimentacaoDescricao> MovimentacaoDescricao { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }

        #endregion Tabelas

        const string ChaveInstanciaWeb = "Modelo.Contexto";

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static Contexto InstanciaWeb
        {
            get
            {
                if (HttpContext.Current.Items[ChaveInstanciaWeb] == null)
                {
                    var contexto = new Contexto();

                    HttpContext.Current.Items[ChaveInstanciaWeb] = contexto;

                    HttpContext.Current.DisposeOnPipelineCompleted(contexto);
                }

                return HttpContext.Current.Items[ChaveInstanciaWeb] as Contexto;
            }
        }

    }
}
