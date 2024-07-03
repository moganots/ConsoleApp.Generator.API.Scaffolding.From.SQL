/*
|--------------------------------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : MS SQL database context utilities class
|--------------------------------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the MS SQL DBContext module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns MsSqlDBContext
 */
module.exports = (config, logger) => {
  /*
    |--------------------------------------------------------------------------------------------------------------------------------------------
    | Dependency(ies)
    |--------------------------------------------------------------------------------------------------------------------------------------------
    */
  require(`expose-gc`);
  const { notSet } = require(`./../../../../utils/general/object.util`)();
  if (notSet(config)) {
    throw new Error(
      `MsSql DBContext - [config] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `MsSql DBContext - [logger] instance has not been provided or set`
    );
  }
  /*
    |--------------------------------------------------------------------------------------------------------------------------------------------
    | Dependency(ies)
    |--------------------------------------------------------------------------------------------------------------------------------------------
    */
  const { yyyymmddhmsmsWithDashSeparator } =
    require(`./../../../../utils/general/date.util`)();
  const { toLocaleLowerCaseWithTrim } =
    require(`./../../../../utils/general/string.util`)();
  const { executeStoredProcedure } =
    require(`./../../connection/mssql/db.connection`)(config, logger);

  /*
    |--------------------------------------------------------------------------------------------------------------------------------------------
    | Function(s)
    |--------------------------------------------------------------------------------------------------------------------------------------------
    */
  /**
   * Creates a new Entity
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} entity the new Entity (model) to be created
   * @param {*} callback an instance of the callback method to be executed upon completion
   */
  const create = async (uid, dto, entity, callback) => {
    try {
      entity.created_by = uid;
      entity.created_timestamp = yyyymmddhmsmsWithDashSeparator();
      executeStoredProcedure(
        dto?.schemaName(),
        getStoredProcedureName(`create`, dto?.getName()),
        entity,
        false,
        callback
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Gets all Entities
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} callback an instance of the callback method to be executed upon completion
   */
  const getAll = async (uid, dto, callback) => {
    try {
      executeStoredProcedure(
        dto?.schemaName(),
        getStoredProcedureName(`fetch`, dto?.getName()),
        {},
        false,
        callback
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Gets the Entity(ies) (model(s)) with the specified Entity ID
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} entityId the ID to filter by
   * @param {*} callback an instance of the callback method to be executed upon completion
   */
  const getById = async (uid, dto, entityId, callback) => {
    try {
      executeStoredProcedure(
        dto?.schemaName(),
        getStoredProcedureName(`fetch`, dto?.getName()),
        { _id: entityId },
        false,
        callback
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Gets the Entity(ies) (model(s)) using the specified filter (parameters)
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} parameters the filter (parameters) to be applied
   * @param {*} callback an instance of the callback method to be executed upon completion
   */
  const getBy = async (uid, dto, parameters, callback) => {
    try {
      executeStoredProcedure(
        dto?.schemaName(),
        getStoredProcedureName(`fetch`, dto?.getName()),
        parameters,
        false,
        callback
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Updates an Entity (model)
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} entity the Entity (model) containing the update(s)
   * @param {*} callback an instance of the callback method to be executed upon completion
   */
  const update = async (uid, dto, entity, callback) => {
    try {
      entity.modified_by = uid;
      entity.modified_timestamp = yyyymmddhmsmsWithDashSeparator();
      executeStoredProcedure(
        dto?.schemaName(),
        getStoredProcedureName(`update`, dto?.getName()),
        entity,
        false,
        callback
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Deletes the Entity (model) with the specified ID
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} entityId the ID of the Entity (model) to be deleted
   * @param {*} callback an instance of the callback method to be executed upon completion
   */
  const _delete = async (uid, dto, entityId, callback) => {
    try {
      executeStoredProcedure(
        dto?.schemaName(),
        getStoredProcedureName(`delete`, dto?.getName()),
        { _id: entityId, modified_by: uid },
        false,
        callback
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Gets the fully qualified stored procedure to be executed for the specified action
   * @param {*} action the action to get the name of the stored procedure for
   * @param {*} entityName the name of the Entity (model) to get the stored procedure for
   * @returns String
   */
  const getStoredProcedureName = (action, entityName) => {
    switch (toLocaleLowerCaseWithTrim(action)) {
      case `create`:
      case `insert`:
      case `new`:
        return `spCreateOrInsert_${entityName}`;
      case `fetch`:
      case `get`:
      case `getAll`:
      case `getById`:
      case `getBy`:
      case `select`:
        return `spFetchOrSelect_${entityName}`;
      case `change`:
      case `edit`:
      case `update`:
        return `spEditOrUpdate_${entityName}`;
      case `archive`:
      case `delete`:
      case `remove`:
        return `spArchiveOrDelete_${entityName}`;
    }
  };

  return {
    create,
    getAll,
    getById,
    getBy,
    update,
    delete: _delete,
  };
};
