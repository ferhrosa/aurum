using Aurum.Modelo;
using Aurum.Negocio;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net;

namespace Aurum.Web.ApiControllers
{
    public class ObjetivoController : ApiController
    {
        private Contexto contexto = new Contexto();

        ObjetivoNegocio _objetivoNegocio;
        private ObjetivoNegocio ObjetivoNegocio
        {
            get
            {
                if (_objetivoNegocio == null)
                    _objetivoNegocio = new ObjetivoNegocio(contexto);
                return _objetivoNegocio;
            }
        }

        // GET api/ObjetivoApi
        public IQueryable<Objetivo> GetObjetivo()
        {
            return this.ObjetivoNegocio.Listar();
        }

        // GET api/ObjetivoApi/5
        [ResponseType(typeof(Objetivo))]
        public IHttpActionResult GetObjetivo(int codigo)
        {
            var objetivo = this.ObjetivoNegocio.Carregar(codigo);

            if (objetivo == null)
                return NotFound();

            return Ok(objetivo);
        }

        // PUT api/ObjetivoApi/5
        public IHttpActionResult PutObjetivo(int codigo, Objetivo objetivo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (codigo != objetivo.Codigo)
                return BadRequest(ModelState);

            try
            {
                this.ObjetivoNegocio.Alterar(objetivo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ObjetivoNegocio.Existe(codigo))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/ObjetivoApi
        [ResponseType(typeof(Objetivo))]
        public IHttpActionResult PostObjetivo(Objetivo objetivo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this.ObjetivoNegocio.Inserir(objetivo);

            return CreatedAtRoute("DefaultApi", new { codigo = objetivo.Codigo }, objetivo);
        }

        // DELETE api/ObjetivoApi/5
        [ResponseType(typeof(Objetivo))]
        public IHttpActionResult DeleteObjetivo(int codigo)
        {
            Objetivo objetivo;
            if (!this.ObjetivoNegocio.Existe(codigo, out objetivo))
                return NotFound();

            this.ObjetivoNegocio.Excluir(codigo);

            return Ok(objetivo);
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