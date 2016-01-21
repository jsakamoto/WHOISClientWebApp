namespace WhoisASPNET {
    export class MainController {
        private whoisApi: ng.resource.IResourceClass<WhoisResponse>;

        public encodings: string[];

        public query: string;

        public server: string;

        public encoding: string;

        public response: WhoisResponse;

        constructor($resource: ng.resource.IResourceService, private $cookies: ng.cookies.ICookiesService) {
            this.encoding = $cookies.get('encoding') || 'us-ascii';
            this.whoisApi = $resource<WhoisResponse>('/api/whois/:query');
            this.encodings = $resource<string>('/api/encodings').query(() => {
                setTimeout(() => { ($('select') as any).material_select(); }, 0);
            });
        }

        public executeQuery(isValidForm: boolean): void {
            if (isValidForm == false) return;

            this.$cookies.put('encoding', this.encoding, { expires: 'Thu, 30 Dec 9999 15:00:00 GMT' });
            this.response = this.whoisApi.get({
                query: this.query,
                server: this.server,
                encoding: this.encoding
            });
        }
    }

    angular
        .module('app', ['ngResource', 'ngCookies'])
        .controller('mainController', MainController);
}