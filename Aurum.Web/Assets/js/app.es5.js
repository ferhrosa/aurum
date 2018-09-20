/// <reference path="../../typings/tsd.d.ts" />
/// <reference path="controllers/homecontroller.ts" />
/// <reference path="controllers/movimentacoesresumocontroller.ts" />
/// <reference path="controllers/cadastrocontacontroller.ts" />
"use strict";

var appModulo = angular.module("testeApp", ["ngRoute", "ngMaterial"]);
//appModulo.controller("HomeController", ($scope, $mdSidenav) => new Controllers.HomeController($scope, $mdSidenav));
appModulo.controller("HomeController", ["$scope", "$mdSidenav", Controllers.HomeController]);
appModulo.controller("MovimentacoesResumoController", ["$scope", Controllers.MovimentacoesResumoController]);
appModulo.controller("CadastroContaController", ["$scope", Controllers.CadastroContaController]);
//appModulo.controller("HomeController", Controllers.HomeController);
//appModulo.controller("MovimentacoesResumoController", Controllers.MovimentacoesResumoController);
//appModulo.controller("CadastroContaController", Controllers.CadastroContaController);
appModulo.config(["$locationProvider", "$routeProvider", "$mdThemingProvider", function ($locationProvider, $routeProvider, $mdThemingProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider.when("/", {
        templateUrl: "/template/movimentacoes/resumo",
        controller: "MovimentacoesResumoController"
    }).when("/cadastros/conta", {
        templateUrl: "/template/CadastroContas/Index",
        controller: "CadastroContaController"
    }).otherwise({
        redirectTo: "/"
    });
    $mdThemingProvider.theme("default").primaryPalette("blue").accentPalette("purple");
}]);

