const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const config = {
        entry: { boot: './src/boot.ts' },
        resolve: {
            extensions: ['.js', '.ts']
        },
        output: {
            path: path.join(__dirname, 'wwwroot'),
            filename: 'bundle.js',
            publicPath: '/'
        },
        module: {
            rules: [
                { test: /\.ts$/, use: ['awesome-typescript-loader?silent=true', 'angular2-template-loader', 'angular2-router-loader'] },
                { test: /\.html$/, use: ['html-loader?minimize=false'] },
                {
                    test: /\.s?css$/,
                    use: ExtractTextPlugin.extract({
                        fallback: 'style-loader',
                        use: [isDevBuild ? 'css-loader' : 'css-loader?minimize', 'sass-loader']
                    })
                }
            ]
        },
        devtool: 'source-map',
        plugins: [
            new ExtractTextPlugin('./content/site.css'),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/scripts/vendor-manifest.json')
            })
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ]),
        externals: { jquery: 'jQuery' }
    };
    return config;
};
