using ConsoleApp.Generator.API.Scaffolding.From.SQL.configuration;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.routes
{
    public class TemplateProxyRoutesJS
    {
        public string? ProjectName { get; set; }
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? Directory { get; set; }
        public ProjectScaffoldingElement[]? ProxyRoutes { get; set; }
        public string FilePath => Path.Combine(Directory!, "routes.js");
        public string ApiGatewayName => ProjectName?.ToLower().Replace("jhb.", string.Empty).Replace("fl360.", string.Empty).Replace("api.", string.Empty)!;
        public TemplateProxyRoutesJS() { }
        public TemplateProxyRoutesJS(string projectName, string author, string dateCreated, string directory, ProjectScaffoldingElement[] proxyRoutes) :this() {
            ProjectName = projectName;
            Author = author;
            DateCreated = dateCreated;
            Directory = directory;
            ProxyRoutes = proxyRoutes;
        }
        public void Generate() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine("| Description   : Proxy routes module");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("/**");
            sb.AppendLine(" * Creates an instance of the proxy routing module");
            sb.AppendLine(" * @returns ProxyRouteModule");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = [");
            foreach(var proxyRoute in ProxyRoutes!)
            {
                var apiName = proxyRoute?.Name?.ToLower()?.Replace("jhb.", string.Empty)?.Replace("fl360.", string.Empty)?.Replace("api.", string.Empty);
                sb.AppendLine("  {");
                sb.AppendLine($"    url: `/{ApiGatewayName}/{apiName}`,");
                sb.AppendLine("    auth: true,");
                //sb.AppendLine("    creditCheck: true,");
                sb.AppendLine("    proxy: {");
                sb.AppendLine($"      target: \"http://localhost:{proxyRoute?.ApiPort}/api/zmb\",");
                sb.AppendLine("      changeOrigin: true,");
                sb.AppendLine("      preserveBody: true,");
                sb.AppendLine("      timeout: 120000,");
                sb.AppendLine("      pathRewrite: {");
                sb.AppendLine($"        [`^/{ApiGatewayName}`]: ``,");
                sb.AppendLine("      },");
                sb.AppendLine("    },");
                sb.AppendLine("  },");
            }
            sb.AppendLine("];");
            new DirectoryIO(Directory).Recreate(true);
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
