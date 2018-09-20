/// <reference path="../../typings/tsd.d.ts" />
/// <reference path="controllers/homecontroller.ts" />
/// <reference path="controllers/movimentacoesresumocontroller.ts" />
/// <reference path="controllers/cadastrocontacontroller.ts" />


var appModulo = angular.module("testeApp", [
    "ngRoute",
    "ngMaterial"
]);

//appModulo.controller("HomeController", ($scope, $mdSidenav) => new Controllers.HomeController($scope, $mdSidenav));

appModulo.factory("homeService", () => new Services.HomeService());

appModulo.controller("HomeController", ["$scope", "$mdSidenav", "homeService", Controllers.HomeController]);
appModulo.controller("MovimentacoesResumoController", ["$scope", "homeService", Controllers.MovimentacoesResumoController]);
appModulo.controller("CadastroContaController", ["$scope", "homeService", Controllers.CadastroContaController]);

//appModulo.controller("HomeController", Controllers.HomeController);
//appModulo.controller("MovimentacoesResumoController", Controllers.MovimentacoesResumoController);
//appModulo.controller("CadastroContaController", Controllers.CadastroContaController);

appModulo.config(["$locationProvider", "$routeProvider", "$mdThemingProvider",
    ($locationProvider: ng.ILocationProvider, $routeProvider: ng.route.IRouteProvider, $mdThemingProvider: ng.material.IThemingProvider) => {

        $locationProvider.html5Mode(true);

        $routeProvider
            .when("/", {
                templateUrl: "/template/Movimentacoes/Resumo",
                controller: "MovimentacoesResumoController"
            })
            .when("/cadastros/conta", {
                templateUrl: "/template/CadastroContas/Index",
                controller: "CadastroContaController"
            })
            .otherwise({
                redirectTo: "/"
            });

        $mdThemingProvider.theme("default")
            .primaryPalette("blue")
            .accentPalette("orange");

    }
]);
