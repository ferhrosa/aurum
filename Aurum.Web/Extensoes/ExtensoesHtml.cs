using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aurum.Negocio.Extensoes;

namespace Aurum.Web.Extensoes
{
    public static class ExtensoesHtml
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string itemText, string actionName, string controllerName, /*Permissao[] permissoes = null,*/ string area = null, MvcHtmlString[] childElements = null)
        {
            //if (permissoes != null
            //    && htmlHelper.ViewContext.RequestContext.HttpContext.User.Identity.IsAuthenticated
            //    && !LoginController.UsuarioAtual.VerificarPermissoes(permissoes))
            //{
            //    return null;
            //}

            var currentArea = (string)htmlHelper.ViewContext.RouteData.DataTokens["area"];
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            string finalHtml;
            var linkBuilder = new TagBuilder("a");
            var liBuilder = new TagBuilder("li");

            if (childElements != null && childElements.Length > 0)
            {
                linkBuilder.MergeAttribute("href", "#");
                linkBuilder.AddCssClass("dropdown-toggle");
                linkBuilder.InnerHtml = itemText + " <b class=\"caret\"></b>";
                linkBuilder.MergeAttribute("data-toggle", "dropdown");
                
                var ulBuilder = new TagBuilder("ul");
                ulBuilder.AddCssClass("dropdown-menu");
                ulBuilder.MergeAttribute("role", "menu");
                
                foreach (var item in childElements)
                {
                    if (item != null)
                        ulBuilder.InnerHtml += item.ToString() + "\n";
                }

                liBuilder.InnerHtml = linkBuilder.ToString() + "\n" + ulBuilder.ToString();
                liBuilder.AddCssClass("dropdown");
                
                if (controllerName == currentController || (area != null && area == currentArea))
                {
                    liBuilder.AddCssClass("active");
                }

                finalHtml = liBuilder.ToString() + ulBuilder.ToString();
            }
            else
            {
                UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);
                
                linkBuilder.MergeAttribute("href", urlHelper.Action(actionName, controllerName, new { area = area }));
                
                linkBuilder.SetInnerText(itemText);
                liBuilder.InnerHtml = linkBuilder.ToString();
                
                if (controllerName == currentController && actionName == currentAction)
                {
                    liBuilder.AddCssClass("active");
                }

                finalHtml = liBuilder.ToString();
            }

            return new MvcHtmlString(finalHtml);
        }

        public static bool IsDebug(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        const byte MesesAntesDepois = 4;

        public static MvcHtmlString Meses(this HtmlHelper htmlHelper, string nomeRota, DateTime mes)
        {
            var divPrincipal = new TagBuilder("div");
            divPrincipal.AddCssClass("meses");

            var labelTitulo = new TagBuilder("label");
            labelTitulo.SetInnerText("Mês:");

            var divLinks = new TagBuilder("div");
            divLinks.AddCssClass("btn-group");

            var mesItem = mes.AddMonths(-MesesAntesDepois);

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

            while (mesItem <= mes.AddMonths(MesesAntesDepois))
            {
                // Cria o elemento que exibirá o mês sendo montado.
                var link = new TagBuilder("a");
                link.AddCssClass("btn");
                link.AddCssClass("btn-default");

                // Monta o texto do mês sendo criado.
                var texto = mesItem.ToString("MMMM");
                // Torna maiúsculo o primeiro caractere do nome do mês.
                texto = texto.Substring(0, 1).ToUpper() + texto.Substring(1, texto.Length - 1);

                // Quando o ano do mês sendo montado não for o ano atual, é exibido o ano no final do texto.
                if (mesItem.Year != DateTime.Now.Year)
                    texto += String.Format("<sub>/{0}</sub>", mesItem.Year.ToString());

                // Se for o mês selecionado, demarca com uma classe CSS, para destacá-lo.
                if (mes.Year == mesItem.Year && mes.Month == mesItem.Month)
                {
                    link.AddCssClass("btn-success");
                    link.MergeAttribute("disabled", String.Empty);
                }
                // Adiciona link para a página do mês referente ao item.
                else
                {
                    // Se o mês sendo montado for o atual, demarca com uma classe CSS, para destacá-lo.
                    if (mesItem.Year == DateTime.Now.Year && mesItem.Month == DateTime.Now.Month)
                        link.AddCssClass("btn-info");

                    link.AddCssClass("btn-sm");
                    link.MergeAttribute("href", urlHelper.RouteUrl(nomeRota, new { mesAno = mesItem.MesAno() }));
                }

                link.InnerHtml = texto;

                divLinks.InnerHtml += link.ToString();

                mesItem = mesItem.AddMonths(1);
            }

            divPrincipal.InnerHtml = labelTitulo.ToString() + divLinks.ToString();
            
            return new MvcHtmlString(divPrincipal.ToString());
        }
    }
}
