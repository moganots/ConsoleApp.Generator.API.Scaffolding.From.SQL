/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Main entry point for the server (app)
|------------------------------------------------------------------------------------------------------------------
 */

const config = require(`./config/config`);
const logger = require(`./middleware/logging/logger`)(config);

try {
  console.clear();

  /*
|------------------------------------------------------------------------------------------------------------------
| Dependency(ies)
|------------------------------------------------------------------------------------------------------------------
 */
  const http = require(`http`);
  const express = require(`express`);
  const bodyParser = require(`body-parser`);
  const methodOverride = require(`method-override`);
  const cors = require(`cors`);
  const responseTime = require(`response-time`);

  logger.info(
    __filename,
    `server.init`,
    `Starting up the ${config.app.api.API_NAME}`
  );

  logger.info(__filename, `server.init`, `Dependencies loaded successfully`);

  /*
|------------------------------------------------------------------------------------------------------------------
| App
|------------------------------------------------------------------------------------------------------------------
 */
  const app = express();
  app.use(
    bodyParser.json({
      parameterLimit: config.app.api.options.body_parser.parameterLimit,
      limit: config.app.api.options.body_parser.limit,
    })
  );
  app.use(
    bodyParser.urlencoded({
      parameterLimit: config.app.api.options.body_parser.parameterLimit,
      limit: config.app.api.options.body_parser.limit,
      extended: config.app.api.options.body_parser.extended,
    })
  );
  app.use(methodOverride());
  app.use(cors({ origin: `*` }));
  app.use(responseTime());

  const port = config.app.api.API_PORT || process.env.PORT;
  app.set(`port`, port);

  /*
|------------------------------------------------------------------------------------------------------------------
| Server
|------------------------------------------------------------------------------------------------------------------
 */
  const server = http.createServer(app);

  server.headersTimeout = config.app.api.API_HEADERS_TIMEOUT_MS;
  server.keepAliveTimeout = config.app.api.API_KEEP_ALIVE_TIMEOUT_MS;
  server.setTimeout(config.app.api.API_TIMEOUT_MS);

  logger.info(
    __filename,
    `http.createServer`,
    `Server initialised successfully`
  );

  /*
|------------------------------------------------------------------------------------------------------------------
| Database
|------------------------------------------------------------------------------------------------------------------
*/
  const dbContext = require(`./middleware/database/context/db.context.factory`)(
    config,
    logger
  ).getDBContext();

  /*
|------------------------------------------------------------------------------------------------------------------
| Routes
|------------------------------------------------------------------------------------------------------------------
*/
  const routes = require(`./routes/routes`)(config, logger, app, dbContext);
  const apiPath = `/${config.app.api.API_BASE_PATH}`;
  app.use(apiPath, routes);
  logger.info(
    __filename,
    `app.use(${apiPath}, routes)`,
    `${config.app.api.API_NAME} Router initialised successfully`
  );

  /*
|------------------------------------------------------------------------------------------------------------------
| Swagger API documentation
|------------------------------------------------------------------------------------------------------------------
 */
  
  // const expressSwagger = require(`express-swagger-generator`)(app);

  // let options = {
  //   swaggerDefinition: {
  //     info: {
  //       description: `Farmers 360° Link - API for configuration management`,
  //       title: `jhb.fl360.api.configuration.management`,
  //       version: `1.0.0`,
  //     },
  //     host: `localhost:35362`,
  //     basePath: `/v1`,
  //     produces: [`application/json`, `application/xml`],
  //     schemes: [`http`, `https`],
  //     /*    securityDefinitions: {
  //       JWT: {
  //         type: `apiKey`,
  //         in: `header`,
  //         name: `Authorization`,
  //         description: ``,
  //       },
  //     }, */
  //   },
  //   basedir: __dirname, //app absolute path
  //   files: [`./routes/**/*.js`], //Path to the API handle folder
  // };
  // expressSwagger(options);

  /*
|------------------------------------------------------------------------------------------------------------------
| Server - Start up
|------------------------------------------------------------------------------------------------------------------
 */
  server.on(`connection`, function (socket) {
    socket.setKeepAlive(config.app.api.API_KEEP_ALIVE_TIMEOUT_MS);
    socket.setTimeout(config.app.api.API_TIMEOUT_MS);
  });
  server.on(`error`, function (error) {
    logger.error(__filename, `server.on('error')`, error);
  });
  server.listen(port, function () {
    logger.info(
      __filename,
      `server.listen`,
      `${config.app.api.API_NAME} listening on ${config.app.api.API_URI}`
    );
    routes?.stack?.forEach((stack) => {
      logger.info(
        __filename,
        `server.listen`,
        `[${apiPath}${stack?.route?.path}]`
      );
    });
  });
} catch (exception) {
  logger.fatal(
    __filename,
    `app.start`,
    exception,
    exception.stack || exception.stackTrace
  );
} finally {
}
