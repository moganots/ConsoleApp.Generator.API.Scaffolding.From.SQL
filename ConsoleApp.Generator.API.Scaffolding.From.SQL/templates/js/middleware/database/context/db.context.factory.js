/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : DBContextFactory module
|------------------------------------------------------------------------------------------------------------------
*/

/**
 * Creates an instance of the DBContextFactory module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns DBContextFactory
 */
module.exports = (config, logger) => {
  /*
    |--------------------------------------------------------------------------------------------------------------------------------------------
    | Dependency(ies)
    |--------------------------------------------------------------------------------------------------------------------------------------------
    */
  const { notSet } = require(`../../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `DBContextFactory - [config] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `DBContextFactory - [logger] instance has not been provided or set`
    );
  }
  /*
    |--------------------------------------------------------------------------------------------------------------------------------------------
    | Funtion(s)
    |--------------------------------------------------------------------------------------------------------------------------------------------
    */
  const getDBContext = () => {
    switch (config?.app?.database?.DB_CONTEXT) {
      case `MONGODB`:
        return require(`./mongodb/db.context`)(config, logger);
      case `POSTGRES`:
        return require(`./postgres/db.context`)(config, logger);
      case `MSSQL`:
      default:
        return require(`./mssql/db.context`)(config, logger);
    }
  };
  return {
    getDBContext,
  };
};
