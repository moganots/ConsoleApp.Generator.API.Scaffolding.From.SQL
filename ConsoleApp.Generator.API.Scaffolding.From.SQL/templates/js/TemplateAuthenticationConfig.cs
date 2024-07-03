using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js
{
    public class TemplateAuthenticationConfig
    {
        public string? ProjectSrcDirectory { get; set; }
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public bool? IsAuthenticationProjectType { get; set; }
        public TemplateAuthenticationConfig() { }
        public TemplateAuthenticationConfig(string projectSrcDirectory, string author, string dateCreated, bool isAuthenticationProjectType = false) : this() {
            ProjectSrcDirectory = projectSrcDirectory;
            Author = author;
            DateCreated = dateCreated;
            IsAuthenticationProjectType = isAuthenticationProjectType;
        }
        public TemplateAuthenticationConfig GenerateConstants(string sourceDirectory)
        {
            new DirectoryIO(sourceDirectory).CopyRecursively(Path.Combine(ProjectSrcDirectory!, "constants", "authentication"), SearchOption.AllDirectories, Author, DateCreated, true, true, true);
            return this;
        }
        public TemplateAuthenticationConfig GenerateMiddleware(string sourceDirectory)
        {
            new DirectoryIO(sourceDirectory).CopyRecursively(Path.Combine(ProjectSrcDirectory!, "middleware", "authentication"), SearchOption.AllDirectories, Author, DateCreated, true, true, true);
            return this;
        }
        public TemplateAuthenticationConfig GenerateRoutes(string sourceDirectory)
        {
            if ((bool)IsAuthenticationProjectType!)
            {
                new DirectoryIO(sourceDirectory).CopyRecursively(Path.Combine(ProjectSrcDirectory!, "routes", "route.authentication"), SearchOption.AllDirectories, Author, DateCreated, true, true, true);
            }
            return this;
        }
    }
}
