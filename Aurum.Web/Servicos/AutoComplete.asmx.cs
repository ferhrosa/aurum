using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using Aurum.Dados.Entidades;

namespace Aurum.Web.Servicos
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] ListarMovimentoDescricao(string prefixText, int count)
        {
            return (
                from d in MovimentoDescricao.Listar(prefixText)
                select AutoCompleteExtender.CreateAutoCompleteItem(d.Descricao, d.CodMovimentoDescricao.ToString())
            ).ToArray();
        }

    }
}
