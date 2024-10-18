const path = require('path');

module.exports = {
  entry: './index.js', // The entry point of your application
  output: {
    filename: 'bundle.js', // The bundled file
    path: path.resolve(__dirname, 'dist')
  },
  mode: 'development',
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader', // Use Babel for transpiling if needed
        },
      },
    ],
  },
};