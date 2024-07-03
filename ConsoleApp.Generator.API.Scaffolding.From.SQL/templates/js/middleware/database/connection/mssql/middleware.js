/*
|--------------------------------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : MS SQL database connection middlware module
|--------------------------------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the MS SQL database connection middleware module
 * @returns MsSqlDBConnectionMiddleware
 */
module.exports = () => {
  require(`expose-gc`);
  const { getJSMsSqlDataType } =
    require(`./../../../../utils/db/db.tedious.util`)();
  const { getInfoOnFetchSuccessfulMessage, getInfoOnFetchNoDataMessage } =
    require(`../../../../utils/db/db.common.messages`)();
  const addRequestParameters = (request, parameters = []) => {
    if (request && hasValues(parameters)) {
      parameters.forEach((parameter) => {
        const parameterName = Object.getOwnPropertyNames(parameter)[0];
        const parameterValue = parameter[parameterName] || null;
        const parameterType =
          parameterValue instanceof Date ? `date` : typeof parameterValue;
        const parameterMsSqlType = getJSMsSqlDataType(parameterType);
        request.addParameter(parameterName, parameterMsSqlType, parameterValue);
      });
    }
  };
  const getParameterNamesWithoutAt = (parameters = []) => {
    return parameters.map((parameter) => Object.getOwnPropertyNames(parameter));
  };
  const getParameterNamesWithAt = (parameters = []) => {
    return getParameterNamesWithoutAt(parameters).map(
      (parameter) => `@${parameter}`
    );
  };
  const sendDbResponse = (command, error, rowCount, data, callback) => {
    try {
      if (callback) {
        const numberOfRecords = hasValues(data) ? data.length : rowCount;
        let message = error
          ? error.message
          : numberOfRecords > 0
          ? getInfoOnFetchSuccessfulMessage(command, "row", numberOfRecords)
          : getInfoOnFetchNoDataMessage(command, "row");
        callback(error, data, message);
      } else {
        throw `DBContext: failed to execute callback`;
      }
    } catch (error) {
      callback(error);
    } finally {
      global.gc();
    }
  };
  const initRequestBuildRow = (request, data = []) => {
    if (request) {
      request.on(`row`, (columns) => {
        let row = {};
        columns.forEach((column) => {
          row[column.metadata.colName] = column.value;
        });
        data.push(row);
      });
    }
  };
  const initRequestDoneInProcDataset = (request, dataset, data, isMultiSet) => {
    request.on(`doneInProc`, function () {
      if (isMultiSet === true) {
        dataset.push(data);
        data = [];
      } else {
        dataset = data;
      }
    });
    return { dataset, data };
  };
  return {
    addRequestParameters,
    getParameterNamesWithoutAt,
    getParameterNamesWithAt,
    sendDbResponse,
    initRequestBuildRow,
    initRequestDoneInProcDataset,
  };
};
