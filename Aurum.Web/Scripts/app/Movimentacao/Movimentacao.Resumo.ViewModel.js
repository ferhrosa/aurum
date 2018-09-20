function MovimentacoesResumoViewModel() {
    var self = this;
    var api = new ApiMovimentacao();
    //var apiCarteira = new ApiCarteira();

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


    self.agrupamento = ko.observable(localStorage.resumoAgrupamento || "categoria");

    self.agrupamentoNome = ko.computed(function () {
        switch (self.agrupamento()) {
            case "categoria": return "Categoria";
            case "descricao": return "Descrição";
            default: return "";
        }
    });

    // Lista de registros;
    self.lista = ko.observable(null);

    self.listar = function () {
        var agrupamento = self.agrupamento();
        if (!agrupamento) agrupamento = "categoria";

        localStorage.resumoAgrupamento = agrupamento;

        api.carregarResumo(self.mesAno, agrupamento)
            .done(function (retorno) {
                self.lista(retorno);

                self.irAoDiaAtual();

                //$("#tableResumo>tbody>tr>th").each(function () {
                //    var th = $(this);
                //    th.height(th.parent().height());
                //});
            })
            .fail(function(retorno) {
                TratarExceptionNegocios(resultado, self.erros, "Não foi possível carregar os dados.");
            });
    }

    // Adiciona assinatura para que a função "listar" seja
    // executada quando for alterado o agrupamento.
    self.agrupamento.subscribe(self.listar);


    self.dias = ko.computed(function () {
        var ano = self.mesAno.substr(0, 4);
        var mes = self.mesAno.substr(4, 2);
        var diasNoMes = new Date(ano, mes, 0).getDate();

        var dias = [];

        for (var i = 1; i <= diasNoMes; i++)
            dias.push(i);

        return dias;
    });

    self.diaAtual = ko.computed(function () {
        var dataAtual = new Date();
        var ano = self.mesAno.substr(0, 4);
        var mes = self.mesAno.substr(4, 2);

        if (dataAtual.getFullYear() != ano || dataAtual.getMonth() + 1 != mes)
            return null;
        else
            return dataAtual.getDate();
    });


    self.jaFoiAoDiaAtual = false;

    self.irAoDiaAtual = function () {
        if (!self.jaFoiAoDiaAtual) {
            var larguraColunaDia = $("#tableResumo>thead>tr>th:nth-of-type(2)").outerWidth();

            $("#tableResumo").parent().scrollLeft((self.diaAtual() - 3) * larguraColunaDia);

            self.jaFoiAoDiaAtual = true;
        }
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

var movimentacoesResumoViewModel = new MovimentacoesResumoViewModel()
ko.applyBindings(movimentacoesResumoViewModel);
