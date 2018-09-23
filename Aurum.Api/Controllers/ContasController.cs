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
    /// Operações relacionadas ao cadastro e exibição de Contas para movimentações financeiras.
    /// </summary>
    public class ContasController : BaseController
    {
        ContaService contaService = new ContaService();

        // GET api/contas
        /// <summary>
        /// Lista todas as Contas.
        /// </summary>
        /// <returns>Lista de <see cref="Conta"/>.</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Conta>))]
        public IEnumerable<Conta> Listar()
        {
            return contaService.Listar();
        }

        // GET api/contas/5
        /// <summary>
        /// Carrega os dados de uma única Conta.
        /// </summary>
        /// <param name="id">ID da Conta a ser carregada.</param>
        /// <returns><see cref="Conta"/></returns>
        [HttpGet]
        [ResponseType(typeof(Conta))]
        //public Conta Obter(int id)
        public IHttpActionResult Obter(int id)
        {
            var conta = contaService.Obter(id);

            if (conta == null)
            {
                return NotFound();
            }

            return Ok(conta);
        }

        // POST api/contas
        /// <summary>
        /// Insere uma nova Conta.
        /// </summary>
        /// <param name="conta">Dados da nova Conta a ser inserida.</param>
        [HttpPost]
        [ResponseType(typeof(Conta))]
        public IHttpActionResult Inserir([FromBody]Conta conta)
        {
            if (conta == null)
            {
                return BadRequest("A conta não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            conta = contaService.Inserir(conta);

            return CreatedAtRoute(WebApiConfig.RotaPadrao, new { Id = conta.Id }, conta);
        }

        // PUT api/contas/5
        /// <summary>
        /// Atualiza os dados de uma Conta já existente.
        /// </summary>
        /// <param name="id">ID da Conta a ser atualizada.</param>
        /// <param name="conta">Dados da Conta a ser atualizada.</param>
        [HttpPut]
        public IHttpActionResult Atualizar(int id, [FromBody]Conta conta)
        {
            if (conta == null)
            {
                return BadRequest("A conta não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (conta.Id != id)
            {
                return BadRequest("\"conta.id\" não é o mesmo que o parâmetro \"id\".");
            }

            conta = contaService.Atualizar(conta);

            if (conta == null)
            {
                return NotFound();
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        // DELETE api/contas/5
        /// <summary>
        /// Exclui uma Conta.
        /// </summary>
        /// <param name="id">ID da Conta a ser excluída.</param>
        [HttpDelete]
        public IHttpActionResult Excluir(int id)
        {
            contaService.Excluir(id);

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}