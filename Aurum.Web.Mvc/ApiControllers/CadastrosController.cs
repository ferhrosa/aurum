using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aurum.Dados.Entidades;
using Aurum.Web.Models;

namespace Aurum.Web.ApiControllers
{
    public class CadastrosController : ApiController
    {
        // GET api/Cadastros/Categorias
        [HttpGet]
        public Pagina Categorias()
        {
            var registros = Categoria.Listar();
            
            #region Templates
            var templates = new string[] {
                "Cadastro"
            };
            #endregion Templates

            return new Pagina() {
                Tipo = TipoPagina.Cadastro,
                Titulo = "Categorias",
                Registros = registros,
                Templates = templates
            };
        }

        // GET api/Cadastros/Cartoes
        [HttpGet]
        public Pagina Cartoes()
        {
            var registros = Cartao.Listar();

            #region Templates
            var templates = new string[] {
                "Cadastro"
            };
            #endregion Templates

            return new Pagina()
            {
                Tipo = TipoPagina.Cadastro,
                Titulo = "Cartões",
                Registros = registros,
                Templates = templates
            };
        }

        // GET api/Cadastros/Contas
        [HttpGet]
        public Pagina Contas()
        {
            var registros = Conta.Listar();

            #region Templates
            var templates = new string[] {
                "Cadastro"
            };
            #endregion Templates

            return new Pagina()
            {
                Tipo = TipoPagina.Cadastro,
                Titulo = "Contas",
                Registros = registros,
                Templates = templates
            };
        }
    }
}
