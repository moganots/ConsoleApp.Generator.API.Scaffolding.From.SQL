/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Database common message(s) utility module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates a new DBCommonMessages instance
 */
const DBCommonMessages = () => {
  const getInfoOnConnectionMessage = (connectionString, providerName) => {
    return {
      type: `InfoOnConnection`,
      message: `Initialising ${providerName} database connection to:\r\n${addConnectionString(
        connectionString
      )}`,
    };
  };
  const getInfoOnFetchNoDataMessage = (command, modelType) => {
    return {
      type: `InfoFetchNoData`,
      message: `Command executed successfully. No ${modelType} found.\r\n${addCommand(
        command
      )}`,
    };
  };
  const getInfoOnFetchSuccessfulMessage = (
    command,
    modelType,
    rowCount = 0
  ) => {
    return {
      type: `InfoFetchWithData`,
      message: `Command executed successfully. ${rowCount} ${modelType}${
        rowCount > 1 ? "(s)" : ""
      } found\r\n${addCommand(command)}`,
    };
  };
  const getErrorOnConnectionMessage = (connectionString, providerName) => {
    return {
      type: `ErrorOnConnection`,
      message: `Failed to initialise ${providerName} database connection to:\r\n${addConnectionString(
        connectionString
      )}`,
    };
  };

  const addConnectionString = (connectionString) => {
    return connectionString
      ? `ConnectionString:\r\n${connectionString
          .split(`,`)
          .map((cs) => (cs || ``).trim())
          .join(`\r\n`)}`
      : `Not specified`;
  };
  const addCommand = (command) => {
    return `\r\nCommand: ${command || `Command was not specified`}`.trim();
  };
  return {
    getInfoOnConnectionMessage,
    getInfoOnFetchNoDataMessage,
    getInfoOnFetchSuccessfulMessage,
    getErrorOnConnectionMessage,
    addConnectionString,
    addCommand,
  };
};

module.exports = DBCommonMessages;
