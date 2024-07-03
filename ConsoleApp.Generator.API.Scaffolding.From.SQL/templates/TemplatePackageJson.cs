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
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates
{
    public class TemplatePackageJson
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectVersion { get; set; }
        public string? ProjectAuthor { get; set; }
        public bool? IsGateway { get; set; }
        public bool? EnableSwagger { get; set; }
        public string[]? ProjectKeywords { get; set; }
        public string? ProjectEntryClass { get; set; }
        public bool? IsSetProjectEntryClass => ProjectEntryClass!.NotEmpty();
        public string? ProjectGithubAccount { get; set; }
        public EnumPackageJsonLicense ProjectLicense { get; set; }
        public string FileName => "package.json";
        public TemplatePackageJson() {
            ProjectLicense = EnumPackageJsonLicense.ISC;
        }
        public TemplatePackageJson(
            string projectName,
            string projectDescription,
            string projectVersion,
            string projectAuthor,
            bool? isGateway = false,
            bool? enableSwagger = false) : this() {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            ProjectVersion = projectVersion;
            ProjectAuthor = projectAuthor;
            IsGateway = isGateway;
            EnableSwagger = enableSwagger;
        }
        public TemplatePackageJson(
            string projectName,
            string projectDescription,
            string projectVersion,
            string projectAuthor,
            string[]? projectKeywords,
            string? projectEntryClass,
            string? projectGithubAccount,
            bool? isGateway = false,
            bool? enableSwagger = false,
            EnumPackageJsonLicense projectLicense = EnumPackageJsonLicense.ISC) : this(
                projectName,
                projectDescription,
                projectVersion,
                projectAuthor, isGateway, enableSwagger)
        {
            ProjectKeywords = projectKeywords;
            ProjectEntryClass = projectEntryClass;
            ProjectGithubAccount = projectGithubAccount;
            ProjectLicense = projectLicense;
        }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine($" \"name\": \"{ProjectName}\",");
            sb.AppendLine($" \"version\": \"{ProjectVersion}\",");
            sb.AppendLine($" \"description\": \"{ProjectDescription}\",");
            if ((bool)IsSetProjectEntryClass!)
            {
                sb.AppendLine($" \"main\": \"{ProjectEntryClass}\",");
            }
            sb.AppendLine("    \"scripts\": {");
            sb.AppendLine("    \"test\": \"echo \\\"Error: no test specified\\\" && exit 1\",");
            sb.AppendLine("    \"clean\": \"rm -rf node_modules && rm -rf dist && rm -rf logs && rm -rf package-lock.json\",");
            sb.AppendLine("    \"custom:install:no:ssl\": \"npm cache clear --force && npm config set strict-ssl false && npm install --legacy-peer-deps\",");
            sb.AppendLine("    \"custom:install:use:ssl\": \"npm cache clear --force && npm install --legacy-peer-deps\",");
            sb.AppendLine("    \"clean:install:no:ssl\": \"npm run clean && npm run custom:install:no:ssl\",");
            sb.AppendLine("    \"clean:install:use:ssl\": \"npm run clean && npm run custom:install:use:ssl\",");
            if ((bool)IsSetProjectEntryClass!)
            {
                sb.AppendLine($"    \"nodemon:local\": \"set APP_ENV=local && set NODE_ENV=local && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"nodemon:dev\": \"set APP_ENV=dev && set NODE_ENV=dev && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"nodemon:stg\": \"set APP_ENV=stg && set NODE_ENV=stg && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"nodemon:uat\": \"set APP_ENV=uat && set NODE_ENV=uat && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"nodemon:sit\": \"set APP_ENV=sit && set NODE_ENV=sit && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"nodemon:prod\": \"set APP_ENV=prod && set NODE_ENV=prod && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"nodemon:dr\": \"set APP_ENV=dr && set NODE_ENV=dr && nodemon src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:local\": \"set APP_ENV=local && set NODE_ENV=local && node src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:dev\": \"set APP_ENV=dev && set NODE_ENV=dev && node src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:stg\": \"set APP_ENV=stg && set NODE_ENV=stg && node src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:uat\": \"set APP_ENV=uat && set NODE_ENV=uat && node src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:sit\": \"set APP_ENV=sit && set NODE_ENV=sit && node src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:prod\": \"set APP_ENV=prod && set NODE_ENV=prod && node src/{ProjectEntryClass}\",");
                sb.AppendLine($"    \"start:dr\": \"set APP_ENV=dr && set NODE_ENV=dr && node src/{ProjectEntryClass}\",");
            }
            AddDockerEnvironmentCommands(sb);
            sb.AppendLine("    },");
            if (ProjectGithubAccount?.Length != 0)
            {
                sb.AppendLine(" \"repository\": {");
                sb.AppendLine($" \"type\": \"git\",");
                sb.AppendLine($" \"url\": \"git + https://github.com/{ProjectGithubAccount}/{ProjectName}.git\"");
                sb.AppendLine("  },");
            }
            sb.AppendLine($" \"keywords\": [{string.Join(',', ProjectKeywords!)}],");
            sb.AppendLine($" \"author\": \"{ProjectAuthor}\",");
            sb.AppendLine($" \"license\": \"{ProjectLicense.ToString()}\",");
            sb.AppendLine(" \"dependencies\": {");
            sb.AppendLine(" \"@types/express\": \"^4.17.15\",");
            sb.AppendLine(" \"bcrypt\": \"^5.1.0\",");
            sb.AppendLine(" \"body-parser\": \"^1.20.2\",");
            sb.AppendLine(" \"compression\": \"^1.7.4\",");
            sb.AppendLine(" \"cors\": \"^2.8.5\",");
            sb.AppendLine(" \"custom-env\": \"^2.0.1\",");
            sb.AppendLine(" \"crypto-js\": \"^4.1.1\",");
            sb.AppendLine(" \"dotenv\": \"^16.0.3\",");
            sb.AppendLine(" \"expose-gc\": \"^1.0.0\",");
            sb.AppendLine(" \"express\": \"^4.18.2\",");
            sb.AppendLine(" \"express-jwt\": \"^6.0.0\",");
            if ((bool)EnableSwagger!)
            {
                sb.AppendLine(" \"express-swagger-generator\": \"^1.1.17\",");
            }
            if ((bool)IsGateway!)
            {
                sb.AppendLine(" \"http-proxy-middleware\": \"^2.0.6\",");
            }
            sb.AppendLine(" \"jsonwebtoken\": \"^8.5.1\",");
            sb.AppendLine(" \"method-override\": \"^3.0.0\",");
            sb.AppendLine(" \"mongoose\": \"^7.4.0\",");
            sb.AppendLine(" \"path\": \"^0.12.7\",");
            sb.AppendLine(" \"pg\": \"^8.11.1\",");
            sb.AppendLine(" \"response-time\": \"^2.3.2\",");
            sb.AppendLine(" \"tedious\": \"^16.2.0\",");
            sb.AppendLine(" \"ts-node\": \"^10.9.1\",");
            sb.AppendLine(" \"typeorm\": \"^0.3.17\"");
            sb.AppendLine("  },");
            sb.AppendLine(" \"devDependencies\": {    ");
            sb.AppendLine(" \"@types/compression\": \"^1.7.2\",");
            sb.AppendLine(" \"@types/cookie-parser\": \"^1.4.3\",");
            sb.AppendLine(" \"@types/express\": \"^4.17.15\",");
            sb.AppendLine(" \"@types/node\": \"^18.11.18\",");
            sb.AppendLine(" \"custom-env\": \"^2.0.1\",");
            sb.AppendLine(" \"dotenv\": \"^16.0.3\",");
            sb.AppendLine(" \"eslint\": \"^8.31.0\",");
            if ((bool)EnableSwagger!)
            {
                sb.AppendLine(" \"express-swagger-generator\": \"^1.1.17\",");
            }
            sb.AppendLine(" \"nodemon\": \"^2.0.20\",");
            sb.AppendLine(" \"ts-node\": \"^10.9.1\",");
            sb.AppendLine(" \"typescript\": \"^4.9.4\"");
            sb.AppendLine("  },");
            if (ProjectGithubAccount?.Length != 0) { 
                sb.AppendLine(" \"bugs\": {");
            sb.AppendLine($" \"url\": \"https://github.com/{ProjectGithubAccount}/{ProjectName}/issues\"");
            sb.AppendLine("  },");
            sb.AppendLine($" \"homepage\": \"https://github.com/{ProjectGithubAccount}/{ProjectName}#readme\"");
                    }
            sb.AppendLine("}");
            return sb.ToString();
        }
        public void AddDockerEnvironmentCommands(StringBuilder stringBuilder)
        {
            string projectDockerImageContainerName = ProjectName?.Replace("jhb.", string.Empty).Replace("fl360.", string.Empty).Replace(".", "_")!;
            string projectDockerImageName = ProjectName?.Replace("jhb.", string.Empty).Replace( "fl360.", "fl360/").Replace( ".", "_" )!;
            stringBuilder.AppendLine($"    \"docker:build:package:local\": \"docker rmi {projectDockerImageName}.local:1.0.0 --force && docker build . -t {projectDockerImageName}.local:1.0.0 -f ./docker/local/Dockerfile.local && rm -rf {projectDockerImageContainerName}.local.tar.gz && docker save {projectDockerImageName}.local:1.0.0 | gzip > {projectDockerImageContainerName}.local.tar.gz\",");
            stringBuilder.AppendLine($"    \"docker:build:package:dev\": \"docker rmi {projectDockerImageName}.dev:1.0.0 --force && docker build . -t {projectDockerImageName}.dev:1.0.0 -f ./docker/dev/Dockerfile.dev && rm -rf {projectDockerImageContainerName}.dev.tar.gz && docker save {projectDockerImageName}.dev:1.0.0 | gzip > {projectDockerImageContainerName}.dev.tar.gz\",");
            stringBuilder.AppendLine($"    \"docker:build:package:stg\": \"docker rmi {projectDockerImageName}.stg:1.0.0 --force && docker build . -t {projectDockerImageName}.stg:1.0.0 -f ./docker/stg/Dockerfile.stg && rm -rf {projectDockerImageContainerName}.stg.tar.gz && docker save {projectDockerImageName}.stg:1.0.0 | gzip > {projectDockerImageContainerName}.stg.tar.gz\",");
            stringBuilder.AppendLine($"    \"docker:build:package:sit\": \"docker rmi {projectDockerImageName}.sit:1.0.0 --force && docker build . -t {projectDockerImageName}.sit:1.0.0 -f ./docker/sit/Dockerfile.sit && rm -rf {projectDockerImageContainerName}.sit.tar.gz && docker save {projectDockerImageName}.sit:1.0.0 | gzip > {projectDockerImageContainerName}.sit.tar.gz\",");
            stringBuilder.AppendLine($"    \"docker:build:package:uat\": \"docker rmi {projectDockerImageName}.uat:1.0.0 --force && docker build . -t {projectDockerImageName}.uat:1.0.0 -f ./docker/uat/Dockerfile.uat && rm -rf {projectDockerImageContainerName}.uat.tar.gz && docker save {projectDockerImageName}.uat:1.0.0 | gzip > {projectDockerImageContainerName}.uat.tar.gz\",");
            stringBuilder.AppendLine($"    \"docker:build:package:prod\": \"docker rmi {projectDockerImageName}.prod:1.0.0 --force && docker build . -t {projectDockerImageName}.prod:1.0.0 -f ./docker/prod/Dockerfile.prod && rm -rf {projectDockerImageContainerName}.prod.tar.gz && docker save {projectDockerImageName}.prod:1.0.0 | gzip > {projectDockerImageContainerName}.prod.tar.gz\",");
            stringBuilder.AppendLine($"    \"docker:build:package:dr\": \"docker rmi {projectDockerImageName}.dr:1.0.0 --force && docker build . -t {projectDockerImageName}.dr:1.0.0 -f ./docker/dr/Dockerfile.dr && rm -rf {projectDockerImageContainerName}.dr.tar.gz && docker save {projectDockerImageName}.dr:1.0.0 | gzip > {projectDockerImageContainerName}.dr.tar.gz\"");
        }
    }
}
