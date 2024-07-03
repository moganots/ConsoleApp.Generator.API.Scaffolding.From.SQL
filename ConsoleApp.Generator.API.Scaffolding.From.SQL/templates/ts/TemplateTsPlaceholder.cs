using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.ts
{
    public class TemplateTsPlaceholder
    {
        public string? RootDirectory { get; set; }
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string FilePath => Path.Combine(RootDirectory!, "placeholder.ts");
        public TemplateTsPlaceholder() { }
        public TemplateTsPlaceholder(string rootDirectory, string author, string dateCreated) : this()
        {
            RootDirectory = rootDirectory;
            Author = author;
            DateCreated = dateCreated;
        }
        public void Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine("| Description   : Placeholder ts file, to correct tsconfig input errors");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("");
            sb.AppendLine("export {};");
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
