/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Logging middleware extension utility module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates a new Logger instance
 * @param {*} config an instance of the configuration module
 */
const Logger = (config) => {
  const { isSet, notSet, ifJoin, ifThen } =
    require(`./../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `Logger - Configuration instance not instantiated or provided`
    );
  }
  if (notSet(config.app)) {
    throw new Error(
      `Logger - Configuration [app] instance not instantiated or provided`
    );
  }
  if (notSet(config.app.environment)) {
    throw new Error(
      `Logger - Configuration [environment] instance not instantiated or provided`
    );
  }
  if (notSet(config.app.database)) {
    throw new Error(
      `Logger - Configuration [database] instance not instantiated or provided`
    );
  }
  if (notSet(config.app.api)) {
    throw new Error(
      `Logger - Configuration [api] instance not instantiated or provided`
    );
  }
  if (notSet(config.app.logging)) {
    throw new Error(
      `Logger - Configuration [logging] instance not instantiated or provided`
    );
  }
  const { yyyymmddThmsmsZ0200WithDashSeparator } =
    require(`./../../utils/general/date.util`)();
  const { getRequestPacket, getRequestStatusCode } =
    require(`./../../utils/general/http.util`)();
  const { hasEmptyString } = require(`./../../utils/general/string.util`)();
  const { formatLogMessage, formatMessage, logToConsole, logToFile } =
    require(`././../../utils/general/logging.util`)(
      config.app.environment.APP_ENV,
      config.app.logging.LOG_DIRECTORY,
      config.app.logging.LOG_FILE_FULL_PATH
    );
  const dbConfig = () => {
    switch (config.app.database.DB_CONTEXT) {
      case `MONGO_DB`:
        return config.app.database.MONGO_DB;
      case `POSTGRES`:
        return config.app.database.POSTGRES;
      case `MSSQL`:
      default:
        return config.app.database.MSSQL;
    }
  };

  const debug = (caller, methodName, message, detailMessage = null) => {
    log(caller, `DEBUG`, methodName, message, detailMessage);
  };
  const info = (caller, methodName, message, detailMessage = null) => {
    log(caller, `INFO`, methodName, message, detailMessage);
  };
  const warn = (caller, methodName, message, detailMessage = null) => {
    log(caller, `WARN`, methodName, message, detailMessage);
  };
  const error = (caller, methodName, message, detailMessage = null) => {
    log(caller, `ERROR`, methodName, message, detailMessage);
  };
  const fatal = (caller, methodName, message, detailMessage = null) => {
    log(caller, `FATAL`, methodName, message, detailMessage);
  };
  const critical = (caller, methodName, message, detailMessage = null) => {
    log(caller, `CRITICAL`, methodName, message, detailMessage);
  };
  const log = (caller, logType, methodName, message, detailMessage = null) => {
    if (hasEmptyString([caller, logType, methodName]) || notSet(message))
      return;
    if (Array.isArray(message) && detailMessage) {
      message.push(detailMessage);
    } else if (message && detailMessage) {
      message = [message, detailMessage];
    }
    const logMessage = formatLogMessage(
      yyyymmddThmsmsZ0200WithDashSeparator(),
      logType,
      (caller || __filename).replace(/\\/g, `/`),
      methodName,
      formatMessage(message)
    );
    logToConsole(logType, logMessage);
    logToFile(logMessage);
  };
  const onHttpRequestCompleted = (
    caller,
    request,
    response,
    result = {
      data: null,
      hasError: false,
      message: null,
      messageDetail: null,
    }
  ) => {
    caller = caller || __filename;
    const logType = request?.hasError ? `ERROR` : `INFO`;
    const methodName = getRequestPacket(request)?.action;
    const statusCode = getRequestStatusCode(logType, methodName);
    log(caller, logType, methodName, result?.message, result?.messageDetail);
    return response?.status(statusCode).json({
      hasData: isSet(result?.data),
      data: result?.data,
      dataCount: notSet(result?.data)
        ? 0
        : Array.isArray(result?.data)
        ? result?.data?.length
        : 1,
      hasError: result?.hasError,
      message: ifJoin(`\r\n`, result?.message, result?.messageDetail)
    });
  };

  log(__filename, `INFO`, `init`, `Loading configuration`);

  // Application configuration
  log(
    __filename,
    `INFO`,
    `init`,
    `APP_ROOT_DIR=${config.app.environment.APP_ROOT_DIR}`
  );
  log(__filename, `INFO`, `init`, `APP_ENV=${config.app.environment.APP_ENV}`);
  log(
    __filename,
    `INFO`,
    `init`,
    `NODE_ENV=${config.app.environment.NODE_ENV}`
  );
  log(
    __filename,
    `INFO`,
    `init`,
    `APP_ENV_CONFIG_NAME=${config.app.environment.APP_ENV_CONFIG_NAME}`
  );
  log(
    __filename,
    `INFO`,
    `init`,
    `APP_ENV_CONFIG_PATH=${config.app.environment.APP_ENV_CONFIG_PATH}`
  );

  // API configuration
  log(
    __filename,
    `INFO`,
    `init`,
    `API_PROTOCOL=${config.app.api.API_PROTOCOL}`
  );
  log(__filename, `INFO`, `init`, `API_HOST=${config.app.api.API_HOST}`);
  log(__filename, `INFO`, `init`, `API_PORT=${config.app.api.API_PORT}`);
  log(
    __filename,
    `INFO`,
    `init`,
    `API_RELATIVE_PATH=${config.app.api.API_RELATIVE_PATH}`
  );

  // Database configuration
  log(
    __filename,
    `INFO`,
    `init`,
    `DB_CONTEXT=${config.app.database.DB_CONTEXT}`
  );
  log(__filename, `INFO`, `init`, `DB_CONNECTION=${JSON.stringify(dbConfig())}`);

  // Logging configuration
  log(
    __filename,
    `INFO`,
    `init`,
    `LOG_DIRECTORY=${config.app.logging.LOG_DIRECTORY}`
  );
  log(
    __filename,
    `INFO`,
    `init`,
    `LOG_FILE_NAME=${config.app.logging.LOG_FILE_NAME}`
  );
  log(
    __filename,
    `INFO`,
    `init`,
    `LOG_FILE_FULL_PATH=${config.app.logging.LOG_FILE_FULL_PATH}`
  );
  log(
    __filename,
    `INFO`,
    `init`,
    `LOG_FILE_SIZE_LIMIT=${config.app.logging.LOG_FILE_SIZE_LIMIT}`
  );

  log(__filename, `INFO`, `init`, `Configuration loaded successfully`);

  return {
    debug,
    info,
    warn,
    error,
    fatal,
    critical,
    log,
    onHttpRequestCompleted,
  };
};

module.exports = Logger;
