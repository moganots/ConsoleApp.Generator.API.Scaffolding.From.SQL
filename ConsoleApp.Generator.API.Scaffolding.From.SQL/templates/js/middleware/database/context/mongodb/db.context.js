/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : MongoDB database context extension utility module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the MongoDB DBContext module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns MongoDBContext
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
        `MongoDB DBContext - [config] instance has not been provided or set`
      );
    }
    if (notSet(logger)) {
      throw new Error(
        `MongoDB DBContext - [logger] instance has not been provided or set`
      );
    }
  
    /*
      |--------------------------------------------------------------------------------------------------------------------------------------------
      | Function(s)
      |--------------------------------------------------------------------------------------------------------------------------------------------
      */
    /**
     * Creates a new Entity
     * @param {*} uid the request User ID
     * @param {*} schemaName the schema name of the Entity (model)
     * @param {*} entityName the name of the Entity (model)
     * @param {*} entity the new Entity (model) to be created
     * @param {*} callback an instance of the callback method to be executed upon completion
     */
    const create = (uid, schemaName, entityName, entity, callback) => {
      try {
      } catch (exception) {
        callback(exception);
      } finally {
        global.gc();
      }
    };
    /**
     * Gets all Entities
     * @param {*} uid the request User ID
     * @param {*} schemaName the schema name of the Entity (model)
     * @param {*} entityName the name of the Entity (model)
     * @param {*} callback an instance of the callback method to be executed upon completion
     */
    const getAll = (uid, schemaName, entityName, callback) => {
      try {
      } catch (exception) {
        callback(exception);
      } finally {
      }
    };
    /**
     * Gets the Entity(ies) (model(s)) with the specified Entity ID
     * @param {*} uid the request User ID
     * @param {*} schemaName the schema name of the Entity (model)
     * @param {*} entityName the name of the Entity (model)
     * @param {*} entityId the ID to filter by
     * @param {*} callback an instance of the callback method to be executed upon completion
     */
    const getById = (uid, schemaName, entityName, entityId, callback) => {
      try {
      } catch (exception) {
        callback(exception);
      } finally {
        global.gc();
      }
    };
    /**
     * Gets the Entity(ies) (model(s)) using the specified filter (parameters)
     * @param {*} uid the request User ID
     * @param {*} schemaName the schema name of the Entity (model)
     * @param {*} entityName the name of the Entity (model)
     * @param {*} parameters the filter (parameters) to be applied
     * @param {*} callback an instance of the callback method to be executed upon completion
     */
    const getBy = (uid, schemaName, entityName, parameters, callback) => {
      try {
      } catch (exception) {
        callback(exception);
      } finally {
        global.gc();
      }
    };
    /**
     * Updates an Entity (model)
     * @param {*} uid the request User ID
     * @param {*} schemaName the schema name of the Entity (model)
     * @param {*} entityName the name of the Entity (model)
     * @param {*} entity the Entity (model) containing the update(s)
     * @param {*} callback an instance of the callback method to be executed upon completion
     */
    const update = (uid, schemaName, entityName, entity, callback) => {
      try {
      } catch (exception) {
        callback(exception);
      } finally {
        global.gc();
      }
    };
    /**
     * Deletes the Entity (model) with the specified ID
     * @param {*} uid the request User ID
     * @param {*} schemaName the schema name of the Entity (model)
     * @param {*} entityName the name of the Entity (model)
     * @param {*} entityId the ID of the Entity (model) to be deleted
     * @param {*} callback an instance of the callback method to be executed upon completion
     */
    const _delete = (uid, schemaName, entityName, entityId, callback) => {
      try {
      } catch (exception) {
        callback(exception);
      } finally {
        global.gc();
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
  