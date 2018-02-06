import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CookieModule } from 'ngx-cookie';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';

import { AppComponent } from './components/app/app.component';

import './site.scss';

@NgModule({
    bootstrap: [AppComponent],
    imports: [
        CommonModule,
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([]),
        LoadingBarHttpClientModule,
        CookieModule.forRoot()
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl },
    ],
    declarations: [
        AppComponent,
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
