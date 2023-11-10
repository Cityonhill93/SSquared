const { build } = require("esbuild");

build({
    entryPoints: ["Scripts/EmployeeList.tsx"],
    bundle: true,
    globalName:"EmployeeList",
    minify: true,   
    outfile: "/wwwroot/js/EmployeeList.js",
    platform: "browser",
    sourcemap:true
});