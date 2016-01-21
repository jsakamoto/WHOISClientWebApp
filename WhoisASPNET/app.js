var WhoisASPNET;
(function (WhoisASPNET) {
    var MainController = (function () {
        function MainController($resource) {
            this.encoding = 'us-ascii';
            this.whoisApi = $resource('/api/whois/:query');
            this.encodings = $resource('/api/encodings').query(function () {
                setTimeout(function () { $('select').material_select(); }, 0);
            });
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
