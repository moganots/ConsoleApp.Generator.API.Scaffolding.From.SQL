/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : General funtion(s) extenstion utililty class
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates a new FunctionsUtil instance
 */
const FunctionsUtil = () => {
  const isNotNaN = (value = NaN) => {
    return !(value === NaN || value <= 0);
  };
  const hasValues = (values) => {
    return values && Array.isArray(values) && values.length != 0;
  };
  const onlyUniqueValues = (value, index, self) => {
    return self.indexOf(value) === index;
  };
  const isValidEmail = (email) => {
    var re =
      /^(([^<>()[\]\\.,;:\s@\`]+(\.[^<>()[\]\\.,;:\s@\`]+)*)|(\`.+\`))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
  };
  const getBytes = (stringValue) => {
    var bytes = [];
    for (var i = 0; i < stringValue.length; ++i) {
      bytes.push(stringValue.charCodeAt(i));
    }
    return bytes;
  };
  const bytesToString = (byteArray) => {
    return String.fromCharCode.apply(String, byteArray);
  };
  const toBase64 = (valueToEncode) => {
    return Buffer.from(valueToEncode).toString(`base64`);
  };
  const getBaseUrl = (protocol, hostIpName, portNumber = null) => {
    if (protocol && hostIpName && portNumber)
      return `${protocol}://${hostIpName}:${portNumber}`;
    if (protocol && hostIpName) return `${protocol}://${hostIpName}`;
    return null;
  };
  const useContentType = (type) => {
    return `application/${type}`.toLocaleLowerCase();
  };
  const formattedAction = (action) => {
    switch (action.toLocaleLowerCase()) {
      case `create`:
        return `created`;
      case `add`:
        return `added`;
      case `edit`:
        return `edited`;
      case `delete`:
        return `deleted`;
      case `update`:
        return `updated`;
      case `change`:
        return `changed`;
      case `find`:
        return `finds`;
      case `fetch`:
        return `fetched`;
      case `login`:
        return `logged in`;
      case `logout`:
        return `logged out`;
      case `validate`:
        return `validated`;
      case `authenticate`:
        return `authenticated`;
      default:
        return action;
    }
  };
  const isBetween = (value, start = 0, bound = 0) => {
    return value && value >= start && value <= bound;
  };
  const toNumber = (value) => {
    return Number.parseFloat(value);
  };
  const ifHasData = (array = []) => {
    return array && array.length != 0;
  };
  const flattenArray = (array = []) => {
    return [].concat.apply([], array);
  };
  const formatBytes = (bytes, decimals = 2) => {
    if (isNaN(bytes) || bytes <= 0) return `0 bytes`;
    const k = 1024;
    const dm = decimals < 0 ? 0 : decimals;
    const sizes = [`bytes`, `KB`, `MB`, `GB`, `TB`, `PB`, `EB`, `ZB`, `YB`];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ` ` + sizes[i];
  };
  return {
    isNotNaN,
    hasValues,
    onlyUniqueValues,
    isValidEmail,
    getBytes,
    bytesToString,
    toBase64,
    getBaseUrl,
    useContentType,
    formattedAction,
    isBetween,
    toNumber,
    ifHasData,
    flattenArray,
    formatBytes,
  };
};

module.exports = FunctionsUtil;
