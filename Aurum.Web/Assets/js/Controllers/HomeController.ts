/// <reference path="../../../typings/tsd.d.ts" />

module Controllers {

    export class HomeController {

        private scope: ng.IScope;
        private mdSidenav: ng.material.ISidenavService;
        public homeSvc: Services.HomeService;

        public lista: Array<string> = [];
        public itemAdicionando: string;


        constructor($scope: ng.IScope, $mdSidenav: ng.material.ISidenavService, homeService: Services.HomeService) {
            this.scope = $scope;
            this.mdSidenav = $mdSidenav;
            this.homeSvc = homeService;
            console.dir(this);
        }


        public alternarMenu = (): void => {
            //console.dir(this.scope);
            //console.dir(this.mdSidenav);
            console.dir(this.mdSidenav("left"));
            console.dir(this.mdSidenav("left").isOpen());
            this.mdSidenav("left").toggle();
        }

        public ocultarMenu = (): void => {
            this.mdSidenav("left").close();
        }

        public adicionarItem = (): void => {
            this.lista.push(`${this.lista.length} - ${this.itemAdicionando}`);
            this.itemAdicionando = "";
        }

    }

}
