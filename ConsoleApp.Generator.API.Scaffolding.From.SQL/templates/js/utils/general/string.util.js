/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : StringUtil extension module
|------------------------------------------------------------------------------------------------------------------
 */

const { isSet, notSet } = require(`./object.util`)();

/**
 * Creates an instance of the StringUtil extension module
 */
module.exports = () => {
  const isEmpty = (value) => {
    return notSet(value) || String(value).trim().length === 0;
  };
  const notEmpty = (value) => {
    return !isEmpty(value);
  };
  const hasEmptyString = (items = []) => {
    return items?.filter((item) => isEmpty(item))?.length !== 0;
  };
  const nullIfJoin = (items = [], delimeter = `.`) => {
    const fi = items?.filter((item) => isSet(item) && !isEmpty(item));
    return fi?.length > 1
      ? fi?.join(delimeter)
      : fi?.length === 1
      ? fi[0]
      : null;
  };
  const toLocaleLowerCase = (value) => {
    return String(value)?.toLocaleLowerCase();
  };
  const toLocaleLowerCaseWithTrim = (value) => {
    return toLocaleLowerCase(value)?.trim();
  };
  const toLocaleLowerCaseWithTrimEnd = (value) => {
    return toLocaleLowerCase(value)?.trimEnd();
  };
  const toLocaleLowerCaseWithTrimStart = (value) => {
    return toLocaleLowerCase(value)?.trimStart();
  };
  const capitalizeFirstLetter = (value) => {
    return [
      (String(value) || ``).charAt(0).toLocaleUpperCase(),
      (String(value) || ``).slice(1),
    ].join(``);
  };
  const stringFormat = (value, args) => {
    if (value && args && args.length > 0) {
      args.forEach((arg, index) => {
        value = String(value).replace(`{` + index + `}`, arg);
      });
      return String(value || ``);
    }
    return String(value || ``);
  };
  const splitString = (value, delimiter = ` `) => {
    return String(value).split(delimiter);
  };
  const splitCamelCase = (value) => {
    return (String(value) || ``).trim().replace(/([a-z])([A-Z])/g, `$1 $2`);
  };
  const splitCamelCaseAndSpecialCharacters = (value) => {
    return (String(value) || ``)
      .trim()
      .replace(/([a-z])([A-Z])([._-])/g, `$1 $2 $3`);
  };
  const splitCamelCaseAndCapitalizeFirstLetter = (value) => {
    return capitalizeFirstLetter(splitCamelCase(String(value) || ``));
  };
  return {
    isEmpty,
    notEmpty,
    hasEmptyString,
    nullIfJoin,
    toLocaleLowerCase,
    toLocaleLowerCaseWithTrim,
    toLocaleLowerCaseWithTrimEnd,
    toLocaleLowerCaseWithTrimStart,
    capitalizeFirstLetter,
    stringFormat,
    splitString,
    splitCamelCase,
    splitCamelCaseAndSpecialCharacters,
    splitCamelCaseAndCapitalizeFirstLetter,
  };
};
