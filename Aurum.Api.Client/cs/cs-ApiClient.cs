/////////////////////////////////////////////
// Código gerado por um template T4.       //
// Não modifique diretamente este arquivo. //
/////////////////////////////////////////////
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aurum.Api.Client
{
    public static class ApiClient
    {
        private static HttpClient httpClient = new HttpClient();

        public readonly static string baseUrl;

        static ApiClient()
        {
            baseUrl = ConfigurationManager.AppSettings["Aurum.Api.Client.BaseUrl"];
            if (!baseUrl.EndsWith("/")) { baseUrl += "/"; }

            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public static class Cartoes
        {

            public static async Task<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Cartao>> ListarAsync()
            {
                System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Cartao> resultado = null;

                var response = await httpClient.GetAsync($"cartoes");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Cartao>>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Cartao> ObterAsync(System.Int32 id)
            {
                Aurum.Modelo.Entidades.Cartao resultado = null;

                var response = await httpClient.GetAsync($"cartoes/{id}");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Cartao>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Cartao> InserirAsync(Aurum.Modelo.Entidades.Cartao cartao)
            {
                Aurum.Modelo.Entidades.Cartao resultado = null;

                var response = await httpClient.PostAsJsonAsync($"cartoes", cartao);

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Cartao>();
                }

                return resultado;
            }

            public static async Task AtualizarAsync(System.Int32 id, Aurum.Modelo.Entidades.Cartao cartao)
            {
                await httpClient.PutAsJsonAsync($"cartoes/{id}", cartao);
            }

            public static async Task ExcluirAsync(System.Int32 id)
            {
                await httpClient.DeleteAsync($"cartoes/{id}");
            }

        } // class Cartoes


        public static class Movimentacoes
        {

            public static async Task<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Movimentacao>> ListarAsync()
            {
                System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Movimentacao> resultado = null;

                var response = await httpClient.GetAsync($"movimentacoes");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Movimentacao>>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Movimentacao> ObterAsync(System.Guid id)
            {
                Aurum.Modelo.Entidades.Movimentacao resultado = null;

                var response = await httpClient.GetAsync($"movimentacoes/{id}");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Movimentacao>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Movimentacao> InserirAsync(Aurum.Modelo.Entidades.Movimentacao movimentacao)
            {
                Aurum.Modelo.Entidades.Movimentacao resultado = null;

                var response = await httpClient.PostAsJsonAsync($"movimentacoes", movimentacao);

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Movimentacao>();
                }

                return resultado;
            }

            public static async Task AtualizarAsync(System.Guid id, Aurum.Modelo.Entidades.Movimentacao movimentacao)
            {
                await httpClient.PutAsJsonAsync($"movimentacoes/{id}", movimentacao);
            }

            public static async Task ExcluirAsync(System.Guid id)
            {
                await httpClient.DeleteAsync($"movimentacoes/{id}");
            }

        } // class Movimentacoes


        public static class Contas
        {

            public static async Task<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Conta>> ListarAsync()
            {
                System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Conta> resultado = null;

                var response = await httpClient.GetAsync($"contas");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Conta>>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Conta> ObterAsync(System.Int32 id)
            {
                Aurum.Modelo.Entidades.Conta resultado = null;

                var response = await httpClient.GetAsync($"contas/{id}");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Conta>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Conta> InserirAsync(Aurum.Modelo.Entidades.Conta conta)
            {
                Aurum.Modelo.Entidades.Conta resultado = null;

                var response = await httpClient.PostAsJsonAsync($"contas", conta);

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Conta>();
                }

                return resultado;
            }

            public static async Task AtualizarAsync(System.Int32 id, Aurum.Modelo.Entidades.Conta conta)
            {
                await httpClient.PutAsJsonAsync($"contas/{id}", conta);
            }

            public static async Task ExcluirAsync(System.Int32 id)
            {
                await httpClient.DeleteAsync($"contas/{id}");
            }

        } // class Contas


        public static class Categorias
        {

            public static async Task<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Categoria>> ListarAsync()
            {
                System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Categoria> resultado = null;

                var response = await httpClient.GetAsync($"categorias");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<System.Collections.Generic.IEnumerable<Aurum.Modelo.Entidades.Categoria>>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Categoria> ObterAsync(System.Int32 id)
            {
                Aurum.Modelo.Entidades.Categoria resultado = null;

                var response = await httpClient.GetAsync($"categorias/{id}");

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Categoria>();
                }

                return resultado;
            }

            public static async Task<Aurum.Modelo.Entidades.Categoria> InserirAsync(Aurum.Modelo.Entidades.Categoria categoria)
            {
                Aurum.Modelo.Entidades.Categoria resultado = null;

                var response = await httpClient.PostAsJsonAsync($"categorias", categoria);

                if (response.IsSuccessStatusCode)
                {
                    resultado = await response.Content.ReadAsAsync<Aurum.Modelo.Entidades.Categoria>();
                }

                return resultado;
            }

            public static async Task AtualizarAsync(System.Int32 id, Aurum.Modelo.Entidades.Categoria categoria)
            {
                await httpClient.PutAsJsonAsync($"categorias/{id}", categoria);
            }

            public static async Task ExcluirAsync(System.Int32 id)
            {
                await httpClient.DeleteAsync($"categorias/{id}");
            }

        } // class Categorias

    } // class ApiClient
} // namespace

