using Aurum.Modelo;
using Aurum.Negocio;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net;

namespace Aurum.Web.ApiControllers
{
    public class CategoriaController : ApiController
    {
        private Contexto contexto = new Contexto();

        CategoriaNegocio _categoriaNegocio;
        private CategoriaNegocio CategoriaNegocio
        {
            get
            {
                if (_categoriaNegocio == null)
                    _categoriaNegocio = new CategoriaNegocio(contexto);
                return _categoriaNegocio;
            }
        }

        // GET api/CategoriaApi
        public IQueryable<Categoria> GetCategoria()
        {
            return this.CategoriaNegocio.Listar();
        }

        // GET api/CategoriaApi/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult GetCategoria(int codigo)
        {
            var categoria = this.CategoriaNegocio.Carregar(codigo);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        // PUT api/CategoriaApi/5
        public IHttpActionResult PutCategoria(int codigo, Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (codigo != categoria.Codigo)
                return BadRequest(ModelState);

            try
            {
                this.CategoriaNegocio.Alterar(categoria);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.CategoriaNegocio.Existe(codigo))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/CategoriaApi
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this.CategoriaNegocio.Inserir(categoria);

            return CreatedAtRoute("DefaultApi", new { codigo = categoria.Codigo }, categoria);
        }

        // DELETE api/CategoriaApi/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategoria(int codigo)
        {
            Categoria categoria;
            if (!this.CategoriaNegocio.Existe(codigo, out categoria))
                return NotFound();

            this.CategoriaNegocio.Excluir(codigo);

            return Ok(categoria);
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