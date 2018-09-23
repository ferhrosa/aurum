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
    /// Operações relacionadas ao cadastro e exibição de Categorias de movimentações financeiras.
    /// </summary>
    public class CategoriasController : BaseController
    {
        CategoriaService categoriaService = new CategoriaService();

        // GET api/categorias
        /// <summary>
        /// Lista todas as Categorias.
        /// </summary>
        /// <returns>Lista de <see cref="Categoria"/>.</returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Categoria>))]
        public IEnumerable<Categoria> Listar()
        {
            return categoriaService.Listar();
        }

        // GET api/categorias/5
        /// <summary>
        /// Carrega os dados de uma única Categoria.
        /// </summary>
        /// <param name="id">ID da Categoria a ser carregada.</param>
        /// <returns><see cref="Categoria"/></returns>
        [HttpGet]
        [ResponseType(typeof(Categoria))]
        //public Categoria Obter(int id)
        public IHttpActionResult Obter(int id)
        {
            var categoria = categoriaService.Obter(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        // POST api/categorias
        /// <summary>
        /// Insere uma nova Categoria.
        /// </summary>
        /// <param name="categoria">Dados da nova Categoria a ser inserida.</param>
        [HttpPost]
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult Inserir([FromBody]Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("A categoria não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            categoria = categoriaService.Inserir(categoria);

            return CreatedAtRoute(WebApiConfig.RotaPadrao, new { Id = categoria.Id }, categoria);
        }

        // PUT api/categorias/5
        /// <summary>
        /// Atualiza os dados de uma Categoria já existente.
        /// </summary>
        /// <param name="id">ID da Categoria a ser atualizada.</param>
        /// <param name="categoria">Dados da Categoria a ser atualizada.</param>
        [HttpPut]
        public IHttpActionResult Atualizar(int id, [FromBody]Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("A categoria não pode ser \"null\".");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (categoria.Id != id)
            {
                return BadRequest("\"categoria.id\" não é o mesmo que o parâmetro \"id\".");
            }

            categoria = categoriaService.Atualizar(categoria);

            if (categoria == null)
            {
                return NotFound();
            }

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        // DELETE api/categorias/5
        /// <summary>
        /// Exclui uma Categoria.
        /// </summary>
        /// <param name="id">ID da Categoria a ser excluída.</param>
        [HttpDelete]
        public IHttpActionResult Excluir(int id)
        {
            categoriaService.Excluir(id);

            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}