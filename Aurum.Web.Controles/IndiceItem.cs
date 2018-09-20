using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Aurum.Web.Controles
{
    [DefaultProperty("Titulo")]
    [ToolboxData("<{0}:IndiceItem runat=server />")]
    public class IndiceItem : HyperLink
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Titulo
        {
            get
            {
                var s = (string)ViewState["Titulo"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["Titulo"] = value;
            }
        }

        /// <summary>
        /// URL da imagem padrão a ser usada quando não for informado o URL da imagem deste controle.
        /// </summary>
        const string ImagemPadrao = "~/Imagens/Transparente.png";

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(ImagemPadrao)]
        public string ImageUrl
        {
            get
            {
                var s = (string)ViewState["Imagem"];
                return ((s == null) ? ImagemPadrao : s);
            }
            set
            {
                ViewState["Imagem"] = value;
            }
        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Descricao
        {
            get
            {
                var s = (string)ViewState["Descricao"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["Descricao"] = value;
            }
        }


        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.CssClass = "Indice";
            this.ToolTip = Titulo + "\n" + Descricao;

            var imagem = new Image();
            imagem.AlternateText = Titulo;
            imagem.ImageUrl = this.ImageUrl;
            this.Controls.Add(imagem);

            var titulo = new HtmlGenericControl("b");
            titulo.InnerHtml = this.Titulo;
            this.Controls.Add(titulo);

            this.Controls.Add(new LiteralControl("<br/>"));

            var descricao = new HtmlGenericControl("em");
            descricao.InnerHtml = this.Descricao;
            this.Controls.Add(descricao);
        }
    }
}
