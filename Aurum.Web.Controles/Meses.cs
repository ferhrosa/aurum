using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aurum.Web.Controles
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Meses runat=server />")]
    public class Meses : Panel, IPostBackEventHandler
    {

        public int MesesAntesDepois
        {
            get
            {
                if ( ViewState["MesesAntesDepois"] != null )
                    return (int)ViewState["MesesAntesDepois"];

                // Se não houver valor salvo em ViewState, retorna 2, que é o valor padrão.
                return 2;
            }
            set
            {
                ViewState["MesesAntesDepois"] = value;
            }
        }

        DateTime _mes;
        public DateTime Mes
        {
            get
            {
                if ( _mes != DateTime.MinValue )
                    return _mes;

                return DateTime.Now;
            }
            protected set
            {
                this._mes = value;
                SaveControlState();
            }
        }


        public event EventHandler MesAlterado;


        protected override void OnInit(EventArgs e)
        {
            // Registra este controle para que os métodos SaveControlState e
            // LoadControlState (deste) sejam executados pela página.
            Page.RegisterRequiresControlState(this);

            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CarregarMesesAntesDepois();
        }

        protected override object SaveControlState()
        {
            return this._mes;
        }

        protected override void LoadControlState(object savedState)
        {
            this._mes = (DateTime)savedState;
        }


        protected override void CreateChildControls()
        {
            // Caso não haja mês selecionado, usa o mês atual.
            if ( Mes == DateTime.MinValue )
                Mes = DateTime.Now.Date;

            DateTime mesItem = Mes.AddMonths(-MesesAntesDepois);

            while ( mesItem <= Mes.AddMonths(MesesAntesDepois) )
            {
                // Cria o elemento que exibirá o mês sendo montado.
                var link = new Label();
                // O elemento fica habilitado somente se o controle (Meses) estiver habilitado.
                link.Enabled = this.Enabled;

                // Monta o texto do mês sendo criado.
                link.Text = mesItem.ToString("MMMM");
                // Torna maiúsculo o primeiro caractere do nome do mês.
                link.Text = link.Text.Substring(0, 1).ToUpper() + link.Text.Substring(1, link.Text.Length - 1);

                // Quando o ano do mês sendo montado não for o ano atual, é exibido o ano no final do texto.
                if ( mesItem.Year != DateTime.Now.Year )
                    link.Text += String.Format("<sub>/{0}</sub>", mesItem.Year.ToString());

                // Se o mês sendo montado for o atual, demarca com uma classe CSS, para destacá-lo.
                if ( mesItem.Year == DateTime.Now.Year && mesItem.Month == DateTime.Now.Month )
                    link.CssClass += " atual";

                // Se for o mês selecionado, demarca com uma classe CSS, para destacá-lo.
                if ( Mes.Year == mesItem.Year && Mes.Month == mesItem.Month )
                {
                    link.CssClass += " selecionado";
                }
                // Adiciona javascript para realização do PostBack, caso o componente esteja ativo.
                else if ( this.Enabled )
                {
                    link.Attributes["onclick"] = Page.ClientScript.GetPostBackEventReference(this, mesItem.ToString("yyyyMM"));
                }

                // Adiciona o mês à lista de controles filhos.
                this.Controls.Add(link);

                // Incrementa um mês para que seja montado o próximo.
                mesItem = mesItem.AddMonths(1);
            }
        }


        public void RaisePostBackEvent(string eventArgument)
        {
            int ano, mes;

            if ( !int.TryParse(eventArgument.Substring(0, 4), out ano) )
                throw new Exception(String.Format("{0}, {1}: Ano inválido no argumento: {2}", this.GetType(), this.ID, eventArgument));

            if ( !int.TryParse(eventArgument.Substring(4, 2), out mes) )
                throw new Exception(String.Format("{0}, {1}: Mês inválido no argumento: {2}", this.GetType(), this.ID, eventArgument));

            this.Mes = new DateTime(ano, mes, 1);

            MesAlterado(this, EventArgs.Empty);
        }


        private void CarregarMesesAntesDepois()
        {
            if ( ViewState["MesesAntesDepois"] == null )
            {
                var configMesesAntesDepois = ConfigurationManager.AppSettings["Meses_MesesAntesDepois"];

                if ( !String.IsNullOrWhiteSpace(configMesesAntesDepois) )
                {
                    int valor;
                    if ( int.TryParse(configMesesAntesDepois, out valor) )
                        this.MesesAntesDepois = valor;
                }
            }
        }

    }
}
