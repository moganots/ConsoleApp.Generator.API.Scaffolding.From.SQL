/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Database extenstion utililty class
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the DBUtil class
 * @returns DBUtil
 */
module.exports = () => {
  const { yyyymmddhmsWithDashSeparator } = require(`../general/date.util`)();
  const { setProtocol } = require(`../general/http.util`)();
  const { getFileNameWithoutExtension } = require(`../general/io.util`)();
  const { ifJoin, getUuid } = require(`../general/object.util`)();
  const { isEmpty, capitalizeFirstLetter } =
    require(`../general/string.util`)();
  const setDBURI = (
    protocol,
    hostname,
    port,
    username = null,
    password = null
  ) => {
    if (isEmpty(protocol)) {
      throw new Error(`Database configuration [DB_PROTOCOL] not provided`);
    }
    if (isEmpty(hostname)) {
      throw new Error(`Database configuration [DB_HOST] not provided`);
    }
    if (isEmpty(port)) {
      throw new Error(`Database configuration [DB_PORT] not provided`);
    }
    return ifJoin(
      `/`,
      setProtocol(protocol),
      ifJoin(
        `@`,
        addCredentials(username, password),
        ifJoin(`:`, hostname, port)
      )
    );
  };
  const addCredentials = (username, password) => {
    return ifJoin(
      `:`,
      encodeURIComponent(username),
      encodeURIComponent(password)
    );
  };
  const setDBConnectionString = (
    protocol,
    hostname,
    port,
    databaseName,
    username = null,
    password = null
  ) => {
    if (isEmpty(databaseName)) {
      throw new Error(`Database configuration [DB_NAME] not provided`);
    }
    return ifJoin(
      `/`,
      setDBURI(protocol, hostname, port, username, password),
      databaseName
    );
  };
  const getSchemaNameFromFile = (path) => {
    return getFileNameWithoutExtension(path)
      ?.split(/[-:_.]/)
      ?.map((element) => capitalizeFirstLetter(element))
      ?.join(` `);
  };
  const getTableName = (schemaName, tableName) => {
    return `${ifJoin(`.`, schemaName, tableName)}`;
  };
  const getDtoColumns = (dto) => {
    return Object.keys(dto?.options?.columns || dto)?.map(column => column.trim());
  };
  const getDtoColumnsExcluding = (dto, ...args) => {
    return getDtoColumns(dto)?.filter((column) => !args?.includes(column));
  };
  const getDtoSelectColumnNames = (dto) => {
    return getDtoColumns(dto)?.join(`, `).trim();
  };
  const getDtoInsertColumnNames = (dto) => {
    return getDtoColumns(dto)?.join(`, `).trim();
  };
  const getDtoInsertColumnValues = (uid, dto, entity) => {
    return getDtoColumns(dto)
      ?.map((column) => setValue(column, uid, entity))
      ?.join(`, `);
  };
  /**
   * Sets the column value
   * @param {*} column the column name
   * @param {*} uid the ID of the user
   * @param {*} entity the entity object instance
   * @returns
   */
  const setValue = (column, uid, entity) => {
    if (column === `id`) return `'${getUuid()}'`;
    if ([`created_by`, `updated_by`].includes(column))
      return uid ? `'${uid}'` : `null`;
    if ([`created_timestamp`, `updated_timestamp`].includes(column))
      return `'${yyyymmddhmsWithDashSeparator()}'`;
    return `'${entity[column]}'`;
  };
  /**
   * Appends WHERE and predicate (filter option(s)) to the query
   * @param {*} query the query to append WHERE to
   * @param {*} predicate the predicate (filter option(s)) to append
   * @returns any
   */
  const appendWhere = (query, predicate) => {
    return query && predicate
      ? `${query} WHERE (${Object.keys(predicate)
          .map((key) => `${key}='${predicate[key]}'`)
          .join(` AND `)})`
      : query;
  };
  /**
   * Appends the RETURNING cause to the query
   * @param {*} query the query to append WHERE to
   * @param {*} args the column(s) to be appended
   * @returns any
   */
  const appendReturning = (query, ...columns) => {
    return `${query} returning ${columns?.join(`, `) || `*`};`
  }
  return {
    setDBURI,
    setDBConnectionString,
    getSchemaNameFromFile,
    getTableName,
    getDtoColumns,
    getDtoColumnsExcluding,
    getDtoSelectColumnNames,
    getDtoInsertColumnNames,
    getDtoInsertColumnValues,
    setValue,
    appendWhere,
    appendReturning
  };
};
