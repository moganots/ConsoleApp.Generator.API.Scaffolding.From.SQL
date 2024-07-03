/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Authentication route module
|------------------------------------------------------------------------------------------------------------------
*/

/**
 * Creates an instance of the Authentication module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @param {*} app an instance of the Express app
 * @param {*} dbContext an instance of the DBContext
 * @returns AuthenticationRouteModule
 */
module.exports = function (config, logger, app, dbContext) {
  const { setRoute } = require(`./../../utils/general/http.util`)();
  const { notSet } = require(`./../../utils/general/object.util`)();

  if (notSet(config)) {
    throw new Error(`AuthenticationRouteModule - [config] instance has not been provided or set`);
  }
  if (notSet(logger)) {
    throw new Error(`AuthenticationRouteModule - [logger] instance has not been provided or set`);
  }
  if (notSet(app)) {
    throw new Error(`AuthenticationRouteModule - [app] instance has not been provided or set`);
  }
  if (notSet(dbContext)) {
    throw new Error(`AuthenticationRouteModule - [dbContext] instance has not been provided or set`);
  }

  const controller = require(`./../../controllers/authentication/controller`)(
    config,
    logger,
    dbContext
  );

  // register
  app.route(setRoute(`register`)).post(controller.register);

  // login
  app.route(setRoute(`login`)).post(controller.login);

  // logout
  app.route(setRoute(`logout`)).post(controller.logout);

  // tokenVerify
  app.route(setRoute(`tokenVerify`)).get(controller.tokenVerify);

  // tokenCancel
  app.route(setRoute(`tokenCancel`)).post(controller.tokenCancel);
};
