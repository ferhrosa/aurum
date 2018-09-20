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
    public partial class Cadastros_Conta : Pagina
    {
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            this.btnInserir.Click += btnInserir_Click;
            this.btnAlterar.Click += btnAlterar_Click;
            this.btnExcluir.Click += btnExcluir_Click;
            this.btnSalvar.Click += btnSalvar_Click;
            this.btnCancelar.Click += btnCancelar_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvLista.BotaoAlterar = this.btnAlterar;
            gvLista.BotaoExcluir = this.btnExcluir;

            if ( !IsPostBack ) CarregarGrid();

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
                foreach ( var codConta in gvLista.CarregarSelecionados<int>() )
                {
                    Conta.Excluir(codConta);
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
                var conta = new Conta();

                if ( !String.IsNullOrWhiteSpace(txtCodigo.Text) )
                {
                    int codConta;

                    if ( int.TryParse(this.txtCodigo.Text, out codConta) )
                        conta.CodConta = codConta;
                    else
                    {
                        lblErro.Text = "Código inválido.";
                        return;
                    }
                }

                conta.Ativo = this.chkAtivo.Checked;
                conta.Banco = this.txtBanco.Text;
                conta.Tipo = byte.Parse(this.rblTipoConta.SelectedValue);
                conta.AgenciaConta = this.txtAgenciaConta.Text;

                conta.Salvar();

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
            this.txtBanco.Text = string.Empty;
            this.rblTipoConta.SelectedIndex = -1;
            this.txtAgenciaConta.Text = string.Empty;
        }

        void PreencherCampos(int codConta)
        {
            LimparCampos();

            try
            {
                var conta = Conta.Carregar(codConta);

                this.txtCodigo.Text = conta.CodConta.ToString();
                this.chkAtivo.Checked = conta.Ativo;
                this.txtBanco.Text = conta.Banco;
                this.rblTipoConta.SelectedValue = conta.Tipo.ToString();
                this.txtAgenciaConta.Text = conta.AgenciaConta;
            }
            catch ( Exception ex )
            {
                lblErro.Text = ex.Message;
            }
        }

        void CarregarGrid()
        {
            gvLista.DataSource = Conta.Listar();
            gvLista.DataBind();
        }
    }
}