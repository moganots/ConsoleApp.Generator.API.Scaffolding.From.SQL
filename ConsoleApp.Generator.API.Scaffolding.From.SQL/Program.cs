using ConsoleApp.Generator.API.Scaffolding.From.SQL.configuration;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.docker;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.env;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.controllers;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.models.entities;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.routes;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.ts;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.classes;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.interfaces;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

var hasErrors = false;

try
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Green;
    // ===================== Configuration =====================================================================================================
    var appSettings = ConfigurationManager.AppSettings;
    var companyName = appSettings["companyName"];
    var connectionStrings = ConfigurationManager.ConnectionStrings;
    var fl360DBConnectionString = connectionStrings["fl360.db"].ConnectionString;
    var projectScaffoldingConfig = (ProjectScaffoldingSection)ConfigurationManager.GetSection("projectScaffolding");
    var projects = string.Join(" UNION\r\n", projectScaffoldingConfig
        .ProjectScaffolding
        .Cast<ProjectScaffoldingElement>()
        .Select((project, index) => $"SELECT '{project.Name.GetFormattedFL360ProjectCode(index)}' AS [code], '{project.Name.GetFormattedFL360ProjectName(index)}' AS [name], {project.ApiPort} AS [port], 'API' AS [application_type_id], '{project.Description}' AS [description]")
        .ToArray());
    var proxyProjects = projectScaffoldingConfig
        .ProjectScaffolding
        .Cast<ProjectScaffoldingElement>()
        .Where((project, index) => !project.IsGateway)
        .Select((project, index) => new ProxyProject { Name = project.Name, ApiPort = project.ApiPort, IsActive = project.IsActive})
        .ToArray();
    var proxyRoutes = projectScaffoldingConfig
        .ProjectScaffolding
        .Cast<ProjectScaffoldingElement>()
        .Where((project, index) => !project.IsGateway)
        .ToArray();
    //string applicationName = "Genio";
    //string attachmentsDirectory = @"c:\data\{0}\{1}\secure\attachments";
    // ===================== CreatedBy, DateCreated ============================================================================================
    string createdBy = Environment.UserName;
    string dateCreated = DateTime.Now.ToString("yyyy-MM-dd");
    // ===================== Variables =========================================================================================================
    var modelsDirectories = new List<string>();
    // ===================== Start : Processing ================================================================================================
    // 1. Create all directories, for each project scaffolding instance
    foreach (ProjectScaffoldingElement project in projectScaffoldingConfig.ProjectScaffolding)
    {
        var projectName = project.Name;
        var usesAuthentication = project.UsesAuthentication;
        var projectRootDirectory = project.RootDirectory;
        var projectDirectory = Path.Combine(projectRootDirectory, projectName);
        var projectDockerDirectory = Path.Combine(projectDirectory, "docker");
        var projectSrcDirectory = Path.Combine(projectDirectory, "src");
        var dataModelNames = project.DataModels.Cast<ProjectScaffoldingDataModelElement>().Select(e => e.Name).ToArray();
        string directoryPath = string.Empty;
        string subDirectoryPath = string.Empty;
        string subChildDirectoryPath = string.Empty;
        string subSubChildDirectoryPath = string.Empty;
        Console.WriteLine($"Creating directory scaffolding for Project Name: {projectName} in Root Directory: {projectDirectory}");
        new DirectoryIO(projectDirectory).Create();
        new DirectoryIO(projectDockerDirectory).Recreate(true);
        new DirectoryIO(projectSrcDirectory).Recreate(true);
        foreach (ProjectScaffoldingDirectoryElement directory in project.Directories)
        {
            directoryPath = Path.Combine(projectDirectory, directory.AnchorDirectory, directory.Name);
            cleanDirectory(directoryPath, project.CanUseMongoDb, project.CanUseMsSqlDb, project.CanUsePostgreSqlDb);
            foreach (ProjectScaffoldingDirectoryElement subDirectory in directory.Directories)
            {
                subDirectoryPath = Path.Combine(directoryPath, subDirectory.AnchorDirectory, subDirectory.Name);
                cleanDirectory(subDirectoryPath, project.CanUseMongoDb, project.CanUseMsSqlDb, project.CanUsePostgreSqlDb);
                foreach (ProjectScaffoldingDirectoryElement subChildDirectory in subDirectory.Directories)
                {
                    subChildDirectoryPath = Path.Combine(subDirectoryPath, subChildDirectory.AnchorDirectory, subChildDirectory.Name);
                    cleanDirectory(subChildDirectoryPath, project.CanUseMongoDb, project.CanUseMsSqlDb, project.CanUsePostgreSqlDb);
                    foreach (ProjectScaffoldingDirectoryElement subSubChildDirectory in subChildDirectory.Directories)
                    {
                        subSubChildDirectoryPath = Path.Combine(subChildDirectoryPath, subSubChildDirectory.AnchorDirectory, subSubChildDirectory.Name);
                        cleanDirectory(subSubChildDirectoryPath, project.CanUseMongoDb, project.CanUseMsSqlDb, project.CanUsePostgreSqlDb);
                    }
                }
            }

            if (directoryPath.GetLastElement('\\') == "utils")
            {
                new DirectoryIO(Path.Combine(Environment.CurrentDirectory, "templates", "js", "utils")).CopyRecursively(Path.Combine(projectSrcDirectory, "utils"), SearchOption.AllDirectories, project.Author, dateCreated, true, true, true);
            }

            if (directoryPath.GetLastElement('\\') == "config")
            {
                new TemplateEnvironmentConfig()
                    .GenerateDefaultConfig(Path.Combine(directoryPath, "environment", ".env"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.")
                    .GenerateDevConfig(Path.Combine(directoryPath, "environment", ".env.dev"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.")
                    .GenerateStagingConfig(Path.Combine(directoryPath, "environment", ".env.stg"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.")
                    .GenerateIntegrationConfig(Path.Combine(directoryPath, "environment", ".env.sit"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.")
                    .GenerateUatConfig(Path.Combine(directoryPath, "environment", ".env.uat"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.")
                    .GenerateProductionConfig(Path.Combine(directoryPath, "environment", ".env.prod"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.")
                    .GenerateDisasterRecoveryConfig(Path.Combine(directoryPath, "environment", ".env.dr"), project.Name, companyName, project.ApiPort, "jhb.fl360.api.");
                new TemplateConfig(Path.Combine(directoryPath, "config.js"), project.Author, dateCreated).Generate();
            }

            // Setup authentication scaffolding
            new TemplateAuthenticationConfig(projectSrcDirectory, project.Author, dateCreated, usesAuthentication)
                .GenerateConstants(Path.Combine(Environment.CurrentDirectory, "templates", "js", "constants", "authentication"))
                .GenerateMiddleware(Path.Combine(Environment.CurrentDirectory, "templates", "js", "middleware", "authentication"))
                .GenerateRoutes(Path.Combine(Environment.CurrentDirectory, "templates", "js", "routes", "route.authentication"));

            if (directoryPath.GetLastElement('\\') == "controllers")
            {
                new DirectoryIO(Path.Combine(Environment.CurrentDirectory, "templates", "js", "controllers")).CopyRecursively(Path.Combine(projectSrcDirectory, "controllers"), SearchOption.AllDirectories, project.Author, dateCreated, project.CanUseMongoDb, project.CanUseMsSqlDb, project.CanUsePostgreSqlDb);
            }

            if (directoryPath.GetLastElement('\\') == "middleware")
            {
                new DirectoryIO(Path.Combine(Environment.CurrentDirectory, "templates", "js", "middleware", "database")).CopyRecursively(Path.Combine(projectSrcDirectory, "middleware", "database"), SearchOption.AllDirectories, project.Author, dateCreated, project.CanUseMongoDb, project.CanUseMsSqlDb, project.CanUsePostgreSqlDb);
                new DirectoryIO(Path.Combine(Environment.CurrentDirectory, "templates", "js", "middleware", "logging")).CopyRecursively(Path.Combine(projectSrcDirectory, "middleware", "logging"), SearchOption.AllDirectories, project.Author, dateCreated, true, true, true);
                new DirectoryIO(Path.Combine(Environment.CurrentDirectory, "templates", "js", "middleware", "routing")).CopyRecursively(Path.Combine(projectSrcDirectory, "middleware", "routing"), SearchOption.AllDirectories, project.Author, dateCreated, true, true, true);
            }

            if (directoryPath.GetLastElement('\\') == "routes")
            {
                new DirectoryIO(Path.Combine(Environment.CurrentDirectory, "templates", "js", "routes", "@core")).CopyRecursively(Path.Combine(projectSrcDirectory, "routes", "@core"), SearchOption.AllDirectories, project.Author, dateCreated, true, true, true);
            }
        }

        // Create ./docker/files/*
        new TemplateDockerEnvironmentFile(project.Name, project.ApiPort, project.IsActive, project.IsGateway, projectDockerDirectory, new string[] { "local", "dev", "stg", "sit", "uat", "prod", "dr" })
            .GenerateDockerFileLocal()
            .GenerateDockerFileDev()
            .GenerateDockerFileStaging()
            .GenerateDockerFileIntegration()
            .GenerateDockerFileUat()
            .GenerateDockerFileProduction()
            .GenerateDockerFileDisasterRecovery()
            .GenerateDockerComposeYamlFile(proxyProjects);

        using (SqlConnection connection = new SqlConnection(fl360DBConnectionString))
        {
            // Open the connection to the database
            connection.Open();
            // Get a collection of all tables from the database schema
            DataTable tables = connection.GetSchema("Tables");
            var routeTables = tables.Rows.Cast<DataRow>().Where(t => dataModelNames.Contains(t[2].ToString()));
            var projectModels = routeTables.Select(rt => string.Format("[{0}].[{1}]", rt[1], rt[2]));
            // Iterate through all the tables
            foreach (DataRow row in routeTables)
            {
                // Get the SchemaName
                string? schemaName = row[1]?.ToString();
                // Get the TableName
                string? tableName = row[2]?.ToString();
                // Instantiate a SqlCommand to get the Schema of any table [schemaName].[tableName]
                using (SqlCommand command = new SqlCommand("SELECT TOP 0 * FROM [" + schemaName + "].[" + tableName + "];", connection))
                {
                    // Execute the command
                    using (var reader = command.ExecuteReader())
                    {
                        // Open the command reader
                        reader.Read();
                        // Get the table schema
                        var table = reader.GetSchemaTable();

                        // Generate models/entities/mongodb
                        if (project.CanUseMongoDb)
                        {
                            new TemplateMongoDBEntityJS(
                            project.Author,
                            dateCreated,
                            schemaName,
                            tableName,
                            Path.Combine(projectSrcDirectory, "models", "entities", "mongodb"),
                            string.Join(',',
                                table
                                    .Rows
                                    .Cast<DataRow>()
                                    .Select((dr, idx) => $"{idx.AppendMongoDBRowTab()}{dr["ColumnName"]}: {{ type: {dr["DataType"].GetMongoDBDataType()}, required: false }}").ToArray())
                            ).GenerateModel();
                        }

                        // Generate models/entities/mssql
                        if (project.CanUseMsSqlDb)
                        {
                            new TemplateMsSqlDBEntityJS(
                            project.Author,
                            dateCreated,
                            schemaName,
                            tableName,
                            Path.Combine(projectSrcDirectory, "models", "entities", "mssql"),
                            string.Join(", ", table.Rows.Cast<DataRow>().Select((tr) => $"entity.{tr["ColumnName"]}").ToArray()).Trim(),
                            string.Join(", ", table.Rows.Cast<DataRow>().Select((tr) => $"{tr["ColumnName"]}").ToArray()).Trim(),
                            string.Join("\r\n", table.Rows.Cast<DataRow>().Select((tr) => $"\t\t\t{tr["ColumnName"]}:{tr["ColumnName"]},").ToArray()).Trim()
                            ).GenerateModel();
                        }

                        // Generate models/entities/postgres
                        if (project.CanUsePostgreSqlDb)
                        {
                            new TemplatePostgresDBEntityJS(
                            project.Author,
                            dateCreated,
                            schemaName,
                            tableName,
                            Path.Combine(projectSrcDirectory, "models", "entities", "postgres"),
                            string.Join(',',
                                table
                                    .Rows
                                    .Cast<DataRow>()
                                    .Select((dr, idx) => $"{idx.AppendPostgresDBRowTab()}{dr["ColumnName"]}: {{ type: `{dr["DataType"].GetPostgresDBDataType()}` }}").ToArray())
                            ).GenerateModel();
                        }
                    }
                }

                // Generate controllers/database/controller/mongodb
                if (project.CanUseMongoDb)
                {
                    new TemplateMongoDBEntityControllerJS(
                        project.Author,
                        dateCreated,
                        schemaName,
                        tableName,
                        Path.Combine(projectSrcDirectory, "controllers", "mongodb"))
                        .Generate();
                }

                // Generate controllers/database/controller/mssql
                if (project.CanUseMsSqlDb)
                {
                    new TemplateMsSqlEntityControllerJS(
                    project.Author,
                    dateCreated,
                    schemaName,
                    tableName,
                    Path.Combine(projectSrcDirectory, "controllers", "mssql"))
                    .Generate();
                }

                // Generate controllers/database/controller/postgres
                if (project.CanUsePostgreSqlDb)
                {
                    new TemplatePostgresEntityControllerJS(
                    project.Author,
                    dateCreated,
                    schemaName,
                    tableName,
                    Path.Combine(projectSrcDirectory, "controllers", "postgres"))
                    .Generate();
                }

                // Generate routes/{EntityName/TableName}
                new TemplateEntityRouteJS(
                    project.Author,
                    dateCreated,
                    Path.Combine(projectSrcDirectory, "routes"),
                    schemaName,
                    tableName).Generate();
            }
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            new TemplateDBEntityFactoryJS(
                project.Author,
                dateCreated,
                Path.Combine(projectSrcDirectory, "models", "entities"),
                routeTables.Select(rt => rt[2].ToString()).ToArray()).Generate();
            new TemplateControllerFactoryJS(
                project.Author,
                dateCreated,
                Path.Combine(projectSrcDirectory, "controllers"),
                routeTables.Select(rt => rt[2].ToString()).ToArray(),
                project.CanUseMongoDb,
                project.CanUseMsSqlDb,
                project.CanUsePostgreSqlDb).Generate();
            if (project.IsGateway)
            {
                new TemplateProxyRoutesJS(
                    projectName
                    , project.Author
                    , dateCreated
                    , Path.Combine(projectSrcDirectory, "routes", "@proxy")
                    , proxyRoutes
                    ).Generate();
            }
            new TemplateRoutesJS(
                project.Author
                , dateCreated
                , Path.Combine(projectSrcDirectory, "routes")
                , string.Join("\r\n", routeTables.Select(rt => $"\tconst route{rt[2].FormatRouteEntityName()} = require(`./{rt[2].FormatRouteEntityDirectoryName()}/api`)(config, logger, app, dbContext);"))
                , project.IsGateway
                , usesAuthentication
            ).Generate();
            addRootDirectoryTemplateFiles(projectDirectory, projectName, project.Description, project.Version, project.Author, project.Keywords?.Split(',')?.Select(kw => $"\"{kw.Trim()}\"")?.ToArray(), project.EntryClass, project.GithubAccount, project.ApiPort, projectModels.ToArray(), project.IsGateway, project.EnableSwagger);
        }
        addProjectTsPlaceholder(projectSrcDirectory, project.Author, dateCreated);
        addProjectEntryclass(projectSrcDirectory, project.EntryClass, project.Author, dateCreated, projectName, project.Description, project.Version, project.ApiPort, project.IsGateway);
    }
}
catch (Exception exception)
{
    hasErrors = true;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($">> ERROR:{exception.Message}\r\n{exception.StackTrace}");
}
finally
{
    Console.ForegroundColor = hasErrors ? ConsoleColor.Red : ConsoleColor.Green;
    Console.WriteLine($"Completed {(!hasErrors ? "successfully" : "with error(s)")}");
}

static void cleanDirectory(string? path, bool? canUseMongoDb = false, bool? canUseMsSqlDb = false, bool? canUsePostgreSqlDb = true)
{
    if(Directory.Exists(path))
    {
        Directory.Delete(path, true);
        Console.WriteLine($"Directory {path}, cleaned successfully");
        createDirectoryIfNotExists(path, canUseMongoDb, canUseMsSqlDb, canUsePostgreSqlDb);
    }
    else
    {
        createDirectoryIfNotExists(path, canUseMongoDb, canUseMsSqlDb, canUsePostgreSqlDb);
    }
}

static void createDirectoryIfNotExists(string? path, bool? canUseMongoDb = false, bool? canUseMsSqlDb = false, bool? canUsePostgreSqlDb = true)
{
    bool isDbDirectory = (path.Contains("database") || path.Contains("connection") || path.Contains("context") || path.Contains("entities") || path.Contains("models") || path.Contains("repository"));
    bool canCopyMongoDbDirectory = isDbDirectory && (path.Contains("mongo") || path.Contains("mongodb")) && (bool)canUseMongoDb!;
    bool canCopyMsSqlDbDirectory = isDbDirectory && (path.Contains("mssql") || path.Contains("sql")) && (bool)canUseMsSqlDb!;
    bool canCopyPostgreSqlDbDirectory = isDbDirectory && (path.Contains("postgres") || path.Contains("postgresql") || path.Contains("postgressql")) && (bool)canUsePostgreSqlDb!;
    if (!Directory.Exists(path) && !isDbDirectory) {
        Directory.CreateDirectory(path);
        Console.WriteLine($"Directory {path}, created successfully");
    }else if (!Directory.Exists(path) && canCopyMongoDbDirectory)
    {
        Directory.CreateDirectory(path);
        Console.WriteLine($"Directory {path}, created successfully");
    }
    else if (!Directory.Exists(path) && canCopyMsSqlDbDirectory)
    {
        Directory.CreateDirectory(path);
        Console.WriteLine($"Directory {path}, created successfully");
    }
    else if (!Directory.Exists(path) && canCopyPostgreSqlDbDirectory)
    {
        Directory.CreateDirectory(path);        
        Console.WriteLine($"Directory {path}, created successfully");
    }
}

static void addRootDirectoryTemplateFiles(string projectDirectory, string projectName, string projectDescription, string projectVersion, string projectAuthor, string[]? projectKeywords, string? projectEntryclass, string? projectGithubAccount, string? projectApiPort, string[]? projectModels, bool? isGateway = false, bool? enableSwagger = false)
{
    TemplatePackageJson templatePackageJson = new TemplatePackageJson(projectName, projectDescription, projectVersion, projectAuthor, projectKeywords, projectEntryclass, projectGithubAccount, isGateway, enableSwagger);
    new FileIO(Path.Combine(projectDirectory, templatePackageJson.FileName)).Replace(templatePackageJson.Generate());
    TemplateGitIgnore templateGitIgnore = new TemplateGitIgnore(projectName, projectDescription, projectVersion, projectAuthor);
    new FileIO(Path.Combine(projectDirectory, templateGitIgnore.FileName)).Replace(templateGitIgnore.Generate());
    TemplateTsConfig templateTsConfig = new TemplateTsConfig(projectName, projectDescription, projectVersion, projectAuthor);
    new FileIO(Path.Combine(projectDirectory, templateTsConfig.FileName)).Replace(templateTsConfig.Generate());
    TemplateReadme templateReadme = new TemplateReadme(projectName, projectDescription, projectVersion, projectAuthor, projectModels);
    new FileIO(Path.Combine(projectDirectory, templateReadme.FileName)).Replace(templateReadme.Generate());
}

static void addProjectTsPlaceholder(string projectSrcDirectory, string author, string dateCreated)
{
    new TemplateTsPlaceholder(projectSrcDirectory, author, dateCreated).Generate();
}

static void addProjectEntryclass(string projectSrcDirectory, string projectEntryclass, string author, string dateCreated, string projectName, string projectDescription, string projectVersion, string projectApiPort, bool isGateway = false)
{
    switch (projectEntryclass?.GetLastElement())
    {
        case "js":
            new TemplateAppJS(projectSrcDirectory, author, dateCreated, projectEntryclass, isGateway).Generate();
            break;
    }
}

static void replaceFile(string path, string content){
    if (File.Exists(path)) { File.Delete(path); }
    File.WriteAllText(path, content);
    Console.WriteLine($"File {path}, created successfully");
}

Console.ReadLine();
Console.WriteLine("Bye...");