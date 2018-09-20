
function ApiCarteira() {
    var self = this;
    var url = urlRaiz + "api/Carteira/";

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

    self.alterar = function (carteira) {
        return $.ajax(url + carteira.Codigo, {
            type: "PUT",
            data: carteira
        });
    }

    self.inserir = function (carteira) {
        return $.ajax(url, {
            type: "POST",
            data: carteira
        });
    }

    self.excluir = function (codigo) {
        return $.ajax(url + codigo, {
            type: "DELETE"
        });
    }
}