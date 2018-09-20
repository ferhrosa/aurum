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
    public partial class Movimentacao_Conferencia : Pagina
    {
        private const string CorValorPositivo = "#090";
        private const string CorValorNegativo = "#c00";


        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.btnMostrar.Click += btnMostrar_Click;

            this.txtDataInicio.Attributes["readonly"] = "readonly";
            this.txtDataFim.Attributes["readonly"] = "readonly";

            this.repeater.ItemDataBound += repeater_ItemDataBound;

            this.lblErro.Text = string.Empty;
        }


        void btnMostrar_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
            {
                var dataItem = (KeyValuePair<DateTime, List<Movimento>>)e.Item.DataItem;

                var lblMesAno = e.Item.FindControl("lblMesAno") as Label;
                var gridView = e.Item.FindControl("gridView") as Aurum.Web.Controles.GridView;

                lblMesAno.Text = dataItem.Key.ToString("MMMM/yyyy");

                gridView.DataSource = dataItem.Value;
                gridView.DataBind();
            }
        }


        void CarregarGrid()
        {
            repeater.DataSource = Movimento.ListarConferencia(DateTime.Parse(txtDataInicio.Text), DateTime.Parse(txtDataFim.Text));
            repeater.DataBind();
        }

    }
}