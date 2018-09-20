using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;

namespace Aurum.ClienteWeb.Base
{
    public class RequisicaoWeb
    {

        const string NomeParametro = "Url";


        private static HttpWebRequest NovaRequisicao(string nomeEntidade, string parametroUrl)
        {
            // Caso não seja informado o nome da entidade, gera erro.
            if ( String.IsNullOrWhiteSpace(nomeEntidade) )
                throw new ArgumentNullException("nomeEntidade");
            
            // Carrega o Url do Aurum que está armazenado no arquivo de configuração.
            var url = ConfigurationManager.AppSettings[NomeParametro];

            // Se não for encontrado item na configuração, gera erro.
            if ( String.IsNullOrWhiteSpace(url) )
                throw new ConfigurationErrorsException(String.Format("Não foi encontrada a chave \"{0}\" no arquivo de configurações.", NomeParametro));

            // Caso o Url configurado não termine em "/", essa barra é adicionada ao final.
            if ( url.Last() != '/' )
                url += '/';

            // Monta o URL da entidade na API web.
            url += "api/" + nomeEntidade;

            // Se for informado o parâmetro que deve estar no URL, esse é adicionado no final.
            if (!String.IsNullOrWhiteSpace(parametroUrl))
                url +="/" + parametroUrl;

            // Cria uma nova requisição web com o URL montado.
            var requisicao = (HttpWebRequest)WebRequest.Create(url);

            requisicao.Accept = "text/json";

            return requisicao;
        }

        private static HttpWebRequest NovaRequisicao(string nomeEntidade)
        {
            return NovaRequisicao(nomeEntidade, null);
        }

        internal static List<T> Listar<T>() where T : Entidades.Base.Entidade, new()
        {
            var nomeEntidade = typeof(T).Name;

            var requisicao = NovaRequisicao(nomeEntidade);

            var resposta = requisicao.GetResponse();

            var texto = resposta.ToString();

            throw new NotImplementedException();
        }

    }
}
