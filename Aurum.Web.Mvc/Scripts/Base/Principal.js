$(function ()
{
    $(window).hashchange(WindowHashChange);
    $(window).hashchange();
});


function WindowHashChange()
{
    //var hash = window.location.hash;

    $('#conteudo').empty();

    api.Paginas.Carregar(function (pagina)
    {
        switch (pagina.Tipo)
        {
            case 1: // Índice
                CarregarIndice(pagina.Indice);
                break;

            case 2: // Cadastro
                CarregarCadastro(pagina);
                break;

            case 9: // Outro
                // TODO: Carregar outros tipos.
                break;
        }
    });
}

function CarregarIndice(indice)
{
    /// <summary> Carrega os itens de um índice retornado pelo servidor. </summary>

    if (indice)
    {
        var grupo;

        $('#tmplIndiceBase').tmpl().appendTo('#conteudo');

        $.each(indice, function (i, item)
        {

            if (item.Grupo && grupo != item.Grupo)
                $('<h2>').html(item.Grupo).appendTo('#indice');

            $('#tmplIndice').tmpl(item).appendTo('#indice');

            grupo = item.Grupo;

        });
    }
}

function CarregarCadastro(pagina)
{
    $('#tmplCadastro').tmpl().appendTo('#conteudo');

    cadastro.Inicializar(pagina.Registros);
}