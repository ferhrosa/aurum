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
    public partial class Site : System.Web.UI.MasterPage
    {

        private Pagina Pagina
        {
            get
            {
                return this.Page as Pagina;
            }
        }

        private Usuario Usuario
        {
            get
            {
                return (this.Pagina != null ? this.Pagina.Usuario : new Usuario());
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !this.Page.IsPostBack )
            {
                if ( this.Usuario.Existe )
                {
                    pnlUsuario.Visible = true;
                    hlUsuario.Text = this.Usuario.Nome;
                }
                else if (!this.Pagina.IgnorarLogin)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
        }
    }
}