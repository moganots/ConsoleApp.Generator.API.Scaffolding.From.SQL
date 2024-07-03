/*
|--------------------------------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : PostgreSql database connection utilities class
|--------------------------------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the PostgreSql DBConnection  module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns PostgreSqlDBConnection
 */
module.exports = (config, logger) => {
  require(`expose-gc`);
  const { notSet } = require(`./../../../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `PostgreSql DBConnection - [config] instance has not been provided or set`
    );
  }
  if (notSet(config.app)) {
    throw new Error(
      `PostgreSql DBConnection - [config.app] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database)) {
    throw new Error(
      `PostgreSql DBConnection - [config.app.database] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database.POSTGRES)) {
    throw new Error(
      `PostgreSql DBConnection - [config.app.database.POSTGRES] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database.POSTGRES.connectionString)) {
    throw new Error(
      `PostgreSql DBConnection - [config.app.database.POSTGRES.connectionString] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `PostgreSql DBConnection - [logger] instance has not been provided or set`
    );
  }
  const { Pool } = require(`pg`);
  let connection;
  const connectDB = async () => {
    try {
      connection = new Pool(config.app.database.POSTGRES.connectionString);
    } catch (exception) {
      throw new Error(exception);
    } finally {
      global.gc();
    }
    return connection;
  };
  const disconnectDB = () => {
    if (connection) {
      connection.end();
    }
  };
  return {
    connectDB,
    disconnectDB,
  };
};
