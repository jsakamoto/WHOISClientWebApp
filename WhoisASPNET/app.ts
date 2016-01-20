namespace WhoisASPNET {
    export class MainController {
        public greeting: string;

        constructor() {
            this.greeting = 'Hey Jude.';
        }
    }

    angular
        .module('app', [])
        .controller('mainController', MainController);
}