
module Controllers {

    export class CadastroContaController {

        private scope: ng.IScope;
        public homeSvc: Services.HomeService;

        public lista: Array<string> = [];
        public itemAdicionando: string;

        constructor($scope: ng.IScope, homeService: Services.HomeService) {
            this.scope = $scope;
            this.homeSvc = homeService;

            this.homeSvc.paginaAtual = "Cadastro de Contas";
            console.dir(this);
        }

    }

}
