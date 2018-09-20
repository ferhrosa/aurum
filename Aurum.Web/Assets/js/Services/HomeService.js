/// <reference path="../../../typings/tsd.d.ts" />
var Services;
(function (Services) {
    var HomeService = (function () {
        function HomeService() {
        }
        HomeService.prototype.adicionarMovimentacao = function () {
            alert("Adicionar movimentação");
        };
        return HomeService;
    }());
    Services.HomeService = HomeService;
})(Services || (Services = {}));
//# sourceMappingURL=HomeService.js.map