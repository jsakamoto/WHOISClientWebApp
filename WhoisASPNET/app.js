var WhoisASPNET;
(function (WhoisASPNET) {
    var MainController = (function () {
        function MainController($resource) {
            this.encoding = 'us-ascii';
            this.query = '219.165.73.19';
            this.whoisApi = $resource('/api/whois/:query');
            this.encodings = $resource('/api/encodings').query();
        }
        MainController.prototype.executeQuery = function () {
            this.response = this.whoisApi.get({
                query: this.query,
                server: this.server,
                encoding: this.encoding
            });
        };
        return MainController;
    })();
    WhoisASPNET.MainController = MainController;
    angular
        .module('app', ['ngResource'])
        .controller('mainController', MainController);
})(WhoisASPNET || (WhoisASPNET = {}));
//# sourceMappingURL=app.js.map