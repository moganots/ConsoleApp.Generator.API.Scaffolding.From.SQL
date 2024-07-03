/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Authentication constants and enumerables utility
|------------------------------------------------------------------------------------------------------------------
 */

module.exports = {
  authentication: {
    loginRetryLimit: parseInt(process.env.AUTH_LOGIN_RETRY_LIMIT),
    useCheckIfUserLoggedIn: Boolean(process.env.AUTH_USE_CHECK_IF_USER_LOGGED_IN),
  },
  jwt: {
    secret: String(process.env.JWT_SECRET),
    saltCount: parseInt(process.env.JWT_SALT_COUNT),
    expiresIn: String(process.env.JWT_EXPIRES_IN),
    ignoreExpiration: Boolean(process.env.JWT_IGNORE_EXPIRATION),
  },
};
