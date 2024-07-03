/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Authentication middleware module
|------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the authentication middleware module
 */
module.exports = () => {
  require(`expose-gc`);
  const { isSet } = require(`./../../utils/general/object.util`)();
  const bcrypt = require(`bcrypt`);
  const cryptoJs = require(`crypto-js`);
  const jwt = require(`jsonwebtoken`);
  const { secret, saltCount, expiresIn, ignoreExpiration } =
    require(`./../../constants/authentication/constants`).jwt;
  const encrypt = (value) => {
    return isSet(value) ? cryptoJs.AES.encrypt(value, secret).toString() : null;
  };
  const decrypt = (value) => {
    return isSet(value)
      ? cryptoJs.AES.decrypt(value, secret).toString(cryptoJs.enc.Utf8)
      : null;
  };
  const isValid = async (plain, encrypted) => {
    return await bcrypt.compare(String(plain), String(decrypt(encrypted)));
  };
  const hashValue = async (value) => {
    const salt = await bcrypt.genSalt(saltCount);
    return await bcrypt.hash(value, salt);
  };
  const jwtCreateToken = (value) => {
    return isSet(value)
      ? jwt.sign(value, secret, {
          expiresIn: expiresIn,
        })
      : null;
  };
  const jwtVerifyToken = (token) => {
    try {
      return isSet(token) && isSet(jwt.verify(token, secret));
    } catch {
      return false;
    } finally {
      global.gc();
    }
  };
  const jwtDecodeToken = (token) => {
    return jwtVerifyToken(token) ? jwt.verify(token, secret) : null;
  };
  return {
    encrypt,
    decrypt,
    isValid,
    hashValue,
    jwtCreateToken,
    jwtVerifyToken,
    jwtDecodeToken
  };
};
