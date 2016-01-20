namespace WhoisASPNET {
    export class MainController {
        private whoisApi: ng.resource.IResourceClass<WhoisResponse>;

        public encodings: string[];

        public query: string;

        public server: string;

        public encoding: string;

        public response: WhoisResponse;

        constructor($resource: ng.resource.IResourceService) {
            this.encoding = 'us-ascii';
            this.query = '219.165.73.19';
            this.whoisApi = $resource<WhoisResponse>('/api/whois/:query');
            this.encodings = $resource<string>('/api/encodings').query();
        }

        public executeQuery(): void {
            this.response = this.whoisApi.get({
                query: this.query,
                server: this.server,
                encoding: this.encoding
            });
        }
    }

    angular
        .module('app', ['ngResource'])
        .controller('mainController', MainController);
}