function Grid(id)
{
    /// <summary> Transforma um elemento TABLE em um controle de grid de dados. </summary>
    /// <param name="id" type="String">ID do elemento que será transformado.</param>


    // [ CONSTRUÇÃO DO OBJETO - INÍCIO ]

    // Se não for informado o ID do elemento TABLE, gera erro.
    if (!id)
        throw 'O ID não foi informado ou não foi encontrado elemento com o ID informado.';

    // Carrega o elemento que possui o ID informado.
    var table = $('#' + id);

    // Se o elemento não for da tag TABLE, gera erro.
    if (table[0].tagName.toLowerCase() != 'table')
        throw 'O elemento que possui o ID informado deve ser do tipo "table".';

    // Adiciona a classe CSS 
    table.attr('class', 'grid');

    // [ CONSTRUÇÃO DO OBJETO - FIM ]


    // [ VARIÁVEIS INTERNAS - INÍCIO ]

    // Lista de campos
    var campos = [];

    // CheckBox do cabeçaho, na primeira coluna.
    var chkCabecalho = $(null);

    var todosSelecionados = false;

    // [ VARIÁVEIS INTERNAS - FIM ]


    function SelecionarTodos(e)
    {
        if (todosSelecionados)
        {
            chkCabecalho.removeAttr('checked');
            $('tbody > tr input[name="chkSelecionar"]', table).removeAttr('checked');
            $('tbody > tr', table).removeClass('selecionada');
        }
        else
        {
            chkCabecalho.attr('checked', 'checked');
            $('tbody > tr input[name="chkSelecionar"]', table).attr('checked', 'checked');
            $('tbody > tr', table).addClass('selecionada');
        }

        todosSelecionados = !todosSelecionados;

        AtualizarBotoesCadastro();
    }

    function RegistroSelecionado(e)
    {
        var limitePesquisa = 5;

        var elemento = $(e.target);

        if (elemento.attr('name') != 'chkSelecionar')
            $('tr', table).removeClass('selecionada');

        var linha = elemento;

        while (limitePesquisa > 0 && linha[0].tagName.toLowerCase() != 'tr')
        {
            linha = linha.parent();
            limitePesquisa--;
        }

        if (linha.hasClass('selecionada'))
        {
            linha.removeClass('selecionada');
            $('input[name="chkSelecionar"]', linha).removeAttr('checked');

            // Se todas as linhas estivessem selecionadas, altera o valor da variável
            // que diz isso e desmarca a seleção da checkbox do cabeçalho.
            if (todosSelecionados)
            {
                todosSelecionados = false;
                chkCabecalho.removeAttr('checked');
            }
        }
        else
        {
            linha.addClass('selecionada');
            $('input[name="chkSelecionar"]', linha).attr('checked', 'checked');
        }

        AtualizarBotoesCadastro();
    }

    this.LimparSelecao = function ()
    {
        todosSelecionados = true;
        SelecionarTodos();
    }


    function AtualizarBotoesCadastro()
    {
        if (cadastro)
            cadastro.AtualizarSituacaoBotoes();
    }


    // Preenche a grid com a lista de registros informada.
    this.Preencher = function (registros)
    {
        // É executado de forma assíncrona para não "travar" a tela enquanto carrega os registros.
        setTimeout(function ()
        {
            // Limpa a lista de campos armazenada.
            campos = [];
            // Limpa os controles de dentro da grid.
            table.empty();

            // Só continua caso haja algum registro.
            if (registros.length)
            {
                // Carrega a lista de campos dos registros.
                for (var campo in registros[0])
                    campos[campos.length] = campo;

                // Cria os elementos THEAD e TR para compor o cabeçalho.
                var thead = $('<thead>');
                var trCabecalho = $('<tr>');
                thead.appendTo(table);
                trCabecalho.appendTo(thead);

                // Adiciona a checkbox à primeira coluna do cabeçalho.
                var thCheckBox = $('<th>');
                thCheckBox.appendTo(trCabecalho);
                thCheckBox.click(SelecionarTodos);

                chkCabecalho = $('<input />', { type: 'checkbox' });
                chkCabecalho.appendTo(thCheckBox);

                // Cria o cabeçalho.
                for (var i = 0; i < campos.length; i++)
                {
                    var th = $('<th>');
                    th.appendTo(trCabecalho);
                    th.text(campos[i]);
                }

                // Cria corpo da tabela.
                var tbody = $('<tbody>');
                tbody.appendTo(table);

                var alternativa = false;

                for (var i = 0; i < registros.length; i++)
                {
                    // Cria a linha para o registro.
                    var tr = $('<tr>');
                    tr.appendTo(tbody);
                    tr.click(RegistroSelecionado);

                    // Verifica se a linha deve conter estilo alternativa.
                    if (alternativa) tr.addClass('alternativa');
                    alternativa = !alternativa;

                    // Cria a checkbox da primeira coluna.
                    var tdCheckBox = $('<td>', { name: 'chkSelecionar' });
                    var chkRegistro = $('<input/>', { type: 'checkbox', name: 'chkSelecionar' });
                    tdCheckBox.appendTo(tr);
                    chkRegistro.appendTo(tdCheckBox);

                    for (var j = 0; j < campos.length; j++)
                    {
                        var td = $('<td>');
                        td.appendTo(tr);

                        var valor = registros[i][campos[j]];

                        if (valor === true || valor === false)
                        {
                            var chkValor = $('<input/>', { type: 'checkbox', disabled: 'disabled' });
                            chkValor.attr('checked', valor);
                            chkValor.appendTo(td);
                            td.addClass('centralizada');
                        }
                        else
                        {
                            td.text(valor);
                        }
                    }
                }
            }
        },
        0,
        null);
    }

    this.QuantidadeSelecionados = function ()
    {
        return $('tbody > tr.selecionada', table).length;
    }

}
