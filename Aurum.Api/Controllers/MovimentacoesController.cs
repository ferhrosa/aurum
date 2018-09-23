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
    /// Operações relacionadas ao cadastro e exibição de Movimentações financeiras.
    /// </summary>
    public class MovimentacoesController : BaseController
    {
        MovimentacaoService movimentacaoService = new MovimentacaoService();

        // GET api/movimentacoes
        /// <summary>
        /// Lista todas as Movimentações.
        /// </summary>
        /// <returns>Lista de <see cref="Movimentacao"/>.</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Movimentacao>))]
        public IEnumerable<Movimentacao> Listar()
        {
            return movimentacaoService.Listar();
        }

        // GET api/movimentacoes/5
        /// <summary>
        /// Carrega os dados de uma única Movimentação.
        /// </summary>
        /// <param name="id">ID da Movimmentação a ser carregada.</param>
        /// <returns><see cref="Movimentacao"/></returns>
        [HttpGet]
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult Obter(Guid id)
        {
            var movimentacao = movimentacaoService.Obter(id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return Ok(movimentacao);
        }

        // POST api/movimentacoes
        /// <summary>
        /// Insere uma nova Movimentação.
        /// </summary>
        /// <param name="movimentacao">Dados da nova Movimentação a ser inserida.</param>
        [HttpPost]
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult Inserir([FromBody]Movimentacao movimentacao)
        {
            if (movimentacao == null)
            {
                return BadRequest("A movimentacao não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            movimentacao = movimentacaoService.Inserir(movimentacao);

            return CreatedAtRoute(WebApiConfig.RotaPadrao, new { Id = movimentacao.Id }, movimentacao);
        }

        // PUT api/movimentacoes/5
        /// <summary>
        /// Atualiza os dados de uma Movimentação já existente.
        /// </summary>
        /// <param name="id">ID da Movimentação a ser atualizada.</param>
        /// <param name="movimentacao">Dados da Movimentação a ser atualizada.</param>
        [HttpPut]
        public IHttpActionResult Atualizar(Guid id, [FromBody]Movimentacao movimentacao)
        {
            if (movimentacao == null)
            {
                return BadRequest($"A {nameof(movimentacao)} não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (movimentacao.Id != id)
            {
                return BadRequest($"\"{nameof(movimentacao)}.id\" não é o mesmo que o parâmetro \"{nameof(id)}\".");
            }

            movimentacao = movimentacaoService.Atualizar(movimentacao);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        // DELETE api/movimentacoes/5
        /// <summary>
        /// Exclui uma Movimentação.
        /// </summary>
        /// <param name="id">ID da Movimentação a ser excluída.</param>
        [HttpDelete]
        public IHttpActionResult Excluir(Guid id)
        {
            movimentacaoService.Excluir(id);

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}