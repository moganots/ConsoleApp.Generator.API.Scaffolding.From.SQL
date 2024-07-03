/*
|--------------------------------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : MS SQL database connection utilities module
|--------------------------------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the MS SQL DBConnection module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns MsSqlDBConnection
 */
module.exports = (config, logger) => {
  require(`expose-gc`);
  const { hasValues } = require(`./../../../../utils/general/functions.util`)();
  const { notSet } = require(`./../../../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `MsSql DBConnection - [config] instance has not been provided or set`
    );
  }
  if (notSet(config.app)) {
    throw new Error(
      `MsSql DBConnection - [config.app] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database)) {
    throw new Error(
      `MsSql DBConnection - [config.app.database] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database.MSSQL)) {
    throw new Error(
      `MsSql DBConnection - [config.app.database.MSSQL] instance has not been provided or set`
    );
  }
  if (notSet(config.app.database.MSSQL.connectionString)) {
    throw new Error(
      `MsSql DBConnection - [config.app.database.MSSQL.connectionString] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `MsSql DBConnection - [logger] instance has not been provided or set`
    );
  }
  const Request = require(`tedious`).Request;
  const { getConnection, closeConnection, getTediousMsSqlDataType } =
    require(`./../../../../utils/db/db.tedious.util`)();
  const {
    addRequestParameters,
    getParameterNamesWithAt,
    sendDbResponse,
    initRequestBuildRow,
    initRequestDoneInProcDataset,
  } = require(`./middleware`)();
  const executeStoredProcedure = (
    schemaName,
    storedProcedureName,
    parameters,
    isMultiSet,
    callback
  ) => {
    try {
      const connection = getConnection(
        config.app.database.MSSQL.connectionString
      );
      connection.connect((error) => {
        if (error) {
          callback(error);
        } else {
          const command = `[${
            schemaName || schemaName
          }].[${storedProcedureName}]`;
          let data = [];
          let dataset = [];
          getDatabaseObjectParameters(
            schemaName,
            storedProcedureName,
            false,
            (error, objParameters) => {
              let request = new Request(command, (error, rowCount) => {
                sendDbResponse(command, error, rowCount, data, callback);
                closeConnection(
                  connection,
                  request,
                  config.app.database.MSSQL.connectionString
                );
              });
              if (!error && objParameters && hasValues(objParameters)) {
                objParameters.forEach((objParameter) => {
                  const parameterValue =
                    parameters[objParameter.ParameterName] || null;
                  request.addParameter(
                    objParameter.ParameterName,
                    getTediousMsSqlDataType(objParameter.DataType),
                    parameterValue
                  );
                });
              }
              initRequestBuildRow(request, data);
              ({ dataset, data } = initRequestDoneInProcDataset(
                request,
                dataset,
                data,
                isMultiSet
              ));
              logger.info(
                __filename,
                `executeStoredProcedure`,
                [
                  `Preparing to execute command`,
                  command,
                  `Parameter(s):\r\n${request?.parameters
                    ?.map(
                      (parameter) => `${parameter.name}: ${parameter.value}`
                    )
                    .join(`\r\n`)}`,
                ].join(`\r\n`)
              );
              connection.callProcedure(request);
            }
          );
        }
      });
    } catch (exception) {
      logger.fatal(
        __filename,
        `executeStoredProcedure`,
        exception,
        exception.stack
      );
    } finally {
      global.gc();
    }
  };
  const executeUserDefinedFunction = (
    schemaName,
    functionName,
    parameters,
    isMultiSet,
    callback
  ) => {
    try {
      const connection = getConnection(
        config.app.database.MSSQL.connectionString
      );
      connection.connect((error) => {
        if (error) {
          callback(error);
        } else {
          const parameterNames = getParameterNamesWithAt(parameters).join(`,`);
          const command = `SELECT * FROM [${
            schemaName || schemaName
          }].[${functionName}](${parameterNames})`;
          let data = [];
          let dataset = [];
          let request = new Request(command, (error, rowCount) => {
            sendDbResponse(command, error, rowCount, data, callback);
            closeConnection(
              connection,
              request,
              config.app.database.MSSQL.connectionString
            );
          });
          addRequestParameters(request, parameters);
          initRequestBuildRow(request, data);
          ({ dataset, data } = initRequestDoneInProcDataset(
            request,
            dataset,
            data,
            isMultiSet
          ));
          logger.info(
            __filename,
            `executeUserDefinedFunction`,
            [`Preparing to execute command`, command].join(`\r\n`)
          );
          connection.execSql(request);
        }
      });
    } catch (error) {
      callback(error);
    } finally {
      global.gc();
    }
  };
  const getDatabaseObjectParameters = (
    schemaName,
    objectName,
    isMultiSet,
    callback
  ) => {
    try {
      const parameters = [
        { SchemaName: schemaName },
        { ObjectName: objectName },
      ];
      executeUserDefinedFunction(
        schemaName,
        `GetDatabaseObjectParameters`,
        parameters,
        isMultiSet,
        callback
      );
    } catch (error) {
      callback(error);
    } finally {
      global.gc();
    }
  };
  return {
    executeStoredProcedure,
    executeUserDefinedFunction,
  };
};
