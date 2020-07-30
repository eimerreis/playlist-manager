//eslint-disable-next-line
"use strict";

const webpack = require("webpack");
const path = require("path");

module.exports = {
	mode: "development",
	entry: "./src",
	output: {
		path: path.resolve(__dirname, "dist"),
		filename: "index.js",
	},
	resolve: {
		extensions: [".ts", ".tsx", ".js", ".json"],
	},
	target: "node",
	module: {
		rules: [
			{
				test: /\.(ts|js)$/,
				loader: "babel-loader",
				include: [__dirname, path.resolve(__dirname, "../shared")],
				exclude: /node_modules/,
				options: {
					babelrc: false,
					presets: [
						[
							"@babel/preset-env",
							{
								targets: {
									esmodules: true,
								},
							},
						],
						"@babel/preset-typescript",
					],
					plugins: [
						[
							"dotenv-import",
							{
								moduleName: "@env",
								path: ".env",
								blacklist: "null",
								whitelist: "null",
								safe: false,
								allowUndefined: false,
							},
						],
						"@babel/plugin-proposal-class-properties",
					],
				},
			},
		],
	},
	// using this because of https://github.com/node-formidable/formidable/issues/337
	plugins: [new webpack.DefinePlugin({ "global.GENTLY": false })],
};
