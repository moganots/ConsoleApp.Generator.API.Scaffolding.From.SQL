/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Authentication controller module
|------------------------------------------------------------------------------------------------------------------
*/

/**
 * Creates an instance of the Authentication Controller
 * @param {*} config an instance of the configuration module
 * @param {*} logger an instance of the logger module
 * @param {*} dbContext an instance of the DBContext module
 * @returns AuthenticationController
 */
module.exports = function (config, logger, dbContext) {
  const { isSet, notSet, ifThen, hasNoData, firstElement, ifNaN } =
    require(`./../../utils/general/object.util`)();
  if (notSet(logger)) {
    throw new Error(
      `AuthenticationController - [logger] instance has not been provided or set`
    );
  }
  if (notSet(dbContext)) {
    throw new Error(
      `AuthenticationController - [DBContext] instance has not been provided or set`
    );
  }
  const { getRequestQueryParameters } =
    require(`./../../utils/general/http.util`)();
  const { isEmpty } = require(`./../../utils/general/string.util`)();
  const { loginRetryLimit } =
    require(`./../../constants/authentication/constants`).authentication;
  const { hashValue, isValid, jwtCreateToken, jwtVerifyToken } =
    require(`./../../middleware/authentication/middleware`)();
  const dtoUser = require(`./../../models/entities/model.entity.factory`)(
    config,
    logger,
    `entity.user`
  ).getModelEntity();
  const register = (request, response) => {};
  const login = (request, response) => {
    try {
      const user =
        require(`./../../utils/general/http.util`)().getRequestUserCredentials(
          request
        );
      if (isEmpty(user.loginId)) {
        return log(
          request,
          response,
          `[username] or [login_id] was not provided`,
          null,
          null
        );
      }
      if (isEmpty(user.password)) {
        return log(
          request,
          response,
          `[password] was not provided`,
          null,
          null
        );
      }
      dbContext.getBy(
        user.loginId,
        dtoUser,
        { login_id: user.loginId },
        async (error, data, message) => {
          if (error || hasNoData(data)) {
            return log(
              request,
              response,
              error ||
                `[username] or [login_id] [${user.loginId}] does not exist`,
              null,
              null
            );
          }
          const userExist = firstElement(data) || data;
          var {
            id,
            profile_id,
            login_id,
            password,
            user_category_id,
            is_logged_in,
            login_attempt_count,
            is_locked,
            is_active,
          } = userExist;
          login_attempt_count = ifNaN(login_attempt_count) + 1;
          /* if (Boolean(is_logged_in)) {
            return log(
              request,
              response,
              `[username] or [login_id] [${user.loginId}] is already logged in.`,
              null,
              null
            );
          } */
          if (Boolean(is_locked)) {
            return log(
              request,
              response,
              `[username] or [login_id] [${user.loginId}] is currently locked. Please contact system administrator.`,
              null,
              null
            );
          }
          if (!Boolean(is_active)) {
            return log(
              request,
              response,
              `[username] or [login_id] [${user.loginId}] is currently not active. Please activate [username] or [login_id] [${user.loginId}] or contact system administrator.`,
              null,
              null
            );
          }
          const { yyyymmddhmsmsWithDashSeparator } =
            require(`./../../utils/general/date.util`)();
          if (await isValid(user.password, password)) {
            const date_last_logged_in = yyyymmddhmsmsWithDashSeparator();
            const user_token = jwtCreateToken({
              id: id,
              profile_id: profile_id,
              login_id: login_id,
              user_category_id: user_category_id,
              app_id: `fla`,
              date_last_logged_in: yyyymmddhmsmsWithDashSeparator(),
            });
            dbContext.update(
              id,
              dtoUser,
              {
                id: id,
                login_attempt_count: 0,
                is_logged_in: 1,
                user_token: user_token,
                date_last_logged_in: date_last_logged_in,
              },
              (error, data, message) => {
                if (error) {
                  return log(request, response, error, null, null);
                } else {
                  response.setHeader(`Authorization`, `Bearer ${user_token}`);
                  response.setHeader(`Authentication`, `Basic ${user_token}`);
                  return log(
                    request,
                    response,
                    null,
                    {
                      user_id: id,
                      profile_id: profile_id,
                    },
                    `[username] or [login_id] [${user.loginId}] successfully logged in.`
                  );
                }
              }
            );
          } else if (login_attempt_count >= loginRetryLimit) {
            dbContext.update(
              id,
              dtoUser,
              {
                id: id,
                is_locked: 1,
                is_logged_in: 0,
                user_token: null,
              },
              async (error, data, message) => {
                if (error) {
                  return log(request, response, error, null, null);
                } else {
                  return log(
                    request,
                    response,
                    `[username] or [login_id] [${user.loginId}] has been locked. ${loginRetryLimit} login attempt(s) have been reached.`,
                    null,
                    null
                  );
                }
              }
            );
          } else {
            dbContext.update(
              id,
              dtoUser,
              {
                id: id,
                login_attempt_count: login_attempt_count,
                is_logged_in: 0,
                user_token: null,
              },
              async (error, data, message) => {
                if (error) {
                  return log(request, response, error, null, null);
                } else {
                  return log(
                    request,
                    response,
                    `Invalid [password] provided for [username] or [login_id] [${
                      user.loginId
                    }]. ${
                      loginRetryLimit - login_attempt_count
                    } login attempt(s) left.`,
                    null,
                    null
                  );
                }
              }
            );
          }
        }
      );
    } catch (exception) {
      return log(request, response, exception, null, null);
    }
  };
  const logout = (request, response) => {
    try {
      const { login_id, loginid, loginId, username } =
        getRequestQueryParameters(request) || {};
      const uid = ifThen(login_id, loginid, loginId, username);
      dbContext.getBy(
        null,
        dtoUser,
        { login_id: uid },
        (error, data, message) => {
          if (error || hasNoData(data)) {
            return log(
              request,
              response,
              error || `[username] or [login_id] [${uid}] does not exist`,
              null,
              null
            );
          } else {
            const { id } = firstElement(data) || data;
            dbContext.update(
              id,
              dtoUser,
              {
                id: id,
                login_attempt_count: 0,
                is_logged_in: 0,
                user_token: null,
              },
              (error, data, message) => {
                return log(
                  request,
                  response,
                  error ||
                    `[username] or [login_id] [${uid}] successfully logged out`,
                  null,
                  null
                );
              }
            );
          }
        }
      );
    } catch (exception) {
      return log(request, response, exception, null, null);
    }
  };
  const tokenVerify = (request, response) => {
    const { token, user_token } = getRequestQueryParameters(request) || {};
    dbContext.getBy(
      null,
      dtoUser,
      { user_token: token || user_token },
      (error, data, message) => {
        if (error || hasNoData(data)) {
          return log(
            request,
            response,
            error || `The specified token is invalid`
          );
        } else {
          return log(
            request,
            response,
            null,
            { is_token_valid: jwtVerifyToken(token || user_token) },
            `Token verified successfully`
          );
        }
      }
    );
  };
  const tokenCancel = (request, response) => {
    const { token, user_token } = getRequestQueryParameters(request) || {};
    dbContext.getBy(
      null,
      dtoUser,
      { user_token: token || user_token },
      (error, data, message) => {
        if (error || hasNoData(data)) {
          return log(
            request,
            response,
            error || `The specified token is invalid`
          );
        } else {
          const { id } = firstElement(data) || data;
          dbContext.update(
            id,
            dtoUser,
            {
              id: id,
              login_attempt_count: 0,
              is_logged_in: 0,
              user_token: null,
            },
            (error, data, message) => {
              return log(
                request,
                response,
                error || `User token successfully cancelled.`,
                null,
                null
              );
            }
          );
        }
      }
    );
  };
  const log = (request, response, error, data, message) => {
    return logger.onHttpRequestCompleted(__filename, request, response, {
      data: data,
      hasError: isSet(error),
      message: ifThen(error?.message, error, message),
      messageDetail: ifThen(error?.stack, error?.stackTrace),
    });
  };
  return {
    register,
    login,
    logout,
    tokenVerify,
    tokenCancel,
  };
};
