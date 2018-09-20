using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aurum.Dados.Entidades;

namespace Aurum.Web.ApiControllers
{
    public class EntidadeCartaoController : ApiController
    {
        // GET api/cartao
        public IEnumerable<Entidades.Cartao> Get()
        {
            return Cartao.Listar();
        }

        // GET api/cartao/5
        public Entidades.Cartao Get(int codigo)
        {
            return Cartao.Carregar(codigo);
        }

        // POST api/cartao
        public HttpResponseMessage Post(Entidades.Cartao cartao)
        {
            var cartaoSalvar = (Cartao)cartao;
            cartaoSalvar.Salvar();

            var resposta = Request.CreateResponse(HttpStatusCode.Created, cartao);
            
            resposta.Headers.Location = new Uri(Url.Link("ApiDados", new { codigo = cartao.CodCartao }));

            return resposta;
        }

        // PUT api/cartao/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/cartao/5
        public void Delete(int id)
        {
        }
    }
}
