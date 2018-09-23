using Aurum.Modelo.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aurum.Api.Client.Tests
{
    [TestClass()]
    public class CategoriasTests
    {

        [TestMethod()]
        public async Task ApiClientCategorias_listar_nao_deve_ser_null()
        {
            var lista = await ApiClient.Categorias.ListarAsync();

            if (lista == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ApiClientCategorias_inserir_obter_atualizar_obter_excluir_obter()
        {
            var nome = $"Categoria {Guid.NewGuid()}".Substring(0, 30);
            var categoria = new Categoria { Nome = nome };

            categoria = await ApiClient.Categorias.InserirAsync(categoria);

            Assert.IsTrue(categoria.Id.HasValue, "Inserir: A categoria retornada não deve ter o ID null.");
            Assert.AreNotEqual(categoria.Id, 0, "Inserir: A categoria retornada não deve ter o ID 0.");
            Assert.AreEqual(categoria.Nome, nome, $"Inserir: O nome da categoria retornada está diferente do informado. Inserida: '{categoria.Nome}; Informada: {nome}");

            var id = categoria.Id.Value;

            categoria = await ApiClient.Categorias.ObterAsync(id);

            Assert.IsNotNull(categoria, $"Obter: A categoria não foi encontrada. ID: {id}");
            Assert.IsTrue(categoria.Id.HasValue, "Obter: A categoria retornada não deve ter o ID null.");
            Assert.AreNotEqual(categoria.Id, 0, "Obter: A categoria retornada não deve ter o ID 0.");
            Assert.AreEqual(categoria.Nome, nome, $"Obter: O nome da categoria retornada está diferente do informado. Inserida: '{categoria.Nome}; Informada: {nome}");

            var novoNome = $"Categoria Alterada {Guid.NewGuid()}".Substring(0, 30);
            categoria.Nome = novoNome;

            await ApiClient.Categorias.AtualizarAsync(id, categoria);
            categoria = await ApiClient.Categorias.ObterAsync(id);

            Assert.IsNotNull(categoria, $"Atualizar: A categoria não foi encontrada. ID: {id}");
            Assert.IsTrue(categoria.Id.HasValue, "Atualizar: A categoria retornada não deve ter o ID null.");
            Assert.AreNotEqual(categoria.Id, 0, "Atualizar: A categoria retornada não deve ter o ID 0.");
            Assert.AreEqual(categoria.Nome, novoNome, $"Atualizar: O nome da categoria retornada está diferente do informado. Inserida: '{categoria.Nome}; Informada: {nome}");

            await ApiClient.Categorias.ExcluirAsync(id);
            categoria = await ApiClient.Categorias.ObterAsync(id);

            Assert.IsNull(categoria, $"Excluir: A categoria não foi excluída. ID: {id}");
        }

        [TestMethod()]
        public async Task ApiClientCategorias_inserir_listar_excluir()
        {
            var nome = $"Categoria {Guid.NewGuid()}".Substring(0, 30);
            var categoria = new Categoria { Nome = nome };

            categoria = await ApiClient.Categorias.InserirAsync(categoria);

            Assert.IsTrue(categoria.Id.HasValue, "Inserir: A categoria retornada não deve ter o ID null.");
            Assert.AreNotEqual(categoria.Id, 0, "Inserir: A categoria retornada não deve ter o ID 0.");
            Assert.AreEqual(categoria.Nome, nome, $"Inserir: O nome da categoria retornada está diferente do informado. Inserida: '{categoria.Nome}; Informada: {nome}");

            var id = categoria.Id.Value;

            var lista = await ApiClient.Categorias.ListarAsync();

            Assert.IsNotNull(lista, $"Listar: Não trouxe a lista de categorias");
            Assert.IsTrue(lista.Any(c => c.Id.HasValue && c.Id.Value == id), $"Listar: Não existe na lista a categoria com o ID {id}.");

            await ApiClient.Categorias.ExcluirAsync(id);
            lista = await ApiClient.Categorias.ListarAsync();

            Assert.IsNotNull(lista, $"Excluir: Não trouxe a lista de categorias");
            Assert.IsFalse(lista.Any(c => c.Id.HasValue && c.Id.Value == id), $"Excluir: A categoria com o ID {id} ainda está na lista.");
        }

    }
}