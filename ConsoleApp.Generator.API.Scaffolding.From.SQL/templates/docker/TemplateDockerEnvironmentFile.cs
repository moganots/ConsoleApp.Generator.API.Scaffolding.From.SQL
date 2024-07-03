using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.classes;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.docker
{
    public class TemplateDockerEnvironmentFile
    {
        public string? ProjectName { get; set; }
        public string? ApiPort { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsGateway { get; set; }
        public string? TargetDirectory { get; set; }
        public string?[] TargetEnvironments { get; set; }
        public TemplateDockerEnvironmentFile()
        {
            TargetEnvironments = new string[]{ "local", "dev", "stg", "sit", "uat", "prod", "dr" };
        }
        public TemplateDockerEnvironmentFile(string projectName, string apiPort, bool isActive, bool isGateway, string targetDirectory, string?[] targetEnvironments)
        {
            ProjectName = projectName;
            ApiPort = apiPort;
            IsActive = isActive;
            IsGateway = isGateway;
            TargetDirectory = targetDirectory;
            TargetEnvironments = targetEnvironments ?? new string[] { "local", "dev", "stg", "sit", "uat", "prod", "dr" };
        }
        string GetTemplateDockerFile()
        {
            string projectRootDirectoryName = ProjectName?.Replace("jhb.", string.Empty).Replace("fl360.", string.Empty).Replace("api.", string.Empty).Replace("app.", string.Empty).Replace("portal.", string.Empty).Replace(".", "_")!;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FROM node:16.14.2-alpine");
            sb.AppendLine("");
            sb.AppendLine("# Create app directory");
            sb.AppendLine($"WORKDIR /usr/src/app/{projectRootDirectoryName}");
            sb.AppendLine("");
            sb.AppendLine("# Install app dependencies");
            sb.AppendLine("# A wildcard is used to ensure both package.json AND package-lock.json are copied");
            sb.AppendLine("# where available (npm@5+)");
            sb.AppendLine("ADD package*.json .");
            sb.AppendLine("");
            sb.AppendLine("# Install project NPM depedencies (run with configuration registry set to http://registry.npmjs.org/");
            sb.AppendLine("# RUN npm config set registry http://registry.npmjs.org/ && npm run clean:install");
            sb.AppendLine("# Install project NPM depedencies (run with configuration registry set to strict-ssl false");
            sb.AppendLine("RUN npm config set strict-ssl false && npm run clean:install");
            sb.AppendLine("");
            sb.AppendLine("# Install app (API) file(s)");
            sb.AppendLine("ADD src ./src");
            sb.AppendLine("");
            sb.AppendLine("# Add the environment configuration file");
            sb.AppendLine("ADD src/config/environment/.env.@Environment .env");
            sb.AppendLine("");
            sb.AppendLine("# Remove any residual source code file(s) and directory(ies), that might have been missed by .dockerignore");
            sb.AppendLine("RUN rm -rf docker");
            sb.AppendLine("RUN rm -rf src/config/environment");
            sb.AppendLine("RUN rm -rf logs");
            sb.AppendLine("RUN rm -rf git");
            sb.AppendLine("RUN rm -rf .git");
            sb.AppendLine("RUN rm -rf .gitignore");
            sb.AppendLine("RUN rm -rf tsconf*.json");
            sb.AppendLine("");
            sb.AppendLine("#EXPOSE PORT");
            sb.AppendLine("EXPOSE @ApiPort");
            sb.AppendLine("");
            sb.AppendLine("CMD [\"npm\", \"run\", \"start:@Environment\"]");
            return sb.ToString();
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileLocal(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "local").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "local")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "local", "Dockerfile.local")).Replace(content);
            return this;
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileDev(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "dev").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "dev")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "dev", "Dockerfile.dev")).Replace(content);
            return this;
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileStaging(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "stg").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "stg")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "stg", "Dockerfile.stg")).Replace(content);
            return this;
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileUat(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "uat").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "uat")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "uat", "Dockerfile.uat")).Replace(content);
            return this;
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileIntegration(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "sit").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "sit")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "sit", "Dockerfile.sit")).Replace(content);
            return this;
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileProduction(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "prod").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "prod")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "prod", "Dockerfile.prod")).Replace(content);
            return this;
        }
        public TemplateDockerEnvironmentFile GenerateDockerFileDisasterRecovery(string? apiPort = null)
        {
            string content = GetTemplateDockerFile().Replace("@Environment", "dr").Replace("@ApiPort", apiPort ?? ApiPort);
            new DirectoryIO(Path.Combine(TargetDirectory!, "dr")).Recreate(true);
            new FileIO(Path.Combine(TargetDirectory!, "dr", "Dockerfile.dr")).Replace(content);
            return this;
        }
        public void GenerateDockerComposeYamlFile(IProxyProject?[] proxyProjects)
        {
            foreach (var targetEnvironment in TargetEnvironments)
            {
                var targetDirectory = Path.Combine(TargetDirectory!, targetEnvironment!);
                new DirectoryIO(targetDirectory).Create();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("version: '3.9'");
                sb.AppendLine("services:");
                sb.AppendLine("");
                sb.AppendLine("  webserver:");
                sb.AppendLine("    image: nginx:1.22.1");
                sb.AppendLine("    container_name: nginx");
                sb.AppendLine("    restart: always");
                sb.AppendLine("    environment:");
                sb.AppendLine("      TZ: UTC");
                sb.AppendLine("    ports:");
                sb.AppendLine("      - \"443:443\"");
                sb.AppendLine("      - \"3000:3000\"");
                sb.AppendLine("    volumes:");
                sb.AppendLine("      - /home/ec2-user/nginx/cert:/etc/nginx/cert");
                sb.AppendLine("      - /home/ec2-user/nginx/conf.d:/etc/nginx/conf.d");
                sb.AppendLine("      - /home/ec2-user/nginx/logs:/etc/nginx/logs");
                sb.AppendLine("      - /home/ec2-user/nginx/logs:/var/log/nginx");
                sb.AppendLine("      - /home/ec2-user/nginx/html:/usr/share/nginx/html");
                sb.AppendLine("    healthcheck:");
                sb.AppendLine("      test: [\"CMD\", \"service\", \"nginx\", \"status\"]");
                sb.AppendLine("      interval: 5s");
                sb.AppendLine("      timeout: 5s");
                sb.AppendLine("      retries: 100");
                sb.AppendLine("      start_period: 60s");
                sb.AppendLine("    networks:");
                sb.AppendLine("      - app-network");
                if (IsGateway!.Value && proxyProjects?.Length != 0)
                {
                    AppendProjectToDockerComposeFile(targetEnvironment!, sb, ProjectName!, IsActive!.Value, ApiPort!);
                    AppendProjectToDockerComposeFile(targetEnvironment!, sb, ProjectName!.IfThen("jhb.fl360.api.gateway.mobile", "jhb.fl360.api.gateway.web")?.ToString()!, IsActive!.Value, ApiPort!.IfThen(35360, 35361)?.ToString()!);
                    var projects = from proxyProject in proxyProjects
                                    orderby proxyProject.ApiPort
                                    select proxyProject;
                    foreach (var project in projects)
                    {
                        AppendProjectToDockerComposeFile(targetEnvironment!, sb, project.Name, project.IsActive, project.ApiPort);
                    }
                }
                else
                {
                    AppendProjectToDockerComposeFile(targetEnvironment!, sb, ProjectName!, IsActive!.Value, ApiPort!);
                }
                sb.AppendLine("");
                sb.AppendLine("networks:");
                sb.AppendLine("  app-network:");
                sb.AppendLine("    driver: bridge");
                GenerateDockerIgnoreFile(targetDirectory);
                new FileIO(Path.Combine(targetDirectory, $"docker-compose.yml")).Replace(sb.ToString());
            }
        }

        private void AppendProjectToDockerComposeFile(string targetEnvironment, StringBuilder sb, string projectName, bool isActive, string apiPort)
        {
            if(projectName.NotEmpty() && apiPort.NotEmpty())
            {
                string projectDockerImageContainerName = projectName?.Replace("jhb.", string.Empty).Replace("fl360.", string.Empty).Replace(".", "_")!;
                string projectDockerImageName = projectName?.Replace("jhb.", string.Empty).Replace("fl360.", "fl360/").Replace(".", "_")!;
                string projectRootDirectoryName = projectName?.Replace("jhb.", string.Empty).Replace("fl360.", string.Empty).Replace("api.", string.Empty).Replace("app.", string.Empty).Replace("portal.", string.Empty).Replace(".", "_")!;
                sb.AppendLine("");
                sb.AppendLine($"{isActive.ifNotActive("#")}  {projectDockerImageContainerName}:");
                sb.AppendLine($"{isActive.ifNotActive("#")}    image: {projectDockerImageName}.{targetEnvironment}:1.0.0");
                sb.AppendLine($"{isActive.ifNotActive("#")}    container_name: {projectDockerImageContainerName}");
                sb.AppendLine($"{isActive.ifNotActive("#")}    restart: always");
                sb.AppendLine($"{isActive.ifNotActive("#")}    ports:");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - { apiPort }:{ apiPort }");
                sb.AppendLine($"{isActive.ifNotActive("#")}    volumes:");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /custom/mount/logs/zmb:/usr/src/app/logs");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /home/ec2-user/apps/api/zmb/{projectRootDirectoryName}/logs:/usr/src/app/{projectRootDirectoryName}/logs");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /home/ec2-user/apps/api/zmb/{projectRootDirectoryName}/public:/usr/src/app/{projectRootDirectoryName}/public");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /home/ec2-user/apps/api/zmb/{projectRootDirectoryName}/public/logs:/usr/src/app/{projectRootDirectoryName}/public/logs");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /home/ec2-user/apps/api/zmb/{projectRootDirectoryName}/public/user_data:/usr/src/app/{projectRootDirectoryName}/public/user_data");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /home/ec2-user/apps/api/zmb/{projectRootDirectoryName}/public/photos/farmer:/usr/src/app/{projectRootDirectoryName}/public/photos/farmer");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - /home/ec2-user/apps/api/zmb/{projectRootDirectoryName}/public/photos/user:/usr/src/app/{projectRootDirectoryName}/public/photos/user");
                sb.AppendLine($"{isActive.ifNotActive("#")}    depends_on:");
                sb.AppendLine($"{isActive.ifNotActive("#")}      webserver:");
                sb.AppendLine($"{isActive.ifNotActive("#")}        condition: service_healthy");
                sb.AppendLine($"{isActive.ifNotActive("#")}    networks:");
                sb.AppendLine($"{isActive.ifNotActive("#")}      - app-network");
            }
        }
        private void GenerateDockerIgnoreFile(string targetDirectory)
        {
            var sb = new StringBuilder();
            sb.AppendLine(".aws");
            sb.AppendLine(".coverage*");
            sb.AppendLine(".dockerignore");
            sb.AppendLine(".env*");
            sb.AppendLine(".eslintrc.js");
            sb.AppendLine(".git");
            sb.AppendLine(".gitignore");
            sb.AppendLine(".history");
            sb.AppendLine(".log");
            sb.AppendLine(".prettierrc");
            sb.AppendLine(".whitesource");
            sb.AppendLine("dist");
            sb.AppendLine("docker");
            sb.AppendLine("docker-compose.yml");
            sb.AppendLine("docker_files");
            sb.AppendLine("env_files");
            sb.AppendLine("environment");
            sb.AppendLine("logs");
            sb.AppendLine("node_modules");
            sb.AppendLine("npm-debug.log");
            sb.AppendLine("README.md");
            sb.AppendLine("shells");
            sb.AppendLine("test");
            sb.AppendLine("tests");
            sb.AppendLine("tsconf*.json");
            new FileIO(Path.Combine(targetDirectory!, ".dockerignore")).Replace(sb.ToString());
        }
    }
}
