var Controllers;
(function (Controllers) {
    var MovimentacoesResumoController = (function () {
        function MovimentacoesResumoController($scope, homeService) {
            this.lista = [];
            this.scope = $scope;
            this.homeSvc = homeService;
            this.scope.homeSvc = this.homeSvc;
            this.homeSvc.paginaAtual = "Movimentações - Resumo";
            console.dir(this);
        }
        return MovimentacoesResumoController;
    }());
    Controllers.MovimentacoesResumoController = MovimentacoesResumoController;
})(Controllers || (Controllers = {}));
//# sourceMappingURL=movimentacoesresumocontroller.js.map