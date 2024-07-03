using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js
{
    public class TemplateAppJS
    {
        public string? ProjectSrcDirectory { get; set; }
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? EntryClass { get; set; }
        public bool? IsGateway { get; set; }
        public bool? EnableSwagger { get; set; }
        public string FilePath => Path.Combine(ProjectSrcDirectory!, EntryClass);
        public TemplateAppJS() { }
        public TemplateAppJS(string projectSrcDirectory, string author, string dateCreated, string entryClass = "app.js", bool isGateway = false, bool enableSwagger = false) : this() {
            ProjectSrcDirectory = projectSrcDirectory;
            Author = author;
            DateCreated = dateCreated;
            EntryClass = entryClass.IfEmpty("app.js");
            IsGateway = isGateway;
            EnableSwagger = enableSwagger;
        }
        public void Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine("| Description   : Main entry point for the server (app)");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("");
            sb.AppendLine("require(`expose-gc`);");
            sb.AppendLine("");
            sb.AppendLine("const config = require(`./config/config`);");
            sb.AppendLine("const logger = require(`./middleware/logging/logger`)(config);");
            sb.AppendLine("");
            sb.AppendLine("try {");
            sb.AppendLine("  console.clear();");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| Dependency(ies)");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("  const http = require(`http`);");
            sb.AppendLine("  const express = require(`express`);");
            sb.AppendLine("  const bodyParser = require(`body-parser`);");
            sb.AppendLine("  const methodOverride = require(`method-override`);");
            sb.AppendLine("  const cors = require(`cors`);");
            sb.AppendLine("  const responseTime = require(`response-time`);");
            sb.AppendLine("");
            sb.AppendLine("  logger.info(");
            sb.AppendLine("    __filename,");
            sb.AppendLine("    `server.init`,");
            sb.AppendLine("    `Starting up the ${config.app.api.API_NAME}`");
            sb.AppendLine("  );");
            sb.AppendLine("");
            sb.AppendLine("  logger.info(__filename, `server.init`, `Dependencies loaded successfully`);");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| App");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("  const app = express();");
            sb.AppendLine("  app.use(");
            sb.AppendLine("    bodyParser.json({");
            sb.AppendLine("      parameterLimit: config.app.api.options.body_parser.parameterLimit,");
            sb.AppendLine("      limit: config.app.api.options.body_parser.limit,");
            sb.AppendLine("    })");
            sb.AppendLine("  );");
            sb.AppendLine("  app.use(");
            sb.AppendLine("    bodyParser.urlencoded({");
            sb.AppendLine("      parameterLimit: config.app.api.options.body_parser.parameterLimit,");
            sb.AppendLine("      limit: config.app.api.options.body_parser.limit,");
            sb.AppendLine("      extended: config.app.api.options.body_parser.extended,");
            sb.AppendLine("    })");
            sb.AppendLine("  );");
            sb.AppendLine("  app.use(methodOverride());");
            sb.AppendLine("  app.use(cors({ origin: `*` }));");
            sb.AppendLine("  app.use(responseTime());");
            sb.AppendLine("");
            sb.AppendLine("  const port = config.app.api.API_PORT || process.env.PORT;");
            sb.AppendLine("  app.set(`port`, port);");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| Server");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("  const server = http.createServer(app);");
            sb.AppendLine("");
            sb.AppendLine("  server.headersTimeout = config.app.api.API_HEADERS_TIMEOUT_MS;");
            sb.AppendLine("  server.keepAliveTimeout = config.app.api.API_KEEP_ALIVE_TIMEOUT_MS;");
            sb.AppendLine("  server.setTimeout(config.app.api.API_TIMEOUT_MS);");
            sb.AppendLine("");
            sb.AppendLine("  logger.info(");
            sb.AppendLine("    __filename,");
            sb.AppendLine("    `http.createServer`,");
            sb.AppendLine("    `Server initialised successfully`");
            sb.AppendLine("  );");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| Database");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("  const dbContext = require(`./middleware/database/context/db.context.factory`)(");
            sb.AppendLine("    config,");
            sb.AppendLine("    logger");
            sb.AppendLine("  ).getDBContext();");
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| Routes");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("  const apiPath = `/${config.app.api.API_BASE_PATH}`;");
            sb.AppendLine("  const routes = require(`./routes/routes`)(config, logger, app, dbContext);");
            sb.AppendLine("  app.use(apiPath, routes);");
            if ((bool)IsGateway!)
            {
                sb.AppendLine("");
                sb.AppendLine("/*");
                sb.AppendLine("	|------------------------------------------------------------------------------------------------------------------");
                sb.AppendLine("	| Proxy Route(s)");
                sb.AppendLine("	|------------------------------------------------------------------------------------------------------------------");
                sb.AppendLine("	*/");
                sb.AppendLine("  const { createProxyMiddleware } = require(`http-proxy-middleware`);");
                sb.AppendLine("  const proxyRoutes = require(`./routes/@proxy/routes`);");
                sb.AppendLine("  proxyRoutes.forEach((proxyRoute) => {");
                sb.AppendLine("    app.use(proxyRoute.url, createProxyMiddleware(proxyRoute.proxy));");
                sb.AppendLine("  });");
                sb.AppendLine("  logger.info(");
                sb.AppendLine("    __filename,");
                sb.AppendLine("    `app.use(${apiPath}, routes)`,");
                sb.AppendLine("    `${config.app.api.API_NAME} Router initialised successfully`");
                sb.AppendLine("  );");
            }
            if ((bool)EnableSwagger!)
            {
                sb.AppendLine("");
                sb.AppendLine("/*");
                sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
                sb.AppendLine("| Swagger API documentation");
                sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
                sb.AppendLine("*/");
                sb.AppendLine("  const expressSwagger = require(`express-swagger-generator`)(app);");
                sb.AppendLine("");
                sb.AppendLine("  let options = {");
                sb.AppendLine("    swaggerDefinition: {");
                sb.AppendLine("      info: {");
                sb.AppendLine("        description: `Farmers 360° Link - API for configuration management`,");
                sb.AppendLine("        title: `jhb.fl360.api.configuration.management`,");
                sb.AppendLine("        version: `1.0.0`,");
                sb.AppendLine("      },");
                sb.AppendLine("      host: `localhost:35362`,");
                sb.AppendLine("      basePath: `/v1`,");
                sb.AppendLine("      produces: [`application/json`, `application/xml`],");
                sb.AppendLine("      schemes: [`http`, `https`],");
                sb.AppendLine("    /*    securityDefinitions: {");
                sb.AppendLine("        JWT: {");
                sb.AppendLine("          type: `apiKey`,");
                sb.AppendLine("          in: `header`,");
                sb.AppendLine("          name: `Authorization`,");
                sb.AppendLine("          description: ``,");
                sb.AppendLine("        },");
                sb.AppendLine("      },*/");
                sb.AppendLine("    },");
                sb.AppendLine("    basedir: __dirname, //app absolute path");
                sb.AppendLine("    files: [`./routes/**/*.js`], //Path to the API handle folder");
                sb.AppendLine("  };");
                sb.AppendLine("  expressSwagger(options);");
            }
            sb.AppendLine("");
            sb.AppendLine("/*");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("| Server - Start up");
            sb.AppendLine("|------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine("*/");
            sb.AppendLine("  server.on(`connection`, function (socket) {");
            sb.AppendLine("    socket.setKeepAlive(config.app.api.API_KEEP_ALIVE_TIMEOUT_MS);");
            sb.AppendLine("    socket.setTimeout(config.app.api.API_TIMEOUT_MS);");
            sb.AppendLine("  });");
            sb.AppendLine("  server.on(`error`, function (error) {");
            sb.AppendLine("    logger.error(__filename, `server.on('error')`, error);");
            sb.AppendLine("  });");
            sb.AppendLine("  server.listen(port, function () {");
            sb.AppendLine("    logger.info(");
            sb.AppendLine("      __filename,");
            sb.AppendLine("      `server.listen`,");
            sb.AppendLine("      `${config.app.api.API_NAME} listening on ${config.app.api.API_URI}`");
            sb.AppendLine("    );");
            sb.AppendLine("    routes?.stack?.filter((stack) => stack?.route?.path).forEach((stack) => {");
            sb.AppendLine("      logger.info(");
            sb.AppendLine("        __filename,");
            sb.AppendLine("        `server.listen`,");
            sb.AppendLine("        `[${apiPath}${stack?.route?.path}]`");
            sb.AppendLine("      );");
            sb.AppendLine("    });");
            sb.AppendLine("  });");
            sb.AppendLine("} catch (exception) {");
            sb.AppendLine("  logger.fatal(");
            sb.AppendLine("    __filename,");
            sb.AppendLine("    `app.start`,");
            sb.AppendLine("    exception,");
            sb.AppendLine("    exception.stack || exception.stackTrace");
            sb.AppendLine("  );");
            sb.AppendLine("} finally {");
            sb.AppendLine("  global.gc();");
            sb.AppendLine("}");
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
