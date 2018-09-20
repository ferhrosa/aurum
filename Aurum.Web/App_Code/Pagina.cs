using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Aurum.Dados.Entidades;

namespace Aurum.Web
{
    public class Pagina : System.Web.UI.Page
    {

        public override string StyleSheetTheme
        {
            get
            {
                return "Site";
            }
            set { }
        }

        
        public Usuario Usuario
        {
            get
            {
                var usuario = Session["Usuario"] as Usuario;

                return (usuario != null ? usuario : new Usuario());
            }
            protected set
            {
                Session["Usuario"] = value;
            }
        }

        public bool IgnorarLogin { get; set; }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if ( String.IsNullOrWhiteSpace(this.Title) )
                this.Title = "Aurum";
            else
                this.Title = String.Format("Aurum - {0}", this.Title.Trim());
        }

        protected void Alert(string mensagem)
        {
            var script = new StringBuilder();
            script.Append("alert('");
            script.Append(mensagem.Replace("\n", "\\n"));
            script.Append("');");

            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
        }
        protected void Alert(StringBuilder mensagem)
        {
            Alert(mensagem.ToString());
        }
   }
}