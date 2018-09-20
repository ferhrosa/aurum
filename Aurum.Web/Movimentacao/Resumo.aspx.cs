using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aurum.Dados.Entidades;
using Aurum.Dados.Excecoes;

namespace Aurum.Web
{
    public partial class Movimentacao_Resumo : Pagina
    {
        private const string CorValorPositivo = "#090";
        private const string CorValorNegativo = "#c00";

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.meses.MesAlterado += meses_MesAlterado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGrid();
            }
        }


        void meses_MesAlterado(object sender, EventArgs e)
        {
            CarregarGrid();
        }


        void CarregarGrid()
        {
            gvListaContas.DataSource = Movimento.ListarResumoConta(meses.Mes);
            gvListaContas.DataBind();

            gvListaCartao.DataSource = Movimento.ListarResumoCartao(meses.Mes);
            gvListaCartao.DataBind();

            gvListaCategoria.DataSource = Movimento.ListarResumoCategoria(meses.Mes);
            gvListaCategoria.DataBind();
        }

    }
}