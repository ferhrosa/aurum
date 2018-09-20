using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aurum.Dados.Entidades;

namespace Aurum.Web
{
    public partial class Login : Pagina
    {
        public Login()
        {
            this.IgnorarLogin = true;
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            btnEntrar.Click += btnEntrar_Click;

            FormsAuthentication.SignOut();
            this.Usuario = null;
        }

        void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Usuario = Usuario.Autenticar(txtUsuario.Text, txtSenha.Text);

                if ( this.Usuario.CodUsuario.HasValue )
                {
                    FormsAuthentication.RedirectFromLoginPage(this.Usuario.Login, true);
                }
            }
            catch ( Exception ex )
            {
                Response.Write(ex.Message);
            }
        }
    }
}