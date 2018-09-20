function MovimentacoesReplicacaoViewModel() {
    var self = this;
    var api = new ApiMovimentacao();

    self.invalido = ko.observable(false);

    self.erros = ko.observableArray([]);

    // Representa o objeto que lida com a lista de mensagens de erro.
    self.errosPai = null;

    // Lista dos registros que serão replicados (caso seja realizada a replicação).
    self.registros = [];

    // Quantidade de cópias que serão realizadas para os registro selecionados.
    // (Para os meses subsequentes aos registros.)
    self.quantidadeMeses = ko.observable('');


    // Realiza a replicação dos registros selecionados, para a quantidade de meses informada.
    self.replicar = function () {

        var formValido = $("#replicacao-movimentacao-form").valid();
        self.invalido(!formValido);

        if (formValido) {

            var textoMovimentacao;
            if (self.registros.length == 1)
                textoMovimentacao = "a movimentação selecionada";
            else
                textoMovimentacao = "todas as movimentações selecionadas";

            var textoMeses;
            if (self.quantidadeMeses() == 1)
                textoMeses = "o mês seguinte";
            else
                textoMeses = "os " + self.quantidadeMeses() + " meses seguintes";

            if (confirm("Deseja realmente replicar " + textoMovimentacao + " para " + textoMeses + "?")) {
                var quantidadeSelecionados = self.registros.length;
                var quantidadeProcessados = 0;

                for (var i = 0; i < quantidadeSelecionados; i++) {
                    api.replicar(self.registros[i].Codigo, self.quantidadeMeses())
                        .fail(function (resultado) {
                            TratarExceptionNegocios(resultado, self.errosPai);
                        })
                        .always(function() {
                            quantidadeProcessados++;

                            if (quantidadeProcessados == quantidadeSelecionados) {
                                alert("Registros replicados com sucesso.");
                                self.ocultar();
                            }
                        });
                }
            }
        }
    }

    self.cancelar = function () {
        self.ocultar();
    }

    self.exibir = function (registros, listaErros) {
        self.invalido(false);
        self.erros([]);
        self.quantidadeMeses('');

        self.errosPai = listaErros;
        self.registros = registros;

        $("#modal-replicacao-movimentacao").modal("show");
        $("#replicacao-movimentacao-form").validate().resetForm();
    }

    self.ocultar = function () {
        $("#modal-replicacao-movimentacao").modal("hide");
    }
}
