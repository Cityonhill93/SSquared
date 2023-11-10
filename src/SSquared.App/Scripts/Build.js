const { build } = require("esbuild");

buildFile("AddEmployee", "Scripts/AddEmployee.tsx", "/wwwroot/js/AddEmployee.js");
buildFile("EmployeeList", "Scripts/EmployeeList.tsx", "/wwwroot/js/EmployeeList.js");

function buildFile(globalName, sourcePath, targetPath) {
    build({
        entryPoints: [sourcePath],
        bundle: true,
        globalName: globalName,
        minify: true,
        outfile: targetPath,
        platform: "browser",
        sourcemap: true
    });
}