using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aurum.Dados.Entidades;
using Aurum.Dados.Excecoes;

namespace Aurum.Web.ApiControllers
{
    public class EntidadeCategoriaController : ApiController
    {
        // GET api/categoria
        [HttpGet]
        public IEnumerable<Entidades.Categoria> Listar()
        {
            return Categoria.Listar();
        }

        // GET api/categoria/5
        [HttpGet]
        public Entidades.Categoria Carregar(int codigo)
        {
            return Categoria.Carregar(codigo);
        }

        // POST api/categoria
        [HttpPost]
        public HttpResponseMessage Inserir(Categoria categoria)
        {
            // Define valor 0 ao código para que seja feita inclusão;            
            categoria.CodCategoria = 0;

            categoria.Salvar();

            var resposta = Request.CreateResponse(HttpStatusCode.Created, categoria);
            resposta.Headers.Location = new Uri(Url.Link("ApiDados", new { codigo = categoria.CodCategoria }));

            return resposta;
        }

        // PUT api/categoria
        [HttpPut]
        public void Atualizar(Categoria categoria)
        {
            // Se não for informado o código do registro, gera resposta de que não encontrou o registro.
            if ( categoria.CodCategoria == 0 )
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            try
            {
                // Executa o método para salvar
                categoria.Salvar();
            }
            catch ( ExRegistroInexistente )
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

        // DELETE api/categoria/5
        [HttpDelete]
        public HttpResponseMessage Excluir(int codigo)
        {
            try
            {
                // Executa método de exclusão do registro.
                Categoria.Excluir(codigo);

                // Retorna status dizendo que não há conteúdo a ser retornado.
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            catch ( ExRegistroInexistente )
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }
    }
}
