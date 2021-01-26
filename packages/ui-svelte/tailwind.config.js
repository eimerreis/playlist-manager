const production = !process.env.ROLLUP_WATCH;
module.exports = {
	future: {
		// for tailwind 2.0 compat
		purgeLayersByDefault: true,
		removeDeprecatedGapUtilities: true,
	},
	plugins: [
		// other plugins here
	],
	purge: {
		content: [
			"./src/**/*.svelte",
			// may also want to include base index.html
		],
		enabled: production, // disable purge in dev
	},
};
