const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const allModules = [
    '@angular/common',
    '@angular/compiler',
    '@angular/core',
    '@angular/forms',
    '@angular/http',
    '@angular/platform-browser',
    '@angular/platform-browser-dynamic',
    '@angular/router',
    '@ngx-loading-bar/http-client',
    'ngx-cookie',
    'reflect-metadata',
    'rxjs',
    'zone.js',
    'es6-promise',
    'es6-shim',
    'event-source-polyfill'
];

module.exports = (env) => {
    const extractCSS = new ExtractTextPlugin('vendor.css');
    const isDevBuild = !(env && env.prod);
    const clientBundleConfig = {
        entry: { vendor: allModules },
        resolve: { extensions: ['.js'] },
        module: {
            rules: [
                { test: /\.css(\?|$)/, use: extractCSS.extract({ use: isDevBuild ? 'css-loader' : 'css-loader?minimize' }) }
            ]
        },
        output: {
            path: path.join(__dirname, 'wwwroot', 'scripts'),
            publicPath: 'scripts/',
            filename: '[name].js',
            library: '[name]_[hash]'
        },
        plugins: [
            new webpack.ContextReplacementPlugin(/\@angular\b.*\b(bundles|linker)/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/11580
            new webpack.ContextReplacementPlugin(/angular(\\|\/)core(\\|\/)@angular/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/14898
            new webpack.IgnorePlugin(/^vertx$/), // Workaround for https://github.com/stefanpenner/es6-promise/issues/100
            extractCSS,
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'scripts', '[name]-manifest.json'),
                name: '[name]_[hash]'
            })
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    };

    return clientBundleConfig;
}
