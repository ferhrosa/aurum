using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aurum.Dados.Entidades;
using Aurum.Dados.Excecoes;

namespace Aurum.Web
{
    public partial class Movimentacao_Movimentacao : Pagina
    {

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.meses.MesAlterado += meses_MesAlterado;

            this.btnInserir.Click += btnInserir_Click;
            this.btnAlterar.Click += btnAlterar_Click;
            this.btnExcluir.Click += btnExcluir_Click;
            this.btnSalvar.Click += btnSalvar_Click;
            this.btnCancelar.Click += btnCancelar_Click;

            this.btnReplicarOk.Click += btnReplicarOk_Click;

            this.btnConsolidar.Click += btnConsolidar_Click;

            this.txtData.Attributes["readonly"] = "readonly";
            this.rblTipo.Load += rblTipo_Load;

            this.gvLista.RowDataBound += gvLista_RowDataBound;

            this.lblErro.Text = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvLista.BotaoAlterar = this.btnAlterar;
            gvLista.BotaoExcluir = this.btnExcluir;

            if (!IsPostBack)
            {
                CarregarGrid();
                CarregarCombos();

                ManipularCampos(false);
            }
        }

        void rblTipo_Load(object sender, EventArgs e)
        {
            rblTipo.Items[0].Attributes.CssStyle.Add(HtmlTextWriterStyle.Color, Aurum.Web.Controles.GridView.CorValorPositivo);
            rblTipo.Items[1].Attributes.CssStyle.Add(HtmlTextWriterStyle.Color, Aurum.Web.Controles.GridView.CorValorNegativo);
        }


        void meses_MesAlterado(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        void btnInserir_Click(object sender, EventArgs e)
        {
            LimparCampos();

            ManipularCampos(true);
        }

        void btnAlterar_Click(object sender, EventArgs e)
        {
            PreencherCampos(gvLista.CarregarSelecionado<long>());

            ManipularCampos(true);
        }

        void btnCancelar_Click(object sender, EventArgs e)
        {
            ManipularCampos(false);
        }

        void btnSalvar_Click(object sender, EventArgs e)
        {
            var movimento = new Movimento();

            if (!String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                int codMovimento;

                if (int.TryParse(txtCodigo.Text, out codMovimento))
                    movimento.CodMovimento = codMovimento;
                else
                    throw new Exception("Código inválido.");
            }

            movimento.Data = DateTime.Parse(txtData.Text);
            movimento.Consolidado = chkConsolidado.Checked;
            movimento.MesAno = meses.Mes; //new DateTime(2013, 6, 1);
            movimento.ValorFormatado = txtValor.Text;

            if (rblTipo.SelectedValue == "-")
                movimento.Valor *= -1;

            int codCategoria;
            if (int.TryParse(this.ddlCategoria.SelectedValue, out codCategoria))
                movimento.CodCategoria = codCategoria;
            else
                throw new Exception(rfvCategoria.ErrorMessage);

            movimento.MovimentoDescricao.Descricao = txtDescricao.Text;

            int codMovimentoDescricao;
            if (int.TryParse(this.hfDescricao.Value, out codMovimentoDescricao))
                movimento.CodMovimentoDescricao = codMovimentoDescricao;

            int codConta;
            if (int.TryParse(this.ddlConta.SelectedValue, out codConta))
                movimento.CodConta = codConta;

            int codCartao;
            if (int.TryParse(this.ddlCartao.SelectedValue, out codCartao))
                movimento.CodCartao = codCartao;

            byte numeroParcela;
            if (byte.TryParse(this.txtNumeroParcela.Text, out numeroParcela))
                movimento.NumeroParcela = numeroParcela;

            byte totalParcelas;
            if (byte.TryParse(this.txtTotalParcelas.Text, out totalParcelas))
                movimento.TotalParcelas = totalParcelas;

            movimento.Observacao = (String.IsNullOrWhiteSpace(txtObservacao.Text) ? null : txtObservacao.Text);


            try
            {
                movimento.Salvar();

                CarregarGrid();
                ManipularCampos(false);
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }
        }

        void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                Movimento.Excluir(gvLista.CarregarSelecionados<long>());
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }

