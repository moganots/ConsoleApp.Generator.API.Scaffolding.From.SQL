using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.models.entities
{
    public class TemplateMsSqlDBEntityJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? EntityName { get; set; }
        public string? SchemaName { get; set; }
        public string? EntityDirectory { get; set; }
        public string? EntityParameters { get; set; }
        public string? ComponentParameters { get; set; }
        public string? ComponentReturnParameters { get; set; }
        public string FilePath => Path.Combine(EntityDirectory!, $"{EntityName!.GetEntityFileName()}.js");
        public TemplateMsSqlDBEntityJS() { }
        public TemplateMsSqlDBEntityJS(string author, string dateCreated, string schemaName, string entityName, string entityDirectory, string entityParameters, string componentParameters, string componentReturnParameters)
        {
            Author = author;
            DateCreated = dateCreated;
            SchemaName = schemaName;
            EntityName = entityName;
            EntityDirectory = entityDirectory;
            EntityParameters = entityParameters;
            ComponentParameters = componentParameters;
            ComponentReturnParameters = componentReturnParameters;
        }
        public void GenerateModel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine($"| Description   : MS SQL Entity (Model) utility module for the [" + SchemaName + "].[" + EntityName + "] Table");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| Function(s)");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("const " + EntityName!.FormatMsSqlEntityName() + " = () => {");
            sb.AppendLine("	const schemaName = () => {");
            sb.AppendLine($"		return `{SchemaName}`;");
            sb.AppendLine("	}");
            sb.AppendLine("	const getName = () => {");
            sb.AppendLine($"		return `{EntityName}`;");
            sb.AppendLine("	}");
            sb.AppendLine("	const getColumnNames = () => {");
            sb.AppendLine($"		return [{String.Join(", ", ComponentParameters!.Split(',').Select(columnName => $"`{columnName.Trim()}`").ToArray()).Trim()}];");
            sb.AppendLine("	}");
            sb.AppendLine("	const fromEntity = (entity = {}) => {");
            sb.AppendLine("		return fromComponents(" + EntityParameters + ");");
            sb.AppendLine("	}");
            sb.AppendLine("	const fromComponents = (" + ComponentParameters + ") => {");
            sb.AppendLine("		return {");
            sb.AppendLine($"\t\t\t{ComponentReturnParameters}");
            sb.AppendLine("		}");
            sb.AppendLine("	}");
            sb.AppendLine("	return {");
            sb.AppendLine("		schemaName,");
            sb.AppendLine("		getName,");
            sb.AppendLine("		getColumnNames,");
            sb.AppendLine("		fromEntity,");
            sb.AppendLine("		fromComponents");
            sb.AppendLine("	}");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| module.exports");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = " + EntityName!.FormatMsSqlEntityName() + ";");
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
