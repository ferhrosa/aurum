var Controllers;
(function (Controllers) {
    var CadastroContaController = (function () {
        function CadastroContaController($scope, homeService) {
            this.lista = [];
            this.scope = $scope;
            this.homeSvc = homeService;
            this.homeSvc.paginaAtual = "Cadastro de Contas";
            console.dir(this);
        }
        return CadastroContaController;
    }());
    Controllers.CadastroContaController = CadastroContaController;
})(Controllers || (Controllers = {}));
//# sourceMappingURL=cadastrocontacontroller.js.map