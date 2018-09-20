function MovimentacoesViewModel() {
    var self = this;
    var api = new ApiMovimentacao();
    var apiCarteira = new ApiCarteira();

    self.erros = ko.observableArray([]);

    self.mesAno = $("#mesAno").val();


    self.formulario = new MovimentacoesFormularioCadastroViewModel();
    self.formulario.mesAno = $("#mesAno").val();
    self.formulario.mesAnoData = $("#mesAnoData").val();

    $(self.formulario).on("aposSalvar", function () {
        location.hash = "";
    });

    $(self.formulario).on("aposCancelar", function () {
        location.hash = "";
    });

    $(self.formulario).on("aposErro", function (e, erro) {
        location.hash = "";
        self.erros.push(erro.mensagem);
    });


    self.replicacao = new MovimentacoesReplicacaoViewModel();


    // Lista de registros;
    self.lista = ko.observableArray();

    self.listando = ko.observable(false);
    self.listou = ko.observable(false);
    self.listar = function () {
        self.listou(false);
        self.listando(true);
        api.listar(self.mesAno)
            .success(function (retorno) {
                self.lista(self.montarLista(retorno, self.lista()));
                self.listou(true);
            })
            .fail(function (resultado) {
                TratarExceptionNegocios(resultado, self.erros, "Não foi possível carregar os dados.")
            })
            .done(function () {
                self.listando(false);
            });
    }

    // Prepara a lista de registros, adicionando um campo em cada registro para
    // controle de registros selecionados e marcando novamente como selecionado
    // os registros que estavam selecionados antes.
    self.montarLista = function (registros, registrosAnteriores) {
        var lista = [];

        for (var i = 0; i < registros.length; i++) {
            var registro = registros[i];

            var selecionado = false;

            for (var j = 0; j < registrosAnteriores.length; j++) {
                if (registro.Codigo == registrosAnteriores[j].Codigo && registrosAnteriores[j].Selecionado()) {
                    selecionado = true;
                    break;
                }
            }

            registro.Selecionado = ko.observable(selecionado);

            lista.push(registro);
        }

        return lista;
    }

    // Carrega lista de registros selecionados na tela.
    self.carregarSelecionados = function () {
        var lista = [];

        for (var i = 0; i < self.lista().length; i++) {
            var registro = self.lista()[i];

            if (registro.Selecionado())
                lista.push(registro);
        }

        return lista;
    }

    // Carrega o primeiro registro selecionado da lista de registros da tela.
    // Se não houver registro selecionado, é retornado null.
    self.carregarPrimeiroSelecionado = function () {
        var lista = self.carregarSelecionados();
        if (lista.length)
            return lista[0];

        return null;
    }

    // Retorna a quantidade de registros selecionados na tela.
    self.quantidadeSelecionados = ko.computed(function () {
        var quantidade = 0;

        for (var i = 0; i < self.lista().length; i++) {
            if (self.lista()[i].Selecionado())
                quantidade++;
        }

        return quantidade;
    });

    // Verifica se todos os registros da tela estão selecionados.
    self.todosSelecionados = ko.computed(function () {
        return self.lista().length == self.quantidadeSelecionados();
    });


    // Inicia a inclusão de novo registro.
    self.inserir = function () {
        location.hash = "Novo";
    }

    // Inicia a alteração do registro selecionado (se houver).
    self.alterar = function () {
        var selecionado = self.carregarPrimeiroSelecionado();

        if (selecionado)
            location.hash = selecionado.Codigo;
    }

    // Exclui todos os registros selecionados
    self.excluir = function () {
        self.erros([]);

        if (confirm("Tem certeza de que deseja excluir todos os registros selecionados?")) {
            var quantidadeSelecionados = self.quantidadeSelecionados();
            var quantidadeExcluidos = 0;

            var selecionados = self.carregarSelecionados();

            for (var i = 0; i < quantidadeSelecionados; i++) {
                api.excluir(selecionados[i].Codigo)
                    .success(function () {
                        quantidadeExcluidos++;

                        if (quantidadeExcluidos == quantidadeSelecionados) {
                            self.listar();
                            alert("Registros excluídos com sucesso.")
                        }
                    })
                    .error(function (resultado) {
                        quantidadeExcluidos++;
                        TratarExceptionNegocios(resultado, self.erros);
                    });
            }
        }
    }

    self.consolidar = function () {
        self.erros([]);

        if (confirm("Deseja realmente marcar todas as movimentações selecionadas como consolidadas?")) {
            var quantidadeSelecionados = self.quantidadeSelecionados();
            var quantidadeProcessados = 0;

            var selecionados = self.carregarSelecionados();

            for (var i = 0; i < quantidadeSelecionados; i++) {
                api.consolidar(selecionados[i].Codigo)
                    .success(function () {
                        quantidadeProcessados++;

                        if (quantidadeProcessados == quantidadeSelecionados) {
                            self.listar();
                            alert("Registros marcados como consolidados com sucesso.")
                        }
                    })
                    .error(function (resultado) {
                        quantidadeProcessados++;
                        TratarExceptionNegocios(resultado, self.erros);
                    });
            }
        }
    }

    self.replicar = function () {
        self.erros([]);
        self.replicacao.exibir(self.carregarSelecionados(), self.erros);
    }


    // Marca todos os registros da tela como não selecionados.
    self.limparSelecao = function () {
        for (var i = 0; i < self.lista().length; i++) {
            self.lista()[i].Selecionado(false);
        }
    }

    // Marca todos os registros da tela como selecionados.
    // Caso já estejam todos selecionados, esses são desmarcados.
    self.selecionarTodos = function () {
        // Se não estiverem todos os registros selecionados, todos são selecionados.
        // Se todos os registros estiverem selecionados, esses são desselecionados.
        var valor = !self.todosSelecionados();

        for (var i = 0; i < self.lista().length; i++) {
            self.lista()[i].Selecionado(valor);
        }
    }

    // Marca somente o registro informado como selecionado.
    // Outros registros selecionados são desselecionados.
    self.selecionar = function (movimentacao) {
        self.limparSelecao();
        movimentacao.Selecionado(true);
    }

    // Marca o registro informado como selecionado.
    // Se já estiver selecionado, esse é desselecionado.
    // Outros registros selecionados não são afetados.
    self.selecionarCheck = function (movimentacao) {
        movimentacao.Selecionado(!movimentacao.Selecionado());
    }

    Sammy(function () {
        this.get(urlPagina + "#Novo", function () {
            self.erros([]);
            self.formulario.exibirInserir();

            if (!self.listando() && !self.listou()) self.listar();
        });

        this.get(urlPagina + "#:codigo", function () {
            var codigo = this.params.codigo;
            self.erros([]);
            self.formulario.exibirAlterar(codigo);

            if (!self.listando() && !self.listou()) self.listar();
        });

        this.get(urlPagina, function () {
            self.erros([]);
            self.formulario.ocultar();
            self.listar();
        });
    }).run();
}

var movimentacoesViewModel = new MovimentacoesViewModel()
ko.applyBindings(movimentacoesViewModel);
