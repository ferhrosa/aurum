// Configura a linguagem para a biblioteca Moment.
moment.lang("pt-br");

// Configura o formato padrão de datas para o Moment.
moment.defaultFormat = "DD/MM/YYYY";
moment.formatoApi = "YYYY-MM-DD";

// Configurações padrões do datepicker.
$.fn.datepicker.defaults.language = "pt-BR";
$.fn.datepicker.defaults.format = "dd/mm/yyyy";
$.fn.datepicker.defaults.autoclose = true;
$.fn.datepicker.defaults.todayBtn = "linked";
$.fn.datepicker.defaults.todayHighlight = true;

// Altera a validação padrão de campos de data.
$.validator.methods["date"] = function (value, element) {
    return this.optional(element) || moment(value, "DD/MM/YYYY").isValid();
}


// Criação de "binding" do Knockout para elementos com opção "checked", para observables do tipo boolean.
ko.bindingHandlers.checkedBoolean =
{
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        // Função para o evento "change" do elemento sendo tratado.
        var changeHandler = function () {
            var elementValue = $(element).val();
            var observable = valueAccessor();

            if (!element.checked)
                // Se o elemento estiver sendo desmarcado (no caso de checkbox), grava null no observable relacionado a ele.
                observable("");
            else
                // Altera o observable relacionado ao elemento para o "value" dele.
                observable($.parseJSON(elementValue));
        };

        // Registra a função para o evento "change" do elemento.
        ko.utils.registerEventHandler(element, "change", changeHandler);
    },

    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var elementValue = $.parseJSON($(element).val());
        var observableValue = ko.utils.unwrapObservable(valueAccessor());

        if (elementValue === observableValue)
            element.checked = true;
        else
            element.checked = false;
    }
};

function TratarExceptionNegocios(resultadoAjax, listaErros, mensagemGenerica) {
    if (resultadoAjax.responseJSON
        && resultadoAjax.responseJSON.ExceptionType == "Aurum.Negocio.Exceptions.NegociosException"
        && resultadoAjax.responseJSON.ExceptionMessage
    ) {
        var mensagens = resultadoAjax.responseJSON.ExceptionMessage.split("|");
        for (var i = 0; i < mensagens.length; i++)
            if (mensagens[i]) listaErros.push(mensagens[i]);
    }
    else if (mensagemGenerica)
        listaErros.push(mensagemGenerica);
    else
        listaErros.push("Ocorreu um erro ao realizar essa operação.");
}

// Aplica máscara de moeda (R$) ao valor informado, e retorna o texto com a máscara.
function formatarMoeda(valor, valorPadrao) {
    if (valor && (valor == parseFloat(valor)))
        return "R$ " + parseFloat(valor).toFixed(2).replace(/\./, ",");
    else if (valorPadrao)
        return valorPadrao;
    else
        return "";
}

function GerarObservableData(objeto, campo) {
    return ko.observable(
        objeto && objeto[campo] && moment(objeto[campo], moment.formatoApi).isValid()
        ? moment(objeto[campo]).format(moment.formatoApi)
        : "");
}

function GerarComputedDataFormatada(campoObservable) {
    return ko.computed({
        read: function () {
            return (
                campoObservable && campoObservable()) && moment(campoObservable(), moment.formatoApi).isValid()
                ? moment(campoObservable(), moment.formatoApi).format()
                : (
                    campoObservable && campoObservable()
                    ? campoObservable()
                    : "");
        },
        write: function (valor) {
            campoObservable(
                moment(valor, moment.defaultFormat).isValid()
                ? moment(valor, moment.defaultFormat).format(moment.formatoApi)
                : valor);
        }
    });
}

function ZeroEsquerda(valor, tamanho) {
    var valorTexto = valor.toString();
    while (valorTexto.length < tamanho) valorTexto = "0" + valorTexto;
    return valorTexto;
}
