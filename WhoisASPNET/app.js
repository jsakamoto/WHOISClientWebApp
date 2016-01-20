var WhoisASPNET;
(function (WhoisASPNET) {
    var MainController = (function () {
        function MainController() {
            this.greeting = 'Hey Jude.';
        }
        return MainController;
    })();
    WhoisASPNET.MainController = MainController;
    angular
        .module('app', [])
        .controller('mainController', MainController);
})(WhoisASPNET || (WhoisASPNET = {}));
//# sourceMappingURL=app.js.map