            CarregarGrid();
        }

        void btnReplicarOk_Click(object sender, EventArgs e)
        {
            var quantidadeMeses = 0;
            if (!int.TryParse(txtReplicarMeses.Text, out quantidadeMeses))
            {
                lblErro.Text = "A quantidade de meses informada é inválida";
                return;
            }

            var movimentacoesNaoReplicadas = new List<long>();

            foreach (var codMovimento in gvLista.CarregarSelecionados<long>())
            {
                try
                {
                    Movimento.Replicar(codMovimento, quantidadeMeses);
                }
                catch (ExReplicacaoMovimentoParcela ex)
                {
                    movimentacoesNaoReplicadas.Add(codMovimento);
                }
                catch (Exception ex)
                {
                    lblErro.Text = ex.Message;
                }
            }

            if (movimentacoesNaoReplicadas.Any())
            {
                var mensagem = new StringBuilder();
                mensagem.Append("As seguintes movimentações não puderam ser replicadas por fazerem parte de algum parcelamento: \n\n");

                var virgula = String.Empty;

                foreach (var codMovimento in movimentacoesNaoReplicadas)
                {
                    mensagem.Append(virgula);
                    mensagem.Append(codMovimento);
                    virgula = ", ";
                }

                mensagem.Append("');");

                Alert(mensagem);
            }

            Alert("Replicação realizada com sucesso.");

            txtReplicarMeses.Text = String.Empty;
            CarregarGrid();
        }

        void btnConsolidar_Click(object sender, EventArgs e)
        {
            try
            {
                Movimento.Consolidar(gvLista.CarregarSelecionados<long>());
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }

            CarregarGrid();
        }


        void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var movimento = (Movimento)e.Row.DataItem;

                var lblParcela = (Label)e.Row.FindControl("lblParcela");

                if (movimento.NumeroParcela.HasValue)
                    lblParcela.Text = String.Format("{0} de {1}", movimento.NumeroParcela.Value.ToString("00"), movimento.TotalParcelas.Value.ToString("00"));
                else
                    lblParcela.Text = "-";
            }
        }


        void ManipularCampos(bool liberar)
        {
            this.meses.Enabled = !liberar;

            this.btnInserir.Visible = !liberar;
            this.btnAlterar.Visible = !liberar;
            this.btnExcluir.Visible = !liberar;
            this.btnSalvar.Visible = liberar;
            this.btnCancelar.Visible = liberar;
            this.btnReplicar.Visible = !liberar;

            this.divCadastro.Visible = liberar;
            this.gvLista.Visible = !liberar;

            this.pnlParcelas.Enabled = String.IsNullOrWhiteSpace(txtCodigo.Text);
            this.pnlParcelas.Visible = (String.IsNullOrWhiteSpace(txtCodigo.Text) || !String.IsNullOrWhiteSpace(txtNumeroParcela.Text));

            this.pnlReplicar.Visible = !liberar;
        }

        void LimparCampos()
        {
            txtCodigo.Text = string.Empty;
            txtData.Text = string.Empty;
            chkConsolidado.Checked = false;
            rblTipo.SelectedIndex = -1;
            txtValor.Text = string.Empty;
            ddlCategoria.SelectedIndex = -1;
            txtDescricao.Text = string.Empty;
            ddlConta.SelectedIndex = -1;
            ddlCartao.SelectedIndex = -1;
            txtNumeroParcela.Text = string.Empty;
            txtTotalParcelas.Text = string.Empty;
            txtObservacao.Text = string.Empty;
        }

        void PreencherCampos(long codMovimento)
        {
            LimparCampos();

            try
            {
                var movimento = Movimento.Carregar(codMovimento);

                txtCodigo.Text = movimento.CodMovimento.ToString();
                txtData.Text = movimento.Data.ToShortDateString();
                chkConsolidado.Checked = movimento.Consolidado;
                rblTipo.SelectedValue = (movimento.Valor > 0 ? "+" : "-");
                txtValor.Text = movimento.ValorFormatado;
                ddlCategoria.SelectedValue = movimento.CodCategoria.ToString();
                hfDescricao.Value = movimento.CodMovimentoDescricao.ToString();
                txtDescricao.Text = movimento.MovimentoDescricao.Descricao;
                ddlConta.SelectedValue = (movimento.CodConta.HasValue ? movimento.CodConta.ToString() : string.Empty);
                ddlCartao.SelectedValue = (movimento.CodCartao.HasValue ? movimento.CodCartao.ToString() : string.Empty);
                txtNumeroParcela.Text = (movimento.NumeroParcela.HasValue ? movimento.NumeroParcela.ToString() : string.Empty);
                txtTotalParcelas.Text = (movimento.TotalParcelas.HasValue ? movimento.TotalParcelas.ToString() : string.Empty);
                txtObservacao.Text = movimento.Observacao;
            }
            catch (Exception ex)
            {
                lblErro.Text = ex.Message;
            }
        }

        void CarregarGrid()
        {
            gvLista.DataSource = Movimento.Listar(meses.Mes);
            gvLista.DataBind();
        }

        void CarregarCombos()
        {
            if (!IsPostBack)
            {
                // Categorias
                ddlCategoria.DataSource = Categoria.Listar();
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, String.Empty);

                // Contas
                ddlConta.DataSource = Conta.Listar();
                ddlConta.DataBind();
                ddlConta.Items.Insert(0, String.Empty);

                // Cartões
                ddlCartao.DataSource = Cartao.Listar();
                ddlCartao.DataBind();
                ddlCartao.Items.Insert(0, String.Empty);
            }
        }
    }
}