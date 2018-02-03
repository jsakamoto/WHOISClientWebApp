import 'es6-promise/auto';
import 'es6-shim';
import 'event-source-polyfill/src/eventsource.min.js'
import 'reflect-metadata';
import 'zone.js';

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';

if (module.hot) {
    module.hot.accept();
    //module.hot.dispose(() => {
    //    // Before restarting the app, we create a new root element and dispose the old one
    //    const oldRootElem = document.querySelector('app');
    //    const newRootElem = document.createElement('app');
    //    oldRootElem!.parentNode!.insertBefore(newRootElem, oldRootElem);
    //    modulePromise.then(appModule => {
    //        appModule.destroy();

    //        // https://github.com/aspnet/JavaScriptServices/issues/1165#issuecomment-335984443
    //        if (oldRootElem !== null) {
    //            oldRootElem!.parentNode!.removeChild(oldRootElem);
    //        }
    //    });
    //});
} else {
    enableProdMode();
}

// Note: @ng-tools/webpack looks for the following expression when performing production
// builds. Don't change how this line looks, otherwise you may break tree-shaking.
const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
