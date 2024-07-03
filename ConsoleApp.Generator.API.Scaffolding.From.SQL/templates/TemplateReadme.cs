using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates
{
    public class TemplateReadme
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectVersion { get; set; }
        public string? ProjectAuthor { get; set; }
        public string?[] ProjectModels { get; set; }
        public string FileName => "README.md";
        public TemplateReadme() { }
        public TemplateReadme(
            string projectName,
            string projectDescription,
            string projectVersion,
            string projectAuthor, string[] projectModels) : this()
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            ProjectVersion = projectVersion;
            ProjectAuthor = projectAuthor;
            ProjectModels = projectModels;
        }
        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"## {ProjectName}");
            sb.AppendLine("# Description");
            sb.AppendLine($"{ProjectDescription}");
            sb.AppendLine("");
            sb.AppendLine("# Dependecies");
            sb.AppendLine("## Data Model(s)");
            sb.AppendLine("The project process(es) data for the following model(s):");
            if(ProjectModels?.Length > 0)
            {
                foreach( var model in ProjectModels )
                {
                    sb.AppendLine($"* {model}");
                }
            }
            else
            {
                sb.AppendLine("* None");
            }
            sb.AppendLine("");
            sb.AppendLine("## Main package(s)");
            sb.AppendLine("* bcrypt			> 5.1.0 or latest");
            sb.AppendLine("* body-parser		> 1.20.2 or latest");
            sb.AppendLine("* compression		> 1.7.4 or latest");
            sb.AppendLine("* cors				> 2.8.5 or latest");
            sb.AppendLine("* custom-env		> 2.0.1 or latest");
            sb.AppendLine("* dotenv			> 16.0.3 or latest");
            sb.AppendLine("* express			> 4.18.2 or latest");
            sb.AppendLine("* express-jwt		> 6.0.0 or latest");
            sb.AppendLine("* jsonwebtoken		> 8.5.1 or latest");
            sb.AppendLine("* method-override	> 3.0.0 or latest");
            sb.AppendLine("* path				> 0.12.7 or latest");
            sb.AppendLine("* response-time		> 2.3.2 or latest");
            sb.AppendLine("## Development environment package(s)");
            sb.AppendLine("* types/compression		> 1.7.2 or latest");
            sb.AppendLine("* types/cookie-parser	> 1.4.3 or latest");
            sb.AppendLine("* types/express			> 4.17.15 or latest");
            sb.AppendLine("* types/node			> 18.11.18 or latest");
            sb.AppendLine("* custom-env			> 2.0.1 or latest");
            sb.AppendLine("* dotenv				> 16.0.3 or latest");
            sb.AppendLine("* eslint				> 8.31.0 or latest");
            sb.AppendLine("* nodemon				> 2.0.20 or latest");
            sb.AppendLine("* ts-node				> 10.9.1 or latest");
            sb.AppendLine("* typescript			> 4.9.4 or latest");
            sb.AppendLine("");
            sb.AppendLine("# Scaffolding (directory structure)");
            sb.AppendLine("* *./src/config* – the config folder is used to organize configuration files.");
            sb.AppendLine("* *./src/controllers* – the controllers directory is used to organise all the handlers for defined routes in the ./src/routes directory. As you’ll see in the routes/ component of the project, when the client “pings” any route for the different CRUD operations, there is a corresponding handler to represent each one of those operations. In the controllers we use the services’ logic to handle responses to the client. A good naming convention is the entity associated to that specific controller followed by controller");
            sb.AppendLine("* *./src/constants* - this is a component/directory designed for implementing constants and enumerables. Instead of using strings for data coming from a third party API, e.g. statuses, you can create constants in the data/ folder which you can reuse easily.");
            sb.AppendLine("* *./src/middlewares* - a separate project component for the middlewares that are used in the routes. A good practice that is followed is that subfolders have been created defining the purpose of each middleware defined. For instance, auth/ subfolder for all authentication middleware, store/ subfolder for all the middleware regarding the store entity, and so on.");
            sb.AppendLine("* *./src/models* – every model is an object matching the fields in the table by name and type. They do not make any changes to the data but simply represent it in your codebase.");
            sb.AppendLine("* *./src/routes* – everything related to routing within the project belongs here");
            sb.AppendLine("* *./src/services* - the business logic in the project is defined and stored in the files in this directory.");
            sb.AppendLine("* *./src/utils* – these are the so-called utility or “pure” functions. You pass them an input and they simply mutate it and return an output. They are called utility because you can use them in multiple parts of your system, regardless of the business logic behind them. Notable examples are date conversion or encryption/ decryption of functions.");
            sb.AppendLine("* *./src/validators* – validators are used mainly to approve payloads and configuration, which is essential for the API’s. When receiving data from the client, you want to make sure it is valid so your services can work with it.");
            sb.AppendLine("* *./src/tests* – after you have created the key components in your folder structure, it’s time to implement testing in your project (both unit and integration). Testing is really important when it comes to developing your applications, and I guarantee that the time and effort you put into it are worth it.");
            sb.AppendLine("* *./src/scripts* – create a scripts folder for some bash scripts which you can run easily afterwards.");
            return sb.ToString();
        }
    }
}
