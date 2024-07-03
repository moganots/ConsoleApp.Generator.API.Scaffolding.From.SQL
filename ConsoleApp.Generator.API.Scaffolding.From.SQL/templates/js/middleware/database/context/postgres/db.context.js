/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : PostgreSql database context extension utility module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the PostgreSql DBContext module
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logging module
 * @returns PostgreSqlDBContext
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
      `PostgreSql DBContext - [config] instance has not been provided or set`
    );
  }
  if (notSet(logger)) {
    throw new Error(
      `PostgreSql DBContext - [logger] instance has not been provided or set`
    );
  }
  /*
    |--------------------------------------------------------------------------------------------------------------------------------------------
    | Dependency(ies)
    |--------------------------------------------------------------------------------------------------------------------------------------------
    */
  const {
    getPostgresInsertQuery,
    getPostgresSelectQuery,
    getPostgresUpdateQuery,
    getPostgresDeleteQuery,
  } = require(`./../../../../utils/db/db.postgres.util`)();
  const { connectDB, disconnectDB } =
    require(`./../../connection/postgres/db.connection`)(config, logger);

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
      callback(
        null,
        (await execQuery(getPostgresInsertQuery(uid, dto, entity))).data,
        `Query executed successfully`
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
      callback(
        null,
        (await execQuery(getPostgresSelectQuery(uid, dto, null))).data,
        `Query executed successfully`
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
      callback(
        null,
        (await execQuery(getPostgresSelectQuery(uid, dto, { id: entityId })))
          .data,
        `Query executed successfully`
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
      callback(
        null,
        (await execQuery(getPostgresSelectQuery(uid, dto, parameters))).data,
        `Query executed successfully`
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
      callback(
        null,
        (await execQuery(getPostgresUpdateQuery(uid, dto, entity))).data,
        `Query executed successfully`
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
      callback(
        null,
        (await execQuery(getPostgresDeleteQuery(uid, dto, { id: entityId })))
          .data,
        `Query executed successfully`
      );
    } catch (exception) {
      callback(exception);
    } finally {
      global.gc();
    }
  };
  /**
   * Executes the query
   * @param {*} query the query to be executed
   * @returns
   */
  const execQuery = async (query) => {
    return await connectDB().then(async (connection) => {
      const result = await connection.query(query);
      await connection.end();
      logger.info(__filename, `execQuery`, JSON.stringify(query));
      return { rowCount: result?.rowCount, data: result?.rows };
    });
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
