using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.routes
{
    public class TemplateRoutesJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? Directory { get; set; }
        public string? Routes { get; set; }
        public bool? IsGateway { get; set; }
        public bool? UsesAuthentication { get; set; }
        public string FilePath => Path.Combine(Directory!, "routes.js");
        public TemplateRoutesJS() { }
        public TemplateRoutesJS(string author, string dateCreated, string directory, string routes, bool isGateway = false, bool usesAuthentication = false) :this() {
            Author = author;
            DateCreated = dateCreated;
            Directory = directory;
            Routes = routes;
            IsGateway = isGateway;
            UsesAuthentication = usesAuthentication;
        }
        public void Generate() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine("| Description   : Main routes module");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine(" * Creates an instance of the main routing module");
            sb.AppendLine(" * @param {*} config an instance of the configuration module");
            sb.AppendLine(" * @param {*} logger an instance of the logging module");
            sb.AppendLine(" * @param {*} app an instance of the Express app");
            sb.AppendLine(" * @param {*} dbContext an instance of the DBContext");
            sb.AppendLine(" * @returns MainRouteModule");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = (config, logger, app, dbContext) => {");
            sb.AppendLine("  const { notSet } = require(`./../utils/general/object.util`)();");
            sb.AppendLine("  if (notSet(config)) {");
            sb.AppendLine("    throw new Error(`MainRouteModule - [config] instance has not been provided or set`); ");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(logger)) {");
            sb.AppendLine("    throw new Error(`MainRouteModule - [logger] instance has not been provided or set`);");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(app)) {");
            sb.AppendLine("    throw new Error(`MainRouteModule - [logger] instance has not been provided or set`);");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(dbContext)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `MainRouteModule - [dbContext] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("");
            if ((bool)IsGateway!)
            {
                sb.AppendLine("\tconst { createProxyMiddleware } = require(`http-proxy-middleware`);");
                sb.AppendLine("\tconst proxyRoutes = require(`./@proxy/routes`);");
                sb.AppendLine("");
            }
            sb.AppendLine("\t/*");
            sb.AppendLine("\t|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("\t| Route(s)");
            sb.AppendLine("\t|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("\t*/");
            sb.AppendLine("\tconst routeRoot = require(`./@core/root`)(config, logger, app);");
            if ((bool)UsesAuthentication!)
            {
                sb.AppendLine("\tconst routeAuthentication = require(`./route.authentication/api`)(config, logger, app, dbContext);");
            }
            if (Routes!.IsSet())
            {
                sb.AppendLine(Routes);
            }
            //if ((bool)IsGateway!)
            //{
            //    sb.AppendLine("  /*");
            //    sb.AppendLine("	|------------------------------------------------------------------------------------------------------------------");
            //    sb.AppendLine("	| Proxy Route(s)");
            //    sb.AppendLine("	|------------------------------------------------------------------------------------------------------------------");
            //    sb.AppendLine("	*/");
            //    sb.AppendLine("  proxyRoutes.forEach((proxyRoute) => {");
            //    sb.AppendLine("    router.use(proxyRoute.url, createProxyMiddleware(proxyRoute.proxy));");
            //    sb.AppendLine("  });");
            //}
            sb.AppendLine("");
            sb.AppendLine("\treturn app._router;");
            sb.AppendLine("};");
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
