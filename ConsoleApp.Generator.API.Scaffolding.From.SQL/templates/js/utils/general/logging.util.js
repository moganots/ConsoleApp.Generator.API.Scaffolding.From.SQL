/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Logging extenstion utililty class
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the LoggingUtil class
 * @param {*} applicationEnvName the application environment name
 * @param {*} logDirectory the fully qualified path for the log directory
 * @param {*} logFileFullPath the fully qualified path for the log file path
 * @returns
 */
module.exports = function LoggingUtil(
  applicationEnvName,
  logDirectory,
  logFileFullPath
) {
  const { appendFile } = require(`fs`);
  const { createDirectory } = require(`./io.util`)();
  const formatLogMessage = (time, logType, caller, methodName, message) => {
    return `[${time}] [${applicationEnvName
      .trim()
      .toLocaleUpperCase()}] [${logType.toLocaleUpperCase()}] ${caller}${
      methodName ? `/${methodName}()` : ``
    } : ${message}`;
  };
  const formatMessage = (message) => {
    return Array.isArray(message)
      ? message
          .filter((msg) => !(msg === null || msg === undefined || msg === ``))
          .map(
            (msg) =>
              `${
                msg.type
                  ? [msg.type, msg.message].join(`:`)
                  : msg.message || msg
              }`
          )
          .join(`\r\n`)
      : message.message || message;
  };
  const logToConsole = (logType, message) => {
    switch ((logType || `info`).trim().toLocaleLowerCase()) {
      case `debug`:
        console.debug(message);
        break;
      case `warn`:
        console.warn(message);
        break;
      case `error`:
      case `fatal`:
      case `critical`:
        console.error(message);
        break;
      default:
        console.info(message);
        break;
    }
  };
  const logToFile = (message) => {
    createDirectory(logDirectory);
    appendFile(logFileFullPath, message + `\n`, function (err) {
      if (err) {
        throw err;
      }
    });
  };

  return {
    formatLogMessage,
    formatMessage,
    logToConsole,
    logToFile,
  };
};
