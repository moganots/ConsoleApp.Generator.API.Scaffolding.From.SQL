/*
|--------------------------------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : MongoDB database connection utilities module
|--------------------------------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the MongoDB DBConnection module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns MongoDBConnection
 */
module.exports = (config, logger) => {
  const { notSet } = require(`./../../../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `MongoDB DBConnection - [config] instance has not been provided or set`
    );
  }
  if (notSet(config.app)) {
    throw new Error(
      `MongoDB DBConnection - [config.app] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database)) {
    throw new Error(
      `MongoDB DBConnection - [config.app.database] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database.MONGODB)) {
    throw new Error(
      `MongoDB DBConnection - [config.app.database.MONGODB] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database.MONGODB.connectionString)) {
    throw new Error(
      `MongoDB DBConnection - [config.app.database.MONGODB.connectionString] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `MongoDB DBConnection - [logger] instance has not been provided or set`
    );
  }
  const connectDB = () => {}
  const disconnectDB = () => {}
  return {
    connectDB,
    disconnectDB
  }
};
