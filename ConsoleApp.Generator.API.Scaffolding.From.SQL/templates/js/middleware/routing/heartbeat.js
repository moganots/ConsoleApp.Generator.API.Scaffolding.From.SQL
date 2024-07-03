/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Core Heartbeat routing middleware module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the core Heartbeat route module
 * @param {*} config an instance of the configuration class and options
 * @param {*} logger an instance of the logger class
 * @returns Heartbeat
 */
module.exports = (config, logger) => {
  const { notSet } = require(`../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `HeartbeatRoute - [logger] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `HeartbeatRoute - [config] instance has not been provided or set`
    );
  }
  const { getRequestPacket } = require(`../../utils/general/http.util`)();
  const getApiName = (config) => {
    return `${config?.app?.api?.API_NAME}`;
  };
  const addRequestAnchorName = (request) => {
    const packet = getRequestPacket(request);
    return [``, `api`, `ping`].includes(packet?.anchorName)
      ? ``
      : ` (${packet?.anchorName})`;
  };
  const ping = (request, response) => {
    return logger?.onHttpRequestCompleted(__filename, request, response, {
      message: `${getApiName(config)}${addRequestAnchorName(
        request
      )} is up and running`,
    });
  };
  return {
    ping,
  };
};
