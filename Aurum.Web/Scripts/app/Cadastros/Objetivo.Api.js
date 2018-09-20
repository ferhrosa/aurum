
function ApiObjetivo() {
    var self = this;
    var url = urlRaiz + "api/Objetivo/";

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

    self.alterar = function (objetivo) {
        return $.ajax(url + objetivo.Codigo, {
            type: "PUT",
            data: objetivo
        });
    }

    self.inserir = function (objetivo) {
        return $.ajax(url, {
            type: "POST",
            data: objetivo
        });
    }

    self.excluir = function (codigo) {
        return $.ajax(url + codigo, {
            type: "DELETE"
        });
    }
}