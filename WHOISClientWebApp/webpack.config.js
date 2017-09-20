module.exports = {
    entry: ['./src/app.ts'],
    output: {
        filename: './wwwroot/bundle.js'
    },
    module: {
        loaders: [
            { test: /\.ts$/, loader: 'ts-loader' },
            { test: /\.css$/, loaders: ['style-loader', 'css-loader'] }
        ]
    },
    devtool: 'source-map'
};