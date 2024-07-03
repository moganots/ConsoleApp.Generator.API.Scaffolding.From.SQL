using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates
{
    public class TemplateGitIgnore
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectVersion { get; set; }
        public string? ProjectAuthor { get; set; }
        public bool? IsGateway { get; set; }
        public string FileName => ".gitignore";
        public TemplateGitIgnore() {}
        public TemplateGitIgnore(
            string projectName,
            string projectDescription,
            string projectVersion,
            string projectAuthor,
            bool? isGateway = false) : this() {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            ProjectVersion = projectVersion;
            ProjectAuthor = projectAuthor;
            IsGateway = isGateway;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("# Quasar core related directories");
            sb.AppendLine(".quasar");
            sb.AppendLine("/dist");
            sb.AppendLine("/quasar.config.*.temporary.compiled*");
            sb.AppendLine("");
            sb.AppendLine("# compiled output");
            sb.AppendLine("/dist");
            sb.AppendLine("dist");
            sb.AppendLine("/tmp");
            sb.AppendLine("tmp");
            sb.AppendLine("/out-tsc");
            sb.AppendLine("out-tsc");
            sb.AppendLine("/output");
            sb.AppendLine("output");
            sb.AppendLine("/.angular");
            sb.AppendLine("");
            sb.AppendLine("# dependencies");
            sb.AppendLine("/node_modules");
            sb.AppendLine("node_modules");
            sb.AppendLine("");
            sb.AppendLine("# IDEs and editors");
            sb.AppendLine("/.idea");
            sb.AppendLine(".idea");
            sb.AppendLine(".project");
            sb.AppendLine(".classpath");
            sb.AppendLine(".c9/");
            sb.AppendLine("*.launch");
            sb.AppendLine(".settings/");
            sb.AppendLine("*.sublime-workspace");
            sb.AppendLine("");
            sb.AppendLine("# IDE - VSCode");
            sb.AppendLine(".vscode/*");
            sb.AppendLine("!.vscode/settings.json");
            sb.AppendLine("!.vscode/tasks.json");
            sb.AppendLine("!.vscode/launch.json");
            sb.AppendLine("!.vscode/extensions.json");
            sb.AppendLine("*.code-workspace");
            sb.AppendLine("# Local History for Visual Studio Code");
            sb.AppendLine(".history/");
            sb.AppendLine("");
            sb.AppendLine("# Log files");
            sb.AppendLine("*npm-debug*.log*");
            sb.AppendLine("*yarn-debug*.log*");
            sb.AppendLine("*yarn-error*.log*");
            sb.AppendLine("*testem.log*");
            sb.AppendLine("*debug*.log*");
            sb.AppendLine("*error*.log*");
            sb.AppendLine("");
            sb.AppendLine("# Editor directories and files");
            sb.AppendLine(".idea");
            sb.AppendLine("*.suo");
            sb.AppendLine("*.ntvs*");
            sb.AppendLine("*.njsproj");
            sb.AppendLine("*.sln");
            sb.AppendLine("");
            sb.AppendLine("# local .env files");
            sb.AppendLine(".env.local*");
            sb.AppendLine("");
            sb.AppendLine("# Private Files");
            sb.AppendLine("*.json");
            sb.AppendLine("*.csv");
            sb.AppendLine("*.csv.gz");
            sb.AppendLine("*.tsv");
            sb.AppendLine("*.tsv.gz");
            sb.AppendLine("*.xlsx");
            sb.AppendLine("*.zip");
            sb.AppendLine("*.war");
            sb.AppendLine("*.gzip");
            sb.AppendLine("*.gz");
            sb.AppendLine("*.tar.*");
            sb.AppendLine("*.tar.gz");
            sb.AppendLine("");
            sb.AppendLine("# misc");
            sb.AppendLine("/.sass-cache");
            sb.AppendLine(".sass-cache");
            sb.AppendLine("/connect.lock");
            sb.AppendLine("connect.lock");
            sb.AppendLine("/coverage");
            sb.AppendLine("coverage");
            sb.AppendLine("/libpeerconnection.log");
            sb.AppendLine("libpeerconnection.log");
            sb.AppendLine("/typings");
            sb.AppendLine("typings");
            sb.AppendLine("");
            sb.AppendLine("# System Files");
            sb.AppendLine("DS_Store");
            sb.AppendLine(".DS_Store");
            sb.AppendLine("thumbs.db");
            sb.AppendLine("Thumbs.db");
            sb.AppendLine(".thumbs.db");
            sb.AppendLine(".Thumbs.db");
            return sb.ToString();
        }
    }
}
