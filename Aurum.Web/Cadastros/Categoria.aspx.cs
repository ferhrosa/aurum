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
    public partial class Cadastros_Categoria : Pagina
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
                foreach ( var codCategoria in gvLista.CarregarSelecionados<int>() )
                {
                    Categoria.Excluir(codCategoria);
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
                var categoria = new Categoria();

                if ( !String.IsNullOrWhiteSpace(txtCodigo.Text) )
                {
                    int codCategoria;

                    if ( int.TryParse(this.txtCodigo.Text, out codCategoria) )
                        categoria.CodCategoria = codCategoria;
                    else
                    {
                        lblErro.Text = "Código inválido.";
                        return;
                    }
                }

                categoria.Ativo = this.chkAtivo.Checked;
                categoria.Descricao = this.txtDescricao.Text;

                categoria.Salvar();

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
            this.txtDescricao.Text = string.Empty;
        }

        void PreencherCampos(int codCategoria)
        {
            LimparCampos();

            try
            {
                var categoria = Categoria.Carregar(codCategoria);

                this.txtCodigo.Text = categoria.CodCategoria.ToString();
                this.chkAtivo.Checked = categoria.Ativo;
                this.txtDescricao.Text = categoria.Descricao;
            }
            catch ( Exception ex )
            {
                lblErro.Text = ex.Message;
            }
        }

        void CarregarGrid()
        {
            gvLista.DataSource = Categoria.Listar();
            gvLista.DataBind();
        }
    }
}