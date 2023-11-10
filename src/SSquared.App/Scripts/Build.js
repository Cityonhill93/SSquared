const { build } = require("esbuild");

build({
    entryPoints: ["Scripts/EmployeeList.tsx"],
    bundle: true,
    minify: true,
    platform: 'node', // for CJS
    outfile: "/wwwroot/js/EmployeeList.js",
});