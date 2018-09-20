
function ApiMovimentacao() {
    var self = this;
    var url = urlRaiz + "api/Movimentacao/";

    self.listar = function (mesAno) {
        return $.ajax(url + "Listar/" + mesAno, {
            type: "GET"
        });
    }

    self.carregarResumo = function (mesAno, agrupamento) {
        return $.ajax(url + "Resumo/" + mesAno + "/" + agrupamento, {
            type: "GET"
        });
    }

    self.carregar = function (codigo) {
        return $.ajax(url + codigo, {
            type: "GET"
        });
    }

    self.alterar = function (movimentacao) {
        return $.ajax(url + movimentacao.Codigo, {
            type: "PUT",
            data: movimentacao
        });
    }

    self.inserir = function (movimentacao) {
        return $.ajax(url, {
            type: "POST",
            data: movimentacao
        });
    }

    self.excluir = function (codigo) {
        return $.ajax(url + codigo, {
            type: "DELETE"
        });
    }

    self.consolidar = function (codigo) {
        return $.ajax(url + "Consolidar/" + codigo, {
            type: "PUT"
        });
    }

    self.replicar = function (codigo, quantidadeMeses) {
        return $.ajax(url + "Replicar/" + codigo, {
            type: "POST",
            data: { "": quantidadeMeses }
        });
    }
}