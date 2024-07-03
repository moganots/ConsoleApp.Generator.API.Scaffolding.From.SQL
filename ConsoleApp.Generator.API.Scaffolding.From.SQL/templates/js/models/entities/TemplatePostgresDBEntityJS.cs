using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.models.entities
{
    public class TemplatePostgresDBEntityJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? EntityName { get; set; }
        public string? SchemaName { get; set; }
        public string? EntityDirectory { get; set; }
        public string? ColumnDefinition { get; set; }
        public string FilePath => Path.Combine(EntityDirectory!, $"{EntityName!.GetEntityFileName()}.js");
        public TemplatePostgresDBEntityJS() { }
        public TemplatePostgresDBEntityJS(string author, string dateCreated, string schemaName, string entityName, string entityDirectory, string columnDefinition)
        {
            Author = author;
            DateCreated = dateCreated;
            SchemaName = schemaName;
            EntityName = entityName;
            EntityDirectory = entityDirectory;
            ColumnDefinition = columnDefinition;
        }
        public void GenerateModel()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine($"| Description   : PostgreSQL Entity (Model) utility module for the [" + SchemaName + "].[" + EntityName + "] Table");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("");
            sb.AppendLine("const EntitySchema = require(`typeorm`).EntitySchema;");
            sb.AppendLine("module.exports = new EntitySchema({");
            sb.AppendLine($"\tschema: `{SchemaName}`,");
            sb.AppendLine($"\tname: `{EntityName}`,");
            sb.AppendLine("\tcolumns: {");
            sb.AppendLine(ColumnDefinition);
            sb.AppendLine("\t}");
            sb.AppendLine("});");
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
