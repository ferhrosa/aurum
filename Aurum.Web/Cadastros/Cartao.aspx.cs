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
    public partial class Cadastros_Cartao : Pagina
    {
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.lblErro.Text = string.Empty;

            this.btnInserir.Click += btnInserir_Click;
            this.btnAlterar.Click += btnAlterar_Click;
            this.btnExcluir.Click += btnExcluir_Click;
            this.btnSalvar.Click += btnSalvar_Click;
            this.btnCancelar.Click += btnCancelar_Click;

            txtValidade.Attributes["readonly"] = "readonly";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvLista.BotaoAlterar = this.btnAlterar;
            gvLista.BotaoExcluir = this.btnExcluir;

            if ( !IsPostBack )
            {
                CarregarGrid();
                CarregarCombos();
            }

            ManipularCampos(false);
        }

        void btnInserir_Click(object sender, EventArgs e)
        {
            LimparCampos();

            ManipularCampos(true);
        }

        void btnAlterar_Click(object sender, EventArgs e)
        {
            PreencherCampos(gvLista.CarregarSelecionado<int>());

            ManipularCampos(true);
        }

        void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                foreach ( var codCartao in gvLista.CarregarSelecionados<int>() )
                {
                    Cartao.Excluir(codCartao);
                }
            }
            catch ( Exception ex )
            {
                lblErro.Text = ex.Message;
            }

            CarregarGrid();
        }

        void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var cartao = new Cartao();

                if ( !String.IsNullOrWhiteSpace(txtCodigo.Text) )
                {
                    int codCartao;

                    if ( int.TryParse(this.txtCodigo.Text, out codCartao) )
                        cartao.CodCartao = codCartao;
                    else
                    {
                        lblErro.Text = "Código inválido.";
                        return;
                    }
                }

                cartao.Ativo = this.chkAtivo.Checked;

                int codConta;
                if ( int.TryParse(this.ddlConta.SelectedValue, out codConta) )
                    cartao.CodConta = (codConta == 0 ? null as int? : codConta);

                cartao.Descricao = this.txtDescricao.Text;
                cartao.Numero = this.txtNumero.Text;
                cartao.Titular = this.txtTitular.Text;

                DateTime validade;
                if ( DateTime.TryParse(this.txtValidade.Text, out validade) )
                    cartao.Validade = validade;

                cartao.Salvar();

                CarregarGrid();
                ManipularCampos(false);
            }
            catch ( Exception ex )
            {
                lblErro.Text = ex.Message;
            }
        }

        void btnCancelar_Click(object sender, EventArgs e)
        {
            ManipularCampos(false);
        }

        void ManipularCampos(bool Liberar)
        {
            this.btnInserir.Visible = !Liberar;
            this.btnAlterar.Visible = !Liberar;
            this.btnExcluir.Visible = !Liberar;
            this.btnSalvar.Visible = Liberar;
            this.btnCancelar.Visible = Liberar;

            this.divCadastro.Visible = Liberar;
            this.gvLista.Visible = !Liberar;
        }

        void LimparCampos()
        {
            this.txtCodigo.Text = string.Empty;
            this.chkAtivo.Checked = true;
            this.ddlConta.SelectedIndex = 0;
            this.txtDescricao.Text = string.Empty;
            this.txtNumero.Text = string.Empty;
            this.txtTitular.Text = string.Empty;
            this.txtValidade.Text = string.Empty;
        }

        void PreencherCampos(int codCartao)
        {
            LimparCampos();

            try
            {
                var cartao = Cartao.Carregar(codCartao);

                this.txtCodigo.Text = cartao.CodCartao.ToString();
                this.chkAtivo.Checked = cartao.Ativo;
                this.ddlConta.SelectedValue = (cartao.CodConta.HasValue ? cartao.CodConta.ToString() : string.Empty);
                this.txtDescricao.Text = cartao.Descricao;
                this.txtNumero.Text = cartao.Numero;
                this.txtTitular.Text = cartao.Titular;
                this.txtValidade.Text = (cartao.Validade.HasValue ? cartao.Validade.Value.ToShortDateString() : string.Empty);
            }
            catch ( Exception ex )
            {
                lblErro.Text = ex.Message;
            }
        }

        void CarregarGrid()
        {
            gvLista.DataSource = Cartao.Listar();
            gvLista.DataBind();
        }

        void CarregarCombos()
        {
            if ( !IsPostBack )
            {
                ddlConta.DataSource = Conta.Listar();
                ddlConta.DataBind();

                ddlConta.Items.Insert(0, String.Empty);
            }
        }
    }
}