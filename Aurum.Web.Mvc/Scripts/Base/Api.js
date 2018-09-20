var api = new Api();


function Api()
{
    /// <summary>
    /// Classe usada para chamadas à web api.
    /// </summary>

    // Constante que diz o caminho da api de dados web dentro do aplicativo.
    var urlDados = window.urlBase + 'Dados/';

    // Constante que diz o caminho da api de páginas web dentro do aplicativo.
    var urlPaginas = window.urlBase + 'Paginas/';

    // Prefixo dos controllers de entidades.
    var urlEntidades = urlDados + 'Entidade';


    // TODO: Criar essas propriedades para funções para exibir e ocultar mensagem de "Carregando".
    //this.FuncaoCarregandoInicio = function () { };
    //this.FuncaoCarregandoFim = function () { };


    var paginasCarregarSucesso = function () { };

    // Contém funções para carregar dados referentes a índices do site.
    this.Paginas = {

        Carregar: function (funcaoSucesso)
        {
            paginasCarregarSucesso = funcaoSucesso;

            var caminho = window.location.hash;

            if (caminho) caminho = caminho.substring(1, caminho.length);
            if (caminho.substring(0, 1) == '/') caminho = caminho.substring(1, caminho.length);

            $.getJSON(urlPaginas + (caminho ? caminho : ''), PaginaCarregarSucesso);
        }

    }

    function PaginaCarregarSucesso(pagina)
    {
        // Altera o título da página atual.
        if (pagina.Titulo)
            document.title = window.tituloBase + ' - ' + pagina.Titulo;
        else
            document.title = window.tituloBase;

        // Carrega todos os templates utilizados pela página carregada.
        if (pagina.Templates && pagina.Templates.length)
            template.Carregar(pagina.Templates);

        // Executa o método (delegate) passado como parâmetro ao carregar a página.
        paginasCarregarSucesso.call(null, pagina);
    }


    // Contém funções para carregar dados referentes a entidades.
    this.Entidades = {

        Listar: function (nomeEntidade, funcaoSucesso)
        {
            return $.getJSON(urlEntidades + nomeEntidade, funcaoSucesso);
        },

        Carregar: function (codigo, funcaoSucesso)
        {
            return $.getJSON(urlEntidades + nomeEntidade, funcaoSucesso);
        }

    }
}
