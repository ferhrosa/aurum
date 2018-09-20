/// <reference path="../../../typings/tsd.d.ts" />
var Controllers;
(function (Controllers) {
    var HomeController = (function () {
        function HomeController($scope, $mdSidenav, homeService) {
            var _this = this;
            this.lista = [];
            this.alternarMenu = function () {
                //console.dir(this.scope);
                //console.dir(this.mdSidenav);
                console.dir(_this.mdSidenav("left"));
                console.dir(_this.mdSidenav("left").isOpen());
                _this.mdSidenav("left").toggle();
            };
            this.ocultarMenu = function () {
                _this.mdSidenav("left").close();
            };
            this.adicionarItem = function () {
                _this.lista.push(_this.lista.length + " - " + _this.itemAdicionando);
                _this.itemAdicionando = "";
            };
            this.scope = $scope;
            this.mdSidenav = $mdSidenav;
            this.homeSvc = homeService;
            console.dir(this);
        }
        return HomeController;
    }());
    Controllers.HomeController = HomeController;
})(Controllers || (Controllers = {}));
//# sourceMappingURL=homecontroller.js.map