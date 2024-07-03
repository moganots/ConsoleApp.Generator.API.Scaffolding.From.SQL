using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates
{
    public class TemplateTsConfig
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectVersion { get; set; }
        public string? ProjectAuthor { get; set; }
        public string FileName => "tsconfig.json";
        public TemplateTsConfig() { }
        public TemplateTsConfig(
            string projectName,
            string projectDescription,
            string projectVersion,
            string projectAuthor) : this()
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            ProjectVersion = projectVersion;
            ProjectAuthor = projectAuthor;
        }
        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"compilerOptions\": {");
            sb.AppendLine("    \"pretty\": true,");
            sb.AppendLine("    \"target\": \"es6\" /* Set the JavaScript language version for emitted JavaScript and include compatible library declarations. */,");
            sb.AppendLine("    \"module\": \"NodeNext\" /* Specify what module code is Generated. */,");
            sb.AppendLine("    \"rootDir\": \"./src\" /* Specify the root folder within your source files. */,");
            sb.AppendLine("    \"moduleResolution\": \"NodeNext\",");
            sb.AppendLine("    \"sourceMap\": true /* Specify how TypeScript looks up a file from a given module specifier. */,");
            sb.AppendLine("    \"outDir\": \"./build\" /* Specify an output folder for all emitted files. */,");
            sb.AppendLine("    \"removeComments\": true /* Disable emitting comments. */,");
            sb.AppendLine("    /* Allow importing helper functions from tslib once per project, instead of including them per-file. */");
            sb.AppendLine("    \"importsNotUsedAsValues\": \"remove\" /* Specify emit/checking behavior for imports that are only used for types. */,");
            sb.AppendLine("    \"isolatedModules\": true /* Ensure that each file can be safely transpiled without relying on other imports. */,");
            sb.AppendLine("    // \"allowSyntheticDefaultImports\": true, /* Allow 'import x from y' when a module doesn't have a default export. */");
            sb.AppendLine("    \"esModuleInterop\": true,");
            sb.AppendLine("    \"forceConsistentCasingInFileNames\": true,");
            sb.AppendLine("    \"strict\": true /* Enable all strict type-checking options. */,");
            sb.AppendLine("    \"noImplicitAny\": true /* Enable error reporting for expressions and declarations with an implied 'any' type. */,");
            sb.AppendLine("    \"strictNullChecks\": true /* When type checking, take into account 'null' and 'undefined'. */,");
            sb.AppendLine("    \"strictFunctionTypes\": true /* When assigning functions, check to ensure parameters and the return values are subtype-compatible. */,");
            sb.AppendLine("    \"strictBindCallApply\": true /* Check that the arguments for 'bind', 'call', and 'apply' methods match the original function. */,");
            sb.AppendLine("    \"strictPropertyInitialization\": true /* Check for class properties that are declared but not set in the constructor. */,");
            sb.AppendLine("    \"noImplicitThis\": true /* Enable error reporting when 'this' is given the type 'any'. */,");
            sb.AppendLine("    // \"useUnknownInCatchVariables\": true, /* Default catch clause variables as 'unknown' instead of 'any'. */");
            sb.AppendLine("    \"alwaysStrict\": true /* Ensure 'use strict' is always emitted. */,");
            sb.AppendLine("    \"noUnusedLocals\": true /* Enable error reporting when local variables aren't read. */,");
            sb.AppendLine("    \"noUnusedParameters\": true /* Raise an error when a function parameter isn't read. */,");
            sb.AppendLine("    // \"exactOptionalPropertyTypes\": true, /* Interpret optional property types as written, rather than adding 'undefined'. */");
            sb.AppendLine("    \"noImplicitReturns\": true /* Enable error reporting for codepaths that do not explicitly return in a function. */,");
            sb.AppendLine("    \"noFallthroughCasesInSwitch\": true /* Enable error reporting for fallthrough cases in switch statements. */,");
            sb.AppendLine("    /* Skip type checking .d.ts files that are included with TypeScript. */");
            sb.AppendLine("    \"skipLibCheck\": true,");
            sb.AppendLine("    \"typeRoots\": [\"node_modules/@types\", \"./src/@types\"] /* Skip type checking all .d.ts or .js files. */");
            sb.AppendLine("  },");
            sb.AppendLine("  \"include\": [\"./src/**/*.ts\", \"./src/**/*.js\"],");
            sb.AppendLine("  \"exclude\": [\"node_modules\", \"**/*.spec.ts\", \"**/*.spec.js\", \"**/*.test.ts\", \"**/*.test.js\"]");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
