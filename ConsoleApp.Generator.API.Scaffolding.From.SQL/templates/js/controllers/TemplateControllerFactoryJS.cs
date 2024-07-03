using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.controllers
{
    public class TemplateControllerFactoryJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? ControllerDirectory { get; set; }
        public string[]? EntityNames { get; set; }
        public bool? CanUseMongoDb { get; set; }
        public bool? CanUseMsSqlDb { get; set; }
        public bool? CanUsePostgreSqlDb { get; set; }
        public string FilePath => Path.Combine(ControllerDirectory!, $"controller.factory.js");
        public TemplateControllerFactoryJS() { }
        public TemplateControllerFactoryJS(string author, string dateCreated, string controllerDirectory, string[] entityNames, bool? canUseMongoDb = false, bool? canUseMsSqlDb = false, bool? canUsePostgreSqlDb = true):this()
        {
            Author = author;
            DateCreated = dateCreated;
            ControllerDirectory = controllerDirectory;
            EntityNames = entityNames;
            CanUseMongoDb = canUseMongoDb;
            CanUseMsSqlDb = canUseMsSqlDb;
            CanUsePostgreSqlDb = canUsePostgreSqlDb;
        }
        public void Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine($"| Description   : Controller factory module");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine(" * Creates an instance of the controller factory module");
            sb.AppendLine(" * @param {*} config an instance of the configuration module");
            sb.AppendLine(" * @param {*} logger an instance of the logging module");
            sb.AppendLine(" * @param {*} dbContext an instance of the DB context");
            sb.AppendLine(" * @param {*} controllerName the name of the controller to be retrieved");
            sb.AppendLine(" * @returns ControllerFactory");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = (config, logger, dbContext, controllerName) => {");
            sb.AppendLine("  const { notSet } = require(`./../utils/general/object.util`)();");
            sb.AppendLine("  const { isEmpty } = require(`./../utils/general/string.util`)();");
            sb.AppendLine("  if (notSet(config)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ControllerFactory - [config] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(logger)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ControllerFactory - [logger] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(dbContext)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ControllerFactory - [dbContext] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  if (isEmpty(controllerName)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ControllerFactory - [controllerName] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  const getController =  () => {");
            sb.AppendLine("    switch (config?.app?.database?.DB_CONTEXT) {");
            if ((bool)CanUseMongoDb!)
            {
                sb.AppendLine("      case `MONGODB`:");
                sb.AppendLine("        return getMongoDBController();");
            }
            if ((bool)CanUseMsSqlDb!)
            {
                sb.AppendLine("      case `MSSQL`:");
                sb.AppendLine("        return getMsSqlController();");
            }
            if ((bool)CanUsePostgreSqlDb!)
            {
                sb.AppendLine("      case `POSTGRES`:");
                sb.AppendLine("        return getPostgresController();");
            }
            sb.AppendLine("      default:");
            sb.AppendLine("        return require(`./no.such.controller`)(logger, controllerName);");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            if ((bool)CanUseMongoDb!)
            {
                sb.AppendLine("  const getMongoDBController =  () => {");
                sb.AppendLine("    switch(controllerName){");
                if (EntityNames?.Length != 0)
                {
                    EntityNames!.ToList().ForEach(en =>
                    {
                        sb.AppendLine($"        case `{en.GetEntityControllerFileName()}`: return require(`./mongodb/{en.GetEntityControllerFileName()}`)(logger, dbContext);");
                    });
                }
                // sb.AppendLine("        default: return require(`./no.such.controller`)(logger, controllerName);");
                sb.AppendLine("    }");
                sb.AppendLine("  };");
            }
            if ((bool)CanUseMsSqlDb!)
            {
                sb.AppendLine("  const getMsSqlController =  () => {");
                sb.AppendLine("    switch(controllerName){");
                if (EntityNames?.Length != 0)
                {
                    EntityNames!.ToList().ForEach(en =>
                    {
                        sb.AppendLine($"        case `{en.GetEntityControllerFileName()}`: return require(`./mssql/{en.GetEntityControllerFileName()}`)(logger, dbContext);");
                    });
                }
                //sb.AppendLine("        default: return require(`./no.such.controller`)(logger, controllerName);");
                sb.AppendLine("    }");
                sb.AppendLine("  };");
            }
            if ((bool)CanUsePostgreSqlDb!)
            {
                sb.AppendLine("  const getPostgresController =  () => {");
                sb.AppendLine("    switch(controllerName){");
                if (EntityNames?.Length != 0)
                {
                    EntityNames!.ToList().ForEach(en =>
                    {
                        sb.AppendLine($"        case `{en.GetEntityControllerFileName()}`: return require(`./postgres/{en.GetEntityControllerFileName()}`)(logger, dbContext);");
                    });
                }
                //sb.AppendLine("        default: return require(`./no.such.controller`)(logger, controllerName);");
                sb.AppendLine("    }");
                sb.AppendLine("  };");
            }
            sb.AppendLine("  return {");
            sb.AppendLine("    getController,");
            sb.AppendLine("  };");
            sb.AppendLine("};");
            new DirectoryIO(ControllerDirectory).Create();
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
