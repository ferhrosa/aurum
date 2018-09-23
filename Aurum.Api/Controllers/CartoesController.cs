using Aurum.Modelo.Entidades;
using Aurum.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Aurum.Api.Controllers
{
    /// <summary>
    /// Operações relacionadas ao cadastro e exibição de Cartões de movimentações financeiras.
    /// </summary>
    public class CartoesController : BaseController
    {
        CartaoService cartaoService = new CartaoService();

        // GET api/cartoes
        /// <summary>
        /// Lista todas as Cartões.
        /// </summary>
        /// <returns>Lista de <see cref="Cartao"/>.</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Cartao>))]
        public IEnumerable<Cartao> Listar()
        {
            return cartaoService.Listar();
        }

        // GET api/cartoes/5
        /// <summary>
        /// Carrega os dados de um único Cartão.
        /// </summary>
        /// <param name="id">ID do Cartão a ser carregado.</param>
        /// <returns><see cref="Cartao"/></returns>
        [HttpGet]
        [ResponseType(typeof(Cartao))]
        //public Cartao Obter(int id)
        public IHttpActionResult Obter(int id)
        {
            var cartao = cartaoService.Obter(id);

            if (cartao == null)
            {
                return NotFound();
            }

            return Ok(cartao);
        }

        // POST api/cartoes
        /// <summary>
        /// Insere um novo Cartão.
        /// </summary>
        /// <param name="cartao">Dados do novo Cartão a ser inserido.</param>
        [HttpPost]
        [ResponseType(typeof(Cartao))]
        public IHttpActionResult Inserir([FromBody]Cartao cartao)
        {
            if (cartao == null)
            {
                return BadRequest("O cartao não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cartao = cartaoService.Inserir(cartao);

            return CreatedAtRoute(WebApiConfig.RotaPadrao, new { Id = cartao.Id }, cartao);
        }

        // PUT api/cartoes/5
        /// <summary>
        /// Atualiza os dados de um Cartão já existente.
        /// </summary>
        /// <param name="id">ID do Cartão a ser atualizado.</param>
        /// <param name="cartao">Dados do Cartão a ser atualizado.</param>
        [HttpPut]
        public IHttpActionResult Atualizar(int id, [FromBody]Cartao cartao)
        {
            if (cartao == null)
            {
                return BadRequest("O cartao não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cartao.Id != id)
            {
                return BadRequest("\"cartao.id\" não é o mesmo que o parâmetro \"id\".");
            }

            cartao = cartaoService.Atualizar(cartao);

            if (cartao == null)
            {
                return NotFound();
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        // DELETE api/cartoes/5
        /// <summary>
        /// Exclui um Cartão.
        /// </summary>
        /// <param name="id">ID do Cartão a ser excluído.</param>
        [HttpDelete]
        public IHttpActionResult Excluir(int id)
        {
            cartaoService.Excluir(id);

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}