var WHOISClientWebApp;
(function (WHOISClientWebApp) {
    var MainController = (function () {
        function MainController($scope, $resource, $cookies, $location) {
            var _this = this;
            this.$cookies = $cookies;
            this.$location = $location;
            this.encoding = $cookies.get('encoding') || 'us-ascii';
            this.whoisApi = $resource('/api/whois/:query');
            this.encodings = $resource('/api/encodings').query(function () {
                setTimeout(function () { $('select').material_select(); }, 0);
            });
            $scope.$watch(function () { return $location.url(); }, function () {
                var urlParams = $location.search();
                _this.query = urlParams.query;
                _this.server = urlParams.server;
                _this.encoding = urlParams.encoding || _this.encoding;
                if ((_this.query || '') != '') {
                    _this.response = _this.whoisApi.get({
                        query: _this.query,
                        server: _this.server,
                        encoding: _this.encoding
                    });
                }
            });
        }
        MainController.prototype.executeQuery = function (isValidForm) {
            if (isValidForm == false)
                return;
            this.$cookies.put('encoding', this.encoding, { expires: 'Thu, 30 Dec 9999 15:00:00 GMT' });
            this.$location.search({
                query: this.query,
                server: this.server,
                encoding: this.encoding
            });
        };
        return MainController;
    }());
    WHOISClientWebApp.MainController = MainController;
    angular
        .module('app', ['ngResource', 'ngCookies'])
        .config(["$locationProvider", function ($locationProvider) { return $locationProvider.html5Mode(true); }])
        .controller('mainController', MainController);
})(WHOISClientWebApp || (WHOISClientWebApp = {}));
//# sourceMappingURL=app.js.map