var WhoisASPNET;
(function (WhoisASPNET) {
    var MainController = (function () {
        function MainController($resource, $cookies) {
            this.$cookies = $cookies;
            this.encoding = $cookies.get('encoding') || 'us-ascii';
            this.whoisApi = $resource('/api/whois/:query');
            this.encodings = $resource('/api/encodings').query(function () {
                setTimeout(function () { $('select').material_select(); }, 0);
            });
        }
        MainController.prototype.executeQuery = function (isValidForm) {
            if (isValidForm == false)
                return;
            this.$cookies.put('encoding', this.encoding, { expires: 'Thu, 30 Dec 9999 15:00:00 GMT' });
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
        .module('app', ['ngResource', 'ngCookies'])
        .controller('mainController', MainController);
})(WhoisASPNET || (WhoisASPNET = {}));
