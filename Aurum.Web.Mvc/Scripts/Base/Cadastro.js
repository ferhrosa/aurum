var cadastro = new Cadastro();

function Cadastro()
{

    var editando = false;

    var btnInserir;
    var btnAlterar;
    var btnExcluir;
    var btnSalvar;
    var btnCancelar;

    var grid;

    this.Inicializar = function (registros)
    {
        btnInserir = $('#btnCadastroInserir');
        btnAlterar = $('#btnCadastroAlterar');
        btnExcluir = $('#btnCadastroExcluir');
        btnSalvar = $('#btnCadastroSalvar');
        btnCancelar = $('#btnCadastroCancelar');

        btnInserir.click(btnInserir_Click);
        btnAlterar.click(cadastro.IniciarEdicao);
        btnCancelar.click(cadastro.FinalizarEdicao);

        grid = new Grid('gridCadastro');
        grid.Preencher(registros);

        cadastro.AtualizarSituacaoBotoes();
    }

    function btnInserir_Click()
    {
        grid.LimparSelecao();

        cadastro.IniciarEdicao();
    }

    this.IniciarEdicao = function ()
    {
        editando = true;
        cadastro.AtualizarSituacaoBotoes();
    }

    this.FinalizarEdicao = function ()
    {
        editando = false;
        cadastro.AtualizarSituacaoBotoes();
    }

    this.AtualizarSituacaoBotoes = function ()
    {
        if (editando)
        {
            btnInserir.attr('disabled', 'disabled');
            btnExcluir.attr('disabled', 'disabled');
            btnSalvar.removeAttr('disabled');
            btnCancelar.removeAttr('disabled');
        }
        else
        {
            btnInserir.removeAttr('disabled');
            btnExcluir.removeAttr('disabled');
            btnSalvar.attr('disabled', 'disabled');
            btnCancelar.attr('disabled', 'disabled');
        }

        if (editando || grid.QuantidadeSelecionados() != 1)
            btnAlterar.attr('disabled', 'disabled');
        else
            btnAlterar.removeAttr('disabled');
    }

}