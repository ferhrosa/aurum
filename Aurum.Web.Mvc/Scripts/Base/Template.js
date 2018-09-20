// Cria a instância da classe Template que será usada nas páginas.
var template = new Template();


function Template()
{
    /// <summary>
    /// Classe usada para carregar templates do servidor.
    /// </summary>

    // Constante que diz o caminho da api web dentro do aplicativo.
    var urlTemplates = window.urlBase + 'Templates/';


    // TODO: Criar essas propriedades para funções para exibir e ocultar mensagem de "Carregando".
    //this.FuncaoCarregandoInicio = function () { };
    //this.FuncaoCarregandoFim = function () { };

    this.Carregar = function (nomes)
    {
        $(nomes).each(function (i, nome)
        {
            // Somente carrega o template se ele não estiver ainda no "repositório".
            if (!$('#tmpl' + nome, '#templates').length)
            {
                $.ajax({
                    // Executa chamada síncrona.
                    async: false,

                    // URL do template.
                    url: urlTemplates + nome,

                    // Adiciona o template à página.
                    success: function (dados)
                    {
                        $('#templates').append(dados);
                    }
                });
            }
        });
    }
}
