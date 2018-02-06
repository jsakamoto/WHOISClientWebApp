import { Component, OnInit } from '@angular/core';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie';
import * as $ from 'jquery';

interface WhoisResponse {
    RespondedServers: string[];
    Raw: string;
    OrganizationName: string;
    AddressRange: { Begin: string; End: string };
}

@Component({
    selector: 'app',
    templateUrl: 'app.component.html'
})
export class AppComponent implements OnInit {

    encodings: string[] = [];

    query: string = '';

    server: string = '';

    encoding: string;

    response: WhoisResponse | null = null;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private cookies: CookieService,
        private http: HttpClient
    ) {
        this.encoding = this.cookies.get('encoding') || 'us-ascii';
    }

    ngOnInit(): void {
        this.initEncodings();
        this.route.queryParams.subscribe(qParams => this.onChangeQueryParams(qParams));
    }

    async initEncodings(): Promise<void> {
        this.encodings = await this.http.get<string[]>('/api/encodings').toPromise();
        setTimeout(() => {
            let data = this.encodings.reduce((d, enc) => (d[enc] = null, d), {} as { [key: string]: null });
            ($('input#encoding') as any).autocomplete({
                data: data,
                limit: 20, // The max amount of results that can be shown at once. Default: Infinity.
                minLength: 0, // The minimum length of the input for the autocomplete to start. Default: 1.
                onAutocomplete: (val: string) => { this.encoding = val; }
            });
        }, 0);

        // Work around: Hide autocomplete popup for IE/Edge.
        const $input = $('input.autocomplete');
        $input.on('change keydown keyup paste blur focus mousedown touch', (a1, a2, a3) => {
            setTimeout(() => {
                const $ul = $(a1.target).closest('.input-field').children('ul.autocomplete-content.dropdown-content');
                const itemCount = $('li', $ul).length;
                if (itemCount == 0) $ul.hide(); else $ul.show();
            }, 0);
        });
    }

    async onChangeQueryParams(qParams: { [key: string]: string }): Promise<void> {
        this.query = qParams.query || '';
        this.server = qParams.server || '';
        this.encoding = qParams.encoding || this.encoding;

        if (this.query != '' && this.encoding != '') {
            this.response = null;
            const url = `api/whois/${encodeURIComponent(this.query)}?encoding=${encodeURIComponent(this.encoding)}&server=${encodeURIComponent(this.server)}`;
            this.response = await this.http.get<WhoisResponse>(url).toPromise();
        }
    }

    executeQuery(form: FormControl): void {
        if (form.invalid) return;

        this.cookies.put('encoding', this.encoding, { expires: 'Thu, 30 Dec 9999 15:00:00 GMT' });
        this.router.navigate([], {
            queryParams: { query: this.query, server: this.server, encoding: this.encoding }
        });
    }
}