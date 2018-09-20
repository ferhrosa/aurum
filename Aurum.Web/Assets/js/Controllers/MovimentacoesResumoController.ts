
module Controllers {

    export class MovimentacoesResumoController {

        private scope: any;
        public homeSvc: Services.HomeService;

        public lista: Array<string> = [];
        public itemAdicionando: string;

        constructor($scope: ng.IScope, homeService: Services.HomeService) {
            this.scope = $scope;
            this.homeSvc = homeService;

            this.scope.homeSvc = this.homeSvc;

            this.homeSvc.paginaAtual = "Movimentações - Resumo";
            console.dir(this);
        }

    }

}
