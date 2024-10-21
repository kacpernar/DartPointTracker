module.exports = {
    globDirectory: "release/wwwroot/",
    globPatterns: [
        '**/*.{html,webmanifest,json,js,css,svg,png,ico,wasm,dll}'
    ],
    swSrc: 'release/wwwroot/sw.js',
    swDest: 'release/wwwroot/sw.js',
};