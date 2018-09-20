

var base = {

    endereco: '',
    conteudo: null,

    Verificar: function () {
        if (base.endereco != window.location.href)
            base.Carregar();

        base.endereco = window.location.href;

        setTimeout('base.Verificar()', 0);
    },

    Carregar: function () {
        //carregandoPagina.style.display = '';
        Aurum.Web.Pagina.Carregar(window.location.href, base.Carregar_Sucesso, base.Carregar_Falha);
    },

    Carregar_Sucesso: function (dados) {
        base.conteudo.html(dados);
    },

    Carregar_Falha: function (a, b) {
        alert(a);
        alert(b);
    }

}


$(function () {

    base.conteudo = $('#conteudo');

    base.Verificar();

});
