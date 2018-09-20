function Movimentacao(movimentacao) {
    this.Codigo = (movimentacao ? movimentacao.Codigo : null);
    this.Data = GerarObservableData(movimentacao, "Data");
    this.Consolidado = ko.observable(movimentacao ? movimentacao.Consolidado : false);
    this.MesAno = (movimentacao ? movimentacao.MesAno : null);
    //this.Descricao_Codigo = (movimentacao ? movimentacao.Descricao_Codigo : 0);
    this.Descricao = { Descricao: (movimentacao && movimentacao.Descricao ? movimentacao.Descricao.Descricao : null) }
    this.Categoria_Codigo = (movimentacao ? movimentacao.Categoria_Codigo : null);
    this.Valor = ko.observable(movimentacao ? (movimentacao.Valor < 0 ? -movimentacao.Valor : movimentacao.Valor) : "");
    this.Carteira_Codigo = ko.observable(movimentacao ? movimentacao.Carteira_Codigo : "");
    this.Carteira_Tipo = ko.observable(movimentacao && movimentacao.Carteira ? movimentacao.Carteira.Tipo : 0);
    this.Objetivo_Codigo = (movimentacao ? movimentacao.Carteira_Codigo : null);
    this.NumeroParcela = ko.observable(movimentacao ? movimentacao.NumeroParcela : null);
    this.TotalParcelas = ko.observable(movimentacao ? movimentacao.TotalParcelas : null);
    this.Observacao = (movimentacao ? movimentacao.Observacao : null);

    this.Tipo = (movimentacao ? (movimentacao.Valor < 0 ? "D" : "C") : "");

    this.Data.Formatada = GerarComputedDataFormatada(this.Data);

    this.ValorTela = "";
    this.Valor.Formatado = ko.computed({
        read: function () {
            return formatarMoeda((this.Valor() || 0) / 100);
        },
        write: function (valor) {
            this.ValorTela = valor;

            valor = valor.toString().replace(/[^\d\.,]/g, "");
            valor = valor.replace(/,/g, ".");

            valor = parseInt(parseFloat(valor) * 100);

            this.Valor(valor);
        }
    }, this);

    this.PermiteParcelamento = ko.computed(function () {
        return this.Carteira_Tipo() == 4;
    }, this);
}

function MovimentacoesFormularioCadastroViewModel() {
    var self = this;
    var api = new ApiMovimentacao();
    var apiCarteira = new ApiCarteira();

    self.mesAno = null;
    self.mesAnoData = null;

    self.erros = ko.observableArray([]);

    self.inserindo = ko.observable(false);
    self.alterando = ko.observable(false);
    self.entrandoFormulario = ko.observable(false);

    self.invalido = ko.observable(false);

    // Registro que está sendo inserido ou alterado.
    self.registro = ko.observable(new Movimentacao());

    self.salvar = function () {
        self.erros([]);

        var formValido = true;

        if (self.registro().ValorTela) {
            if (self.registro().Valor() != parseInt(self.registro().Valor())) {
                self.erros.push("O valor precisa ser numérico.");
                var formValido = false;
            } else {
                var formValido = true;
            }
        }

        if (!formValido) return;

        formValido = $("#cadastro").valid();
        self.invalido(!formValido);

        if (formValido) {
            if (self.registro().Tipo == "D")
                self.registro().Valor(-self.registro().Valor());

            if (self.inserindo()) {
                self.registro().Codigo = 0;
                api.inserir(self.registro())
                    .success(function () {
                        $(self).trigger("aposSalvar");
                    })
                    .error(function (resultado) {
                        self.registro().Codigo = "";
                        TratarExceptionNegocios(resultado, self.erros);
                    });
            }
            else if (self.alterando()) {
                api.alterar(self.registro())
                    .success(function () {
                        $(self).trigger("aposSalvar");
                    })
                    .error(function (resultado) {
                        TratarExceptionNegocios(resultado, self.erros);
                    });
            }
        }
    }

    self.cancelar = function () {
        $(self).trigger("aposCancelar");
    }

    self.exibirInserir = function () {
        $("#modal-cadastro-movimentacao").modal("show");
        self.erros([]);
        self.registro(new Movimentacao());
        self.registro().Consolidado(false);
        self.registro().MesAno = self.mesAnoData;
        self.inserindo(true);
        self.alterando(false);
        self.entrandoFormulario(true);

        self.inicializarTypeahead();
    }

    self.exibirAlterar = function (codigo) {
        self.erros([]);

        api.carregar(codigo)
            .success(function (movimentacao) {
                $("#modal-cadastro-movimentacao").modal("show");
                self.registro(new Movimentacao(movimentacao));
                self.inserindo(false);
                self.alterando(true);

                self.inicializarTypeahead();
            })
            .fail(function () {
                $(self).trigger("aposErro", { mensagem: "A movimentação de código " + codigo + " não foi encontrada." });
            });
    }

    self.ocultar = function () {
        $("#modal-cadastro-movimentacao").modal("hide");
        self.inserindo(false);
        self.alterando(false);
        self.invalido(false);
    }


    // Inicializa o motor de sugestões, que busca os dados na API de acordo com o termo digitado no campo na tela.
    self.descricoes = new Bloodhound({
        remote: urlRaiz + "api/Movimentacao/PesquisarDescricoes/%QUERY",
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 10
    });
    self.descricoes.initialize();

    // Configura o campo da tela para utilizar o componente Typeahead.
    self.inicializarTypeahead = function () {
        $("#cadastro #Descricao_Descricao").typeahead({
            minlength: 1,
            highlight: true
        },
        {
            name: "descricoes",
            displayKey: "Descricao",
            source: self.descricoes.ttAdapter()
        });
    }


    self.carteiras = [];

    apiCarteira.listar()
        .done(function (resultado) {
            self.carteiras = resultado;
        });

    self.verificarCarteiraTipo = function () {
        self.registro().Carteira_Tipo(0);

        if (self.registro().Carteira_Codigo()) {
            for (var i = 0; i < self.carteiras.length; i++) {
                var carteira = self.carteiras[i];

                if (carteira.Codigo == self.registro().Carteira_Codigo())
                    self.registro().Carteira_Tipo(carteira.Tipo);
            }
        }

        if (!self.registro().PermiteParcelamento()) {
            self.registro().NumeroParcela(null);
            self.registro().TotalParcelas(null);
        }
    }

}
