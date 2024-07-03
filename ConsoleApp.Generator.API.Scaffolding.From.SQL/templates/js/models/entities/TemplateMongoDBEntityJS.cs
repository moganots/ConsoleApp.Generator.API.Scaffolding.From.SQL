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
    public class TemplateMongoDBEntityJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? EntityName { get; set; }
        public string? SchemaName { get; set; }
        public string? EntityDirectory { get; set; }
        public string? ColumnDefinition { get; set; }
        public string FilePath => Path.Combine(EntityDirectory!, $"{EntityName!.GetEntityFileName()}.schema");
        public TemplateMongoDBEntityJS() { }
        public TemplateMongoDBEntityJS(string author, string dateCreated, string schemaName, string entityName, string entityDirectory, string columnDefinition)
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
            //sb.AppendLine($"| Description   : MongoDB Entity (Model) utility module for the [" + SchemaName + "].[" + EntityName + "] Table");
            sb.AppendLine($"| Description   : MongoDB Entity (Model) utility module for the [" + EntityName + "] Collection");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("");
            sb.AppendLine("const mongoose = require(`mongoose`);");
            sb.AppendLine("const types = mongoose.SchemaTypes;");
            sb.AppendLine($"module.exports = mongoose.model(`{EntityName}`, new mongoose.Schema({{");
            sb.AppendLine(ColumnDefinition);
            sb.AppendLine("}))");
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
