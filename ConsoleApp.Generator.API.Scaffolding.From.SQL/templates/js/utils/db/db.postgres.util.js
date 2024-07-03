/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Postgres Database extenstion utililty class
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the Postgres DBUtil class
 * @returns DBUtil
 */
module.exports = () => {
  const {
    getTableName,
    getDtoInsertColumnNames,
    getDtoInsertColumnValues,
    getDtoSelectColumnNames,
    getDtoColumnsExcluding,
    appendWhere,
    setValue,
    appendReturning
  } = require(`./db.util`)();
  const { yyyymmddhmsWithDashSeparator } = require(`./../general/date.util`)();
  /**
   * Gets the Postgres CREATE query for the specified DTO and predicate
   * @param {*} uid the ID of the user
   * @param {*} dto the DTO to get the CREATE query for
   * @param {*} entity the instance of the entity to be created
   * @returns
   */
  const getPostgresInsertQuery = (uid, dto, entity) => {
    const tableName = getTableName(dto?.options?.schema, dto?.options?.name);
    const columnNames = getDtoInsertColumnNames(dto);
    const columnValues = getDtoInsertColumnValues(uid, dto, entity);
    return dto && entity
      ? `insert into ${tableName} (${columnNames}) values (${columnValues}) returning *;`
      : null;
  };
  /**
   * Gets the Postgres SELECT query for the specified DTO and predicate
   * @param {*} uid the ID of the user
   * @param {*} dto the DTO to get the SELECT query for
   * @param {*} predicate the predicate (filter option(s)) to be applied, if any
   * @returns
   */
  const getPostgresSelectQuery = (uid, dto, predicate) => {
    const tableName = getTableName(dto?.options?.schema, dto?.options?.name);
    const columnNames = getDtoSelectColumnNames(dto);
    return appendWhere(
      dto ? `SELECT ${columnNames || `*`} FROM ${tableName}` : null,
      predicate
    );
  };
  /**
   * Gets the Postgres UPDATE query for the specified DTO and predicate
   * @param {*} uid the request User ID
   * @param {*} dto the DTO object
   * @param {*} entity the Entity (model) containing the update(s)
   * @returns
   */
  const getPostgresUpdateQuery = (uid, dto, entity) => {
    const tableName = getTableName(dto?.options?.schema, dto?.options?.name);
    const updateColumns = getDtoColumnsExcluding(
      entity,
      `id`,
      `created_by`,
      `created_timestamp`
    );
    const updateColumnValues = getUpdateColumnValues(
      updateColumns,
      uid,
      entity
    );
    return appendReturning(appendWhere(
      dto ? `UPDATE ${tableName} SET ${updateColumnValues}, updated_by=${setValue(`updated_by`, uid, entity)}, updated_timestamp=${setValue(`updated_timestamp`, uid, entity)}` : null,
      { id: entity?.id }
    ), `*`);
  };
  const getUpdateColumnValues = (columnNames, uid, entity) => {
    return columnNames
      ?.map(
        (columnName) => `${columnName}=${setValue(columnName, uid, entity)}`
      )
      ?.join(`, `)
      .trim();
  };
  /**
   * Gets the Postgres DELETE query for the specified DTO and predicate
   * @param {*} uid the ID of the user
   * @param {*} dto the DTO to get the DELETE query for
   * @param {*} predicate the predicate (filter option(s)) to be applied, if any
   * @returns
   */
  const getPostgresDeleteQuery = (uid, dto, predicate) => {
    const tableName = getTableName(dto?.options?.schema, dto?.options?.name);
    return appendReturning(appendWhere(
      dto
        ? `UPDATE ${tableName} SET is_active=false, updated_by=${
            uid ? `'${uid}'` : `null`
          }, updated_timestamp='${yyyymmddhmsWithDashSeparator()}'`
        : null,
      predicate
    ), `*`);
  };
  return {
    getPostgresInsertQuery,
    getPostgresSelectQuery,
    getPostgresUpdateQuery,
    getPostgresDeleteQuery,
  };
};
