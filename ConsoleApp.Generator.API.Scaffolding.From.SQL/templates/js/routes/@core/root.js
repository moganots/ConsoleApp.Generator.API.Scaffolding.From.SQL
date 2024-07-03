/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Core Root route module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the core Root route module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @param {*} app an instance of the Express app
 * @returns RootRouteModule
 */
module.exports = function (config, logger, app) {
  const { setRoute } = require(`./../../utils/general/http.util`)();
  const { notSet } = require(`./../../utils/general/object.util`)();

  if (notSet(config)) {
    throw new Error(`RootRouteModule - [config] instance has not been provided or set`);
  }
  if (notSet(logger)) {
    throw new Error(`RootRouteModule - [logger] instance has not been provided or set`);
  }
  if (notSet(app)) {
    throw new Error(`RootRouteModule - [app] instance has not been provided or set`);
  }

  const heartbeat = require(`./../../middleware/routing/heartbeat`)(config, logger);

  // ping
  app.route(setRoute(`ping`)).get(heartbeat.ping);
};
