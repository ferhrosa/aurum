using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aurum.Web.Controles
{
    public class GridView : System.Web.UI.WebControls.GridView
    {
        /// <summary>Cor verde usada para exibição de valores positivos.</summary>
        public const string CorValorPositivo = "#090";
        /// <summary>Cor vermelha usada para exibição de valores negativos.</summary>
        public const string CorValorNegativo = "#c00";


        public bool ExibirSelecao { get; set; }

        public Button BotaoAlterar { get; set; }
        public Button BotaoExcluir { get; set; }

        public string CampoValor { get; set; }
        public string LabelValor { get; set; }


        public GridView()
            : base()
        {
            this.CssClass = "grid";
            this.ExibirSelecao = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            MontarColunaSelecao();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if ( this.ExibirSelecao )
            {
                this.CssClass += " grid-selecionavel";
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            // Executa o método da classe base para que continue executando
            // os eventos "OnRowDataBound" das páginas.
            base.OnRowDataBound(e);

            if (
                // Somente continua se for informado o campo de valor.
                !String.IsNullOrWhiteSpace(CampoValor)
                // Somente continua se for informado o Label para exibição de valor.
                && !String.IsNullOrWhiteSpace(LabelValor)
                // Somente continua se for linha de exibição de dados.
                && e.Row.RowType == DataControlRowType.DataRow )
            {
                // Carrega o valor (em número inteiro) do campo de nome informado em propriedade.
                var valor = (int)DataBinder.Eval(e.Row.DataItem, CampoValor);

                // Carrega a instância do Label para exibição do valor.
                var lblValor = (Label)e.Row.FindControl(LabelValor);

                // Se for valor positivo, altera a cor para verde.
                if ( valor > 0 )
                    lblValor.Style[HtmlTextWriterStyle.Color] = CorValorPositivo;
                // Se for valor negativo, altera a cor para vermelho.
                else if ( valor < 0 )
                    lblValor.Style[HtmlTextWriterStyle.Color] = CorValorNegativo;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.UseAccessibleHeader = true;

            if ( this.Rows.Count > 0 )
            {
                this.HeaderRow.TableSection = TableRowSection.TableHeader;
                this.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if ( Visible )
            {
                var script = String.Format(
                        "var {0} = new Grid('{1}', {2}, '{3}', '{4}');",
                        this.ID,
                        this.ClientID,
                        this.ExibirSelecao.ToString().ToLower(),
                        (this.BotaoAlterar == null ? String.Empty : this.BotaoAlterar.ClientID),
                        (this.BotaoExcluir == null ? String.Empty : this.BotaoExcluir.ClientID));

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), String.Empty, script, true);
            }
        }

        private void MontarColunaSelecao()
        {
            if ( this.ExibirSelecao )
            {
                var coluna = new TemplateField();
                coluna.HeaderTemplate = new CheckBoxTemplate("chkTodos");
                coluna.ItemTemplate = new CheckBoxTemplate("chkSelecionar");

                this.Columns.Insert(0, coluna);

                this.CssClass += " grid-selecionavel";
            }
        }


        #region Carregar selecionados

        dynamic _selecionados = null;

        public T CarregarSelecionado<T>()
        {
            return CarregarSelecionados<T>().FirstOrDefault();
        }

        public List<T> CarregarSelecionados<T>()
        {
            if ( _selecionados == null )
            {
                this._selecionados = new List<T>();

                if ( ExibirSelecao )
                {
                    foreach ( GridViewRow item in this.Rows )
                    {
                        if ( item.RowType == DataControlRowType.DataRow )
                        {
                            var chkSelecionar = item.FindControl("chkSelecionar") as CheckBox;

                            if ( chkSelecionar.Checked )
                                this._selecionados.Add((T)this.DataKeys[item.RowIndex].Value);
                        }
                    }
                }
            }

            return this._selecionados;
        }

        #endregion Carregar selecionados


        /// <summary>
        /// Template que contém apenas um controle CheckBox, com o ID informado na construção.
        /// </summary>
        private class CheckBoxTemplate : ITemplate
        {
            /// <summary>
            /// Armazena o ID que deve ter os controles CheckBox criados.
            /// </summary>
            string id;

            /// <summary>
            /// Cria o template informando o ID do controle CheckBox.
            /// </summary>
            /// <param name="id"></param>
            public CheckBoxTemplate(string id)
            {
                this.id = id;
            }

            /// <summary>
            /// Adiciona os controles do template ao container.
            /// </summary>
            /// <param name="container"></param>
            public void InstantiateIn(Control container)
            {
                container.Controls.Add(new CheckBox() { ID = this.id });
            }
        }
    }
}