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
    public class TemplateEntityRouteJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? Directory { get; set; }
        public string? SchemaName { get; set; }
        public string? EntityName { get; set; }
        public string FileDirectory => Path.Combine(Directory!, $"{EntityName!.FormatRouteEntityDirectoryName()}");
        public string FilePath => Path.Combine(FileDirectory, "api.js");
        public TemplateEntityRouteJS() { }
        public TemplateEntityRouteJS(string author, string dateCreated, string directory, string schemaName, string entityName) :this() {
            Author = author;
            DateCreated = dateCreated;
            Directory = directory;
            SchemaName = schemaName;
            EntityName = entityName;
        }
        public void Generate() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author		: {Author}");
            sb.AppendLine($"| Date Created	: {DateCreated}");
            sb.AppendLine($"| Description   : Route module for the [{SchemaName}].[{EntityName}] table");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine($" * Creates an instance of the {EntityName!.ProperCase()}RouteModule");
            sb.AppendLine(" * @param {*} config an instance of the configuration module");
            sb.AppendLine(" * @param {*} logger an instance of the logging module");
            sb.AppendLine(" * @param {*} app an instance of the Express app");
            sb.AppendLine(" * @param {*} dbContext an instance of the DBContext");
            sb.AppendLine($" * @returns {EntityName!.ProperCase()}RouteModule");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = function(config, logger, app, dbContext, router) {");
            sb.AppendLine($"\tconst apiAnchorName = `{EntityName!.FormatEntityRouteName()}`;");
            sb.AppendLine("\tconst { setRoute } = require(`./../../utils/general/http.util`)();");
            sb.AppendLine("\tconst { notSet } = require(`./../../utils/general/object.util`)();");
            sb.AppendLine("");
            sb.AppendLine("  if (notSet(config)) {");
            sb.AppendLine($"    throw new Error(`{EntityName!.ProperCase()}RouteModule - [config] instance has not been provided or set`); ");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(logger)) {");
            sb.AppendLine($"    throw new Error(`{EntityName!.ProperCase()}RouteModule - [logger] instance has not been provided or set`);");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(app)) {");
            sb.AppendLine($"    throw new Error(`{EntityName!.ProperCase()}RouteModule - [logger] instance has not been provided or set`);");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(dbContext)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine($"      `{EntityName!.ProperCase()}RouteModule - [dbContext] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("");
            sb.AppendLine("\tconst heartbeat = require(`./../../middleware/routing/heartbeat`)(config, logger);");
            sb.AppendLine($"\tconst controller = require(`./../../controllers/controller.factory`)(config, logger, dbContext, `{EntityName!.GetEntityControllerFileName()}`).getController();");
            sb.AppendLine("");
            sb.AppendLine("\t// ping");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `ping`)).get(heartbeat.ping);");
            sb.AppendLine("");
            sb.AppendLine("\t// create");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `create`)).post(controller.create);");
            sb.AppendLine("");
            sb.AppendLine("\t// getAll");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `getAll`)).get(controller.getAll);");
            sb.AppendLine("");
            sb.AppendLine("\t// getById");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `getById`, `:id`)).get(controller.getById);");
            sb.AppendLine("");
            sb.AppendLine("\t// getBy");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `getBy?`)).get(controller.getBy);");
            sb.AppendLine("");
            sb.AppendLine("\t// update");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `update`)).post(controller.update);");
            sb.AppendLine("");
            sb.AppendLine("\t// delete");
            sb.AppendLine("\tapp.route(setRoute(apiAnchorName, `delete`, `:id`)).post(controller.delete);");
            sb.AppendLine("}");
            new DirectoryIO(FileDirectory).Recreate(true);
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
