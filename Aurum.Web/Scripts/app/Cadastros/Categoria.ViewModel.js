function Categoria(categoria) {
    this.Codigo = (categoria ? categoria.Codigo : null);
    this.Descricao = (categoria ? categoria.Descricao : null);
    this.Ativo = (categoria ? categoria.Ativo : '');
}

function CategoriasViewModel() {
    var self = this;
    var api = new ApiCategoria();

    self.erros = ko.observableArray([]);

    self.inserindo = ko.observable(false);
    self.alterando = ko.observable(false);
    self.entrandoFormulario = ko.observable(false);

    self.invalido = ko.observable(false);

    self.selecionado = ko.observable(null);

    // Registro que está sendo inserido ou alterado.
    self.registro = ko.observable(new Categoria());

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

        var formValido = $("#cadastro").valid();
        self.invalido(!formValido);

        if (formValido) {
            if (self.inserindo()) {
                self.registro().Codigo = 0;
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

    self.selecionar = function (categoria) {
        self.selecionado(categoria);
    }

    Sammy(function () {
        this.get(urlPagina + "#Novo", function () {
            $("#modal-cadastro").modal("show");
            self.erros([]);
            self.registro(new Categoria());
            self.registro().Ativo = true;
            self.inserindo(true);
            self.alterando(false);
            self.entrandoFormulario(true);
        });

        this.get(urlPagina + "#:codigo", function () {
            $("#modal-cadastro").modal("show");
            var codigo = this.params.codigo;
            self.erros([]);

            api.carregar(codigo)
                .success(function (categoria) {
                    self.registro(categoria);
                    self.inserindo(false);
                    self.alterando(true);
                    self.entrandoFormulario(true);
                })
                .fail(function () {
                    location.hash = "";
                    self.erros.push("A categoria de código " + codigo + " não foi encontrada.");
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

ko.applyBindings(new CategoriasViewModel());