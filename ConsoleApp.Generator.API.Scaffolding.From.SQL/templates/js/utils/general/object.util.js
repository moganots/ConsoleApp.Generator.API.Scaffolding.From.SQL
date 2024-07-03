/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : ObjectUtil extension module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates a new ObjectUtil extension module
 */
module.exports = () => {
  const { v4 } = require(`uuid`);
  const isSet = (value) => {
    return !(
      value === null ||
      value === undefined ||
      (value && Object.keys(value).length === 0)
    );
  };
  const notSet = (value) => {
    return !isSet(value);
  };
  const hasNulls = (items = []) => {
    return items?.filter((item) => !isSet(item))?.length !== 0;
  };
  const appendLeadingZero = (number) => {
    return number <= 9 ? `0${number}` : number;
  };
  const padLeft = (value, padding) => {
    return value?.toString().padEnd(padding.length, padding);
  };
  const padRight = (value, padding) => {
    return value?.toString().padStart(padding.length, padding);
  };
  const ifJoin = (delimiter, ...args) => {
    const filter = args?.filter(
      (arg) => arg && !(String(arg).trim().length === 0)
    );
    switch (filter?.length) {
      case 0:
        return null;
      case 1:
        return filter[0];
      default:
        return filter?.join(delimiter);
    }
  };
  const elseIfNull = (value, option) => {
    return value || option;
  };
  const elseIfNullWithTrim = (value, option) => {
    return String(elseIfNull(value, option))?.trim();
  };
  const firstElement = (arr = []) => {
    return (arr || [])[0];
  };
  const lastElement = (arr = []) => {
    return (arr || [])[(arr || []).length - 1];
  };
  const elementAt = (arr = [], index = -1) => {
    return (arr || [])[index];
  };
  const getLastElement = (valueToSplit, delimiter) => {
    var items = valueToSplit && delimiter ? valueToSplit.split(delimiter) : [];
    return items[items.length - 1];
  };
  const ifMapGetter = (objectMap, key) => {
    return objectMap && String(key)?.length !== 0 ? objectMap[key] : null;
  };
  const getUuid = () => {
    return v4();
  };
  const hasData = (arg) => {
    return isSet(arg) || (Array.isArray(arg) && Array.from(arg)?.length !== 0);
  };
  const hasNoData = (arg) => {
    return !hasData(arg);
  }
  const ifThen = (...args) => {
    return args?.find(arg => isSet(arg));
  };
  const ifNaN = (value) => {
    return parseInt(value) === NaN || notSet(value) ? 0 : parseInt(value);
  };
  return {
    isSet,
    notSet,
    hasNulls,
    appendLeadingZero,
    padLeft,
    padRight,
    ifJoin,
    elseIfNull,
    elseIfNullWithTrim,
    firstElement,
    lastElement,
    getLastElement,
    elementAt,
    ifMapGetter,
    getUuid,
    hasData,
    hasNoData,
    ifThen,
    ifNaN,
  };
};
