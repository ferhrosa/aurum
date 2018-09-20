using Aurum.Modelo;
using Aurum.Negocio;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net;

namespace Aurum.Web.ApiControllers
{
    public class CarteiraController : ApiController
    {
        private Contexto contexto = new Contexto();

        CarteiraNegocio _carteiraNegocio;
        private CarteiraNegocio CarteiraNegocio
        {
            get
            {
                if (_carteiraNegocio == null)
                    _carteiraNegocio = new CarteiraNegocio(contexto);
                return _carteiraNegocio;
            }
        }

        // GET api/CarteiraApi
        public IQueryable<Carteira> GetCarteira()
        {
            return this.CarteiraNegocio.Listar();
        }

        // GET api/CarteiraApi/5
        [ResponseType(typeof(Carteira))]
        public IHttpActionResult GetCarteira(int codigo)
        {
            var carteira = this.CarteiraNegocio.Carregar(codigo);

            if (carteira == null)
                return NotFound();

            return Ok(carteira);
        }

        // PUT api/CarteiraApi/5
        public IHttpActionResult PutCarteira(int codigo, Carteira carteira)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (codigo != carteira.Codigo)
                return BadRequest(ModelState);

            try
            {
                this.CarteiraNegocio.Alterar(carteira);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.CarteiraNegocio.Existe(codigo))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/CarteiraApi
        [ResponseType(typeof(Carteira))]
        public IHttpActionResult PostCarteira(Carteira carteira)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this.CarteiraNegocio.Inserir(carteira);

            return CreatedAtRoute("DefaultApi", new { codigo = carteira.Codigo }, carteira);
        }

        // DELETE api/CarteiraApi/5
        [ResponseType(typeof(Carteira))]
        public IHttpActionResult DeleteCarteira(int codigo)
        {
            Carteira carteira;
            if (!this.CarteiraNegocio.Existe(codigo, out carteira))
                return NotFound();

            this.CarteiraNegocio.Excluir(codigo);

            return Ok(carteira);
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