using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aurum.Web.Models;

namespace Aurum.Web.ApiControllers
{
    public class BaseController : ApiController
    {
        // GET api/Base
        [HttpGet]
        public Pagina Principal()
        {
            #region Índice
            var indice = new IndiceItem[] {
                
                // Movimentação

                new IndiceItem() {
                    Grupo = "Movimentação",
                    Titulo = "Saldos",
                    Descricao = "Saldos das contas separados por meses",
                    Url = "movimentacao/saldos",
                    //Imagem = "Saldos"
                },

                new IndiceItem() {
                    Grupo = "Movimentação",
                    Titulo = "Resumo",
                    Descricao = "Resumo das movimentações dos meses",
                    Url = "movimentacao/resumo",
                    Imagem = "Movimento"
                },

                new IndiceItem() {
                    Grupo = "Movimentação",
                    Titulo = "Movimentações",
                    Descricao = "Cadastro das movimentações de entrada e saída por meses",
                    Url = "movimentacao/movimentacoes",
                    Imagem = "Movimento"
                },


                // Cadastros

                new IndiceItem() {
                    Grupo = "Cadastros",
                    Titulo = "Contas",
                    Descricao = "Cadastro de contas bancárias",
                    Url = "cadastros/contas",
                    Imagem = "Conta"
                },

                new IndiceItem() {
                    Grupo = "Cadastros",
                    Titulo = "Cartões",
                    Descricao = "Cadastro de cartões de crédito",
                    Url = "cadastros/cartoes",
                    Imagem = "Cartao"
                },

                new IndiceItem() {
                    Grupo = "Cadastros",
                    Titulo = "Categorias",
                    Descricao = "Cadastro de categorias de movimentações",
                    Url = "cadastros/categorias",
                    Imagem = "Categoria"
                },

            };
            #endregion Índice

            #region Templates
            var templates = new string[] {
                "Indice"
            };
            #endregion Templates

            return new Pagina() {
                Tipo = TipoPagina.Indice,
                Indice = indice,
                Templates = templates
            };
        }
    }
}
