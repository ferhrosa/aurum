function Objetivo(objetivo) {
    this.Codigo = (objetivo ? objetivo.Codigo : null);
    this.Descricao = (objetivo ? objetivo.Descricao : null);
    this.Ativo = (objetivo ? objetivo.Ativo : null);
    this.Concluido = (objetivo ? objetivo.Concluido : null);

    this.Valor = ko.observable(objetivo ? objetivo.Valor || "" : "");

    this.Valor.Formatado = ko.computed({
        read: function () {
            return formatarMoeda(this.Valor());
        },
        write: function (valor) {
            valor = valor.toString().replace(/[^\d\.,]/g, "");
            valor = valor.replace(/,/g, ".");
            this.Valor(valor);
        }
    }, this);

    this.Carteira_Codigo = (objetivo ? objetivo.Carteira_Codigo : null);
    this.Data = GerarObservableData(objetivo, "Data");
    this.Data.Formatada = GerarComputedDataFormatada(this.Data);
}

function ObjetivosViewModel() {
    var self = this;
    var api = new ApiObjetivo();

    self.erros = ko.observableArray([]);

    self.inserindo = ko.observable(false);
    self.alterando = ko.observable(false);
    self.entrandoFormulario = ko.observable(false);

    self.invalido = ko.observable(false);

    self.selecionado = ko.observable(null);

    // Registro que está sendo inserido ou alterado.
    self.registro = ko.observable(new Objetivo());

    // Lista de registros;
    self.lista = ko.observableArray();

    self.listando = ko.observable(false);
    self.listou = ko.observable(false);
    self.listar = function () {
        self.listou(false);
        self.listando(true);
        api.listar()
            .success(function (retorno) {
                self.selecionado(null);
                self.lista(retorno);
                self.listou(true);
            })
            .fail(function (resultado) {
                TratarExceptionNegocios(resultado, self.erros, "Não foi possível carregar os dados.")
            })
            .done(function () {
                self.listando(false);
            });
    }

    self.inserir = function () {
        location.hash = "Novo";
    }

    self.alterar = function () {
        location.hash = self.selecionado().Codigo;
    }

    self.excluir = function () {
        self.erros([]);

        if (confirm("Tem certeza de que deseja excluir o registro selecionado?")) {
            api.excluir(self.selecionado().Codigo)
                .success(self.listar)
                .error(function (resultado) {
                    TratarExceptionNegocios(resultado, self.erros);
                });
        }
    }

    self.salvar = function () {
        self.erros([]);

        if (self.registro().Valor()) {
            var valor = self.registro().Valor().toString().replace(/,/g, ".");

            if (valor != parseFloat(valor)) {
                self.erros.push("O valor precisa ser numérico.");
                var formValido = false;
            } else {
                var formValido = true;
            }
        }

        //var formValido = $("#cadastro").valid();
        //self.invalido(!formValido);

        if (formValido) {
            if (self.inserindo()) {
                self.registro().Codigo = 0;
                self.registro().Valor = (self.registro().Valor() * 100);
                api.inserir(self.registro())
                    .success(function () {
                        location.hash = "";
                    })
                    .error(function (resultado) {
                        self.registro().Codigo = "";
                        TratarExceptionNegocios(resultado, self.erros);
                    });
            }
            else if (self.alterando()) {
                api.alterar(self.registro())
                    .success(function () {
                        location.hash = "";
                    })
                    .error(function (resultado) {
                        TratarExceptionNegocios(resultado, self.erros);
                    });
            }
        }
    }

    self.cancelar = function () {
        location.hash = "";
    }

    self.selecionar = function (objetivo) {
        self.selecionado(objetivo);
    }

    Sammy(function () {
        this.get(urlPagina + "#Novo", function () {
            $("#modal-cadastro").modal("show");
            self.erros([]);
            self.registro(new Objetivo);
            self.registro().Ativo = true;
            self.registro().Concluido = false;
            self.inserindo(true);
            self.alterando(false);
            self.entrandoFormulario(true);
        });

        this.get(urlPagina + "#:codigo", function () {
            $("#modal-cadastro").modal("show");
            var codigo = this.params.codigo;
            self.erros([]);

            api.carregar(codigo)
                .success(function (objetivo) {
                    self.registro(new Objetivo(objetivo));
                    self.inserindo(false);
                    self.alterando(true);
                    self.entrandoFormulario(true);
                })
                .fail(function () {
                    location.hash = "";
                    self.erros.push("O objetivo de código " + codigo + " não foi encontrado.");
                });
        });

        this.get(urlPagina, function () {
            self.erros([]);
            $("#modal-cadastro").modal("hide");
            self.listar();
            self.inserindo(false);
            self.alterando(false);
            self.invalido(false);
        });
    }).run();
}

var objetivosViewModel = new ObjetivosViewModel()
ko.applyBindings(objetivosViewModel);
