
function ApiCategoria() {
    var self = this;
    var url = urlRaiz + "api/Categoria/";

    self.listar = function () {
        return $.ajax(url, {
            type: "GET"
        });
    }

    self.carregar = function (codigo) {
        return $.ajax(url + codigo, {
            type: "GET"
        });
    }

    self.alterar = function (categoria) {
        return $.ajax(url + categoria.Codigo, {
            type: "PUT",
            data: categoria
        });
    }

    self.inserir = function (categoria) {
        return $.ajax(url, {
            type: "POST",
            data: categoria
        });
    }

    self.excluir = function (codigo) {
        return $.ajax(url + codigo, {
            type: "DELETE"
        });
    }
}