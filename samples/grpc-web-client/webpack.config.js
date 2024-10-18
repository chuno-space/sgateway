const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin'); // Import the plugin

module.exports = {
  entry: './index.ts',
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, 'dist'),
    publicPath: '/', // Important for webpack-dev-server HMR
  },
  resolve: {
    extensions: ['.ts', '.js'],
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
  mode: 'development',
  devtool: 'inline-source-map', // Enable source maps for debugging
  devServer: {
    static: {
      directory: path.join(__dirname, 'dist'),
    },
    hot: true, // Enable HMR
    open: true, // Automatically open the browser
    port: 8080, // You can specify your preferred port here
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin(), // Add HMR plugin
    new HtmlWebpackPlugin({
        template: './index.html', // Use your existing index.html as a template
        filename: 'index.html', // Output file in 'dist'
      }),
  ],
};