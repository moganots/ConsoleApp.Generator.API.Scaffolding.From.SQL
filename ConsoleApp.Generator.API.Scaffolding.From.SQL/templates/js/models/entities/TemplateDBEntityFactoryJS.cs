using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.models.entities
{
    public class TemplateDBEntityFactoryJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? ModelsEntitiesDirectory { get; set; }
        public string[]? EntityNames { get; set; }
        public bool? CanUseMongoDb { get; set; }
        public bool? CanUseMsSqlDb { get; set; }
        public bool? CanUsePostgreSqlDb { get; set; }
        public string FilePath => Path.Combine(ModelsEntitiesDirectory!, $"model.entity.factory.js");
        public TemplateDBEntityFactoryJS() { }
        public TemplateDBEntityFactoryJS(string author, string dateCreated, string modelsEntitiesDirectory, string[] entityNames, bool? canUseMongoDb = false, bool? canUseMsSqlDb = false, bool? canUsePostgreSqlDb = true) : this()
        {
            Author = author;
            DateCreated = dateCreated;
            ModelsEntitiesDirectory = modelsEntitiesDirectory;
            EntityNames = entityNames;
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
            sb.AppendLine($"| Description   : Model entity factory module");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine(" * Creates an instance of the model entity factory module");
            sb.AppendLine(" * @param {*} config an instance of the configuration module");
            sb.AppendLine(" * @param {*} logger an instance of the logging module");
            sb.AppendLine(" * @param {*} entityName the name of the model or entity to be retrieved");
            sb.AppendLine(" * @returns ModelEntityFactory");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = (config, logger, entityName) => {");
            sb.AppendLine("  const { notSet } = require(`./../../utils/general/object.util`)();");
            sb.AppendLine("  const { isEmpty } = require(`./../../utils/general/string.util`)();");
            sb.AppendLine("  if (notSet(config)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ModelEntityFactory - [config] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(logger)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ModelEntityFactory - [logger] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  if (isEmpty(entityName)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine("      `ModelEntityFactory - [entityName] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  const getModelEntity =  () => {");
            sb.AppendLine("    switch (config?.app?.database?.DB_CONTEXT) {");
            if ((bool)CanUseMongoDb!)
            {
                sb.AppendLine("      case `MONGODB`:");
                sb.AppendLine("        return getMongoDBModelEntity();");
            }
            if ((bool)CanUseMsSqlDb!)
            {
                sb.AppendLine("      case `MSSQL`:");
                sb.AppendLine("        return getMsSqlModelEntity();");
            }
            if ((bool)CanUsePostgreSqlDb!)
            {
                sb.AppendLine("      case `POSTGRES`:");
                sb.AppendLine("        return getPostgresModelEntity();");
            }
            sb.AppendLine("      default:");
            sb.AppendLine("        return { };");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            if ((bool)CanUseMongoDb!)
            {
                sb.AppendLine("  const getMongoDBModelEntity =  () => {");
                sb.AppendLine("    switch(entityName){");
                if (EntityNames?.Length != 0)
                {
                    EntityNames!.ToList().ForEach(en =>
                    {
                        sb.AppendLine($"        case `{en.GetEntityFileName()}`: return require(`./mongodb/{en.GetEntityFileName()}`);");
                    });
                }
                sb.AppendLine("        default: return { };");
                sb.AppendLine("    }");
                sb.AppendLine("  };");
            }
            if ((bool)CanUseMsSqlDb!)
            {
                sb.AppendLine("  const getMsSqlModelEntity =  () => {");
                sb.AppendLine("    switch(entityName){");
                if (EntityNames?.Length != 0)
                {
                    EntityNames!.ToList().ForEach(en =>
                    {
                        sb.AppendLine($"        case `{en.GetEntityFileName()}`: return require(`./mssql/{en.GetEntityFileName()}`);");
                    });
                }
                sb.AppendLine("        default: return { };");
                sb.AppendLine("    }");
                sb.AppendLine("  };");
            }
            if ((bool)CanUsePostgreSqlDb!)
            {
                sb.AppendLine("  const getPostgresModelEntity =  () => {");
                sb.AppendLine("    switch(entityName){");
                if (EntityNames?.Length != 0)
                {
                    EntityNames!.ToList().ForEach(en =>
                    {
                        sb.AppendLine($"        case `{en.GetEntityFileName()}`: return require(`./postgres/{en.GetEntityFileName()}`);");
                    });
                }
                sb.AppendLine("        default: return { };");
                sb.AppendLine("    }");
                sb.AppendLine("  };");
            }
            sb.AppendLine("  return {");
            sb.AppendLine("    getModelEntity,");
            sb.AppendLine("  };");
            sb.AppendLine("};");
            new DirectoryIO(ModelsEntitiesDirectory).Create();
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
