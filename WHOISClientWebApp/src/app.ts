import * as angular from 'angular';
import 'angular-resource';
import 'angular-cookies';
import 'angular-loading-bar';
import '../node_modules/angular-loading-bar/src/loading-bar.css';

interface WhoisResponse {
    RespondedServers: string[];
    Raw: string;
    OrganizationName: string;
    AddressRange: { Begin: string; End: string };
}

const app = angular.module('app', ['ngResource', 'ngCookies', 'angular-loading-bar']);

app.config(['cfpLoadingBarProvider', (cfpLoadingBarProvider: any) => {
    cfpLoadingBarProvider.includeSpinner = false;
}]);

class MainController {

    private whoisApi: ng.resource.IResourceClass<WhoisResponse>;

    public encodings: string[];

    public query: string;

    public server: string;

    public encoding: string;

    public response: WhoisResponse;

    constructor(
        $scope: ng.IScope,
        $resource: ng.resource.IResourceService,
        private $cookies: ng.cookies.ICookiesService,
        private $location: ng.ILocationService
    ) {
        this.encoding = $cookies.get('encoding') || 'us-ascii';

        this.whoisApi = $resource<WhoisResponse>('/api/whois/:query');
        this.encodings = $resource<string>('/api/encodings').query(() => {
            setTimeout(() => { ($('select') as any).material_select(); }, 0);
        });

        $scope.$watch(() => $location.url(), () => {
            var urlParams = $location.search();
            this.query = urlParams.query;
            this.server = urlParams.server;
            this.encoding = urlParams.encoding || this.encoding;

            if ((this.query || '') != '') {
                this.response = this.whoisApi.get({
                    query: this.query,
                    server: this.server,
                    encoding: this.encoding
                });
            }
        });
    }

    public executeQuery(isValidForm: boolean): void {
        if (isValidForm == false) return;

        this.$cookies.put('encoding', this.encoding, { expires: 'Thu, 30 Dec 9999 15:00:00 GMT' });

        this.$location.search({
            query: this.query,
            server: this.server,
            encoding: this.encoding
        });
    }
}

app.controller('mainController', ['$scope', '$resource', '$cookies', '$location', MainController])
