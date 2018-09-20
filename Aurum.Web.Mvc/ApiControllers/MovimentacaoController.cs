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
    public class MovimentacaoController : ApiController
    {
        // GET api/Movimentacao/Saldos
        [HttpGet]
        public Pagina Saldos()
        {
            //var registros = Categoria.Listar();
            
            #region Templates
            var templates = new string[] {
                //"Cadastro"
            };
            #endregion Templates

            return new Pagina() {
                Tipo = TipoPagina.Outro,
                Titulo = "Saldos dos meses",
                //Registros = registros,
                Templates = templates
            };
        }

        // GET api/Movimentacao/Resumo
        [HttpGet]
        public Pagina Resumo()
        {
            //var registros = Cartao.Listar();

            #region Templates
            var templates = new string[] {
                //"Cadastro"
            };
            #endregion Templates

            return new Pagina()
            {
                Tipo = TipoPagina.Outro,
                Titulo = "Resumo da movimentação",
                //Registros = registros,
                Templates = templates
            };
        }

        // GET api/Movimentacao/Movimentos
        [HttpGet]
        public Pagina Movimentos()
        {
            //var registros = Conta.Listar();

            #region Templates
            var templates = new string[] {
                //"Cadastro"
            };
            #endregion Templates

            return new Pagina()
            {
                Tipo = TipoPagina.Cadastro,
                Titulo = "Movimentos",
                //Registros = registros,
                Templates = templates
            };
        }
    }
}
