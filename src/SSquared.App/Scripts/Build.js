const { build } = require("esbuild");

buildFile("AddEmployee", "Scripts/AddEmployee.tsx", "/wwwroot/js/AddEmployee.js");
buildFile("EmployeeList", "Scripts/EmployeeList.tsx", "/wwwroot/js/EmployeeList.js");
buildFile("OrgChart", "Scripts/OrgChart.tsx", "/wwwroot/js/OrgChart.js");
buildFile("UpdateEmployee", "Scripts/UpdateEmployee.tsx", "/wwwroot/js/UpdateEmployee.js");

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