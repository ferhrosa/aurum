/// <reference path="~/Scripts/jquery-1.7.1.js" />


function Grid(id, exibirSelecao, idBotaoAlterar, idBotaoExcluir)
{
    /// <summary> Transforma um elemento TABLE em um controle de grid de dados. </summary>
    /// <param name="id" type="String">ID do elemento que será transformado.</param>


    // Se não for informado o ID do elemento TABLE, gera erro.
    if (!id)
        throw 'O ID não foi informado ou não foi encontrado elemento com o ID informado.';

    var instancia = this;

    // Carrega o elemento que possui o ID informado.
    var table = $(null);

    // Lista de campos
    var campos = [];

    // CheckBox do cabeçaho, na primeira coluna.
    var chkCabecalho = $(null);

    var todosSelecionados = false;


    // Botões do cadastro.
    var btnAlterar = $(null);
    var btnExcluir = $(null);


    // Método para inicializar o componente.
    $(document).ready( function ()
    {
        // Se não for informado o ID do elemento TABLE, gera erro.
        table = $('#' + id);

        if (table.length && exibirSelecao)
        {
            // Se o elemento não for da tag TABLE, gera erro.
            if (table[0].tagName.toLowerCase() != 'table')
                throw 'O elemento que possui o ID informado deve ser do tipo "table".';

            // Carrega o elemento TR do THEAD.
            var trCabecalho = $('thead > tr', table);

            // Carrega a checkbox da primeira coluna do cabeçalho.
            var thCheckBox = $('th:first', trCabecalho);
            thCheckBox.click(SelecionarTodos);

            chkCabecalho = $('input[type="checkbox"]', thCheckBox);

            // Carrega o corpo da tabela.
            var tbody = $('tbody', table);

            $('tr', tbody).each(function (i, item)
            {
                // Carrega a linha do registro.
                var tr = $(this);
                tr.click(RegistroSelecionado);
                tr.dblclick(AlterarRegistro);

                // Cria a checkbox da primeira coluna.
                var tdCheckBox = $('td:first', tr);
                tdCheckBox.attr('name', 'chkSelecionar');

                var chkRegistro = $('input[type="checkbox"]', tdCheckBox);
                chkRegistro.attr('nome', 'chkSelecionar');

                if (chkRegistro.is(':checked'))
                    tr.addClass('selecionada');
            });
        }

        if (idBotaoAlterar) btnAlterar = $('#' + idBotaoAlterar);
        if (idBotaoExcluir) btnExcluir = $('#' + idBotaoExcluir);

        AtualizarBotoesCadastro();
    },
    0, null); // Final do setTimeout.


    this.QuantidadeSelecionados = function ()
    {
        return $('tbody > tr.selecionada', table).length;
    }


    function SelecionarTodos(e)
    {
        if (todosSelecionados)
        {
            chkCabecalho.removeAttr('checked');
            $('tbody > tr input[nome="chkSelecionar"]', table).removeAttr('checked');
            $('tbody > tr', table).removeClass('selecionada');
        }
        else
        {
            chkCabecalho.attr('checked', 'checked');
            $('tbody > tr input[nome="chkSelecionar"]', table).attr('checked', 'checked');
            $('tbody > tr', table).addClass('selecionada');
        }

        todosSelecionados = !todosSelecionados;

        AtualizarBotoesCadastro();
    }

    function RegistroSelecionado(e)
    {
        var limitePesquisa = 5;

        var elemento = $(e.target);

        if (elemento.attr('nome') != 'chkSelecionar' & elemento.attr('name') != 'chkSelecionar')
        {
            $('tr', table).removeClass('selecionada');
            $('input[nome="chkSelecionar"]', table).removeAttr('checked');
        }

        var linha = elemento;

        while (limitePesquisa > 0 && linha[0].tagName.toLowerCase() != 'tr')
        {
            linha = linha.parent();
            limitePesquisa--;
        }

        if (linha.hasClass('selecionada'))
        {
            linha.removeClass('selecionada');
            $('input[nome="chkSelecionar"]', linha).removeAttr('checked');
        }
        else
        {
            linha.addClass('selecionada');
            $('input[nome="chkSelecionar"]', linha).attr('checked', 'checked');
        }

        // Se todas as linhas estivessem selecionadas, altera o valor da variável
        // que diz isso e desmarca a seleção da checkbox do cabeçalho.
        if (todosSelecionados)
        {
            todosSelecionados = false;
            chkCabecalho.removeAttr('checked');
        }


        AtualizarBotoesCadastro();
    }

    function AlterarRegistro(e)
    {
        if (instancia.QuantidadeSelecionados() == 1 && idBotaoAlterar)
        {
            btnAlterar.click();
        }
    }

    this.LimparSelecao = function ()
    {
        todosSelecionados = true;
        SelecionarTodos();
    }

    function AtualizarBotoesCadastro()
    {
        if (idBotaoAlterar)
        {
            if (instancia.QuantidadeSelecionados() != 1)
                btnAlterar.attr('disabled', 'disabled');
            else
                btnAlterar.removeAttr('disabled');
        }

        if (idBotaoExcluir)
        {
            if (instancia.QuantidadeSelecionados())
                btnExcluir.removeAttr('disabled');
            else
                btnExcluir.attr('disabled', 'disabled');
        }
    }
}
