using Aurum.Modelo;
using Aurum.Negocio;
using Aurum.Negocio.Exceptions;
using Aurum.Negocio.Extensoes;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Aurum.Web.ApiControllers
{
    public class MovimentacaoController : ApiController
    {
        private Contexto contexto = new Contexto();

        MovimentacaoNegocio _movimentacaoNegocio;
        private MovimentacaoNegocio MovimentacaoNegocio
        {
            get
            {
                if (_movimentacaoNegocio == null)
                    _movimentacaoNegocio = new MovimentacaoNegocio(contexto);
                return _movimentacaoNegocio;
            }
        }

        // GET api/Movimentacao/Listar/201405
        [Route("api/Movimentacao/Listar/{mesAno}")]
        [HttpGet]
        [ResponseType(typeof(IQueryable<Movimentacao>))]
        public IHttpActionResult Listar(int mesAno)
        {
            var mesAnoData = mesAno.MesAno();
            if (!mesAnoData.HasValue)
                return BadRequest("Mês/ano inválido");

            return Ok(this.MovimentacaoNegocio.Listar(mesAnoData.Value));
        }

        [Route("api/Movimentacao/PesquisarDescricoes/{descricao}")]
        [HttpGet]
        [ResponseType(typeof(IQueryable<MovimentacaoDescricao>))]
        public IQueryable<MovimentacaoDescricao> PesquisarDescricoes(string descricao)
        {
            return this.MovimentacaoNegocio.PesquisarDescricoes(descricao);
        }

        [Route("api/Movimentacao/Resumo/{mesAno}/{agrupamento}")]
        [HttpGet]
        public IHttpActionResult CarregarResumo(int mesAno, string agrupamento)
        {
            var mesAnoData = mesAno.MesAno();
            if (!mesAnoData.HasValue)
                return BadRequest("Mês/ano inválido");

            switch (agrupamento.ToLower())
            {
                case "categoria":
                    return Ok(this.MovimentacaoNegocio.CarregarResumoPorCategoria(mesAnoData.Value));
                case "descricao":
                    return Ok(this.MovimentacaoNegocio.CarregarResumoPorDescricao(mesAnoData.Value));
                default:
                    throw new NegociosException("Agrupamento inválido.");
            }
        }
        
        // GET api/Movimentacao/5
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult GetMovimentacao(long codigo)
        {
            var movimentacao = this.MovimentacaoNegocio.Carregar(codigo);

            if (movimentacao == null)
                return NotFound();

            return Ok(movimentacao);
        }

        // PUT api/Movimentacao/5
        public IHttpActionResult PutMovimentacao(long codigo, Movimentacao movimentacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (codigo != movimentacao.Codigo)
                return BadRequest(ModelState);

            try
            {
                this.MovimentacaoNegocio.Alterar(movimentacao);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MovimentacaoNegocio.Existe(codigo))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Movimentacao
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult PostMovimentacao(Movimentacao movimentacao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this.MovimentacaoNegocio.Inserir(movimentacao);

            return CreatedAtRoute("DefaultApi", new { codigo = movimentacao.Codigo }, movimentacao);
        }

        // DELETE api/Movimentacao/5
        [ResponseType(typeof(Movimentacao))]
        public IHttpActionResult DeleteMovimentacao(long codigo)
        {
            Movimentacao movimentacao;
            if (!this.MovimentacaoNegocio.Existe(codigo, out movimentacao))
                return NotFound();

            this.MovimentacaoNegocio.Excluir(codigo);

            return Ok(movimentacao);
        }

        // PUT api/Movimentacao/Consolidar/5
        [Route("api/Movimentacao/Consolidar/{codigo}")]
        [HttpPut]
        public IHttpActionResult Consolidar(long codigo)
        {
            try
            {
                this.MovimentacaoNegocio.Consolidar(codigo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MovimentacaoNegocio.Existe(codigo))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Movimentacao/Replicar/5
        [Route("api/Movimentacao/Replicar/{codigo}")]
        [HttpPost]
        public IHttpActionResult Replicar(long codigo, [FromBody] int quantidadeMeses)
        {
            try
            {
                this.MovimentacaoNegocio.Replicar(codigo, quantidadeMeses);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MovimentacaoNegocio.Existe(codigo))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contexto.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}