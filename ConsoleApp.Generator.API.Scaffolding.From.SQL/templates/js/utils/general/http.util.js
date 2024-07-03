/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : HttpUtil extension module
|------------------------------------------------------------------------------------------------------------------
 */

const http = require(`http`);
const https = require(`https`);
const { isSet, notSet, ifJoin, lastElement, getLastElement, ifThen } =
  require(`./object.util`)();
const { notEmpty } = require(`./string.util`)();
const { toBase64, getBytes } = require(`./functions.util`)();
const uid_field_list = [
  `Login_Id`,
  `Login_id`,
  `login_Id`,
  `login_id`,
  `Login_Name`,
  `Login_name`,
  `login_name`,
  `loginId`,
  `loginid`,
  `LoginName`,
  `loginName`,
  `loginname`,
  `u_id`,
  `u_n`,
  `uid`,
  `un`,
  `User_Id`,
  `User_id`,
  `user_Id`,
  `user_id`,
  `User_Name`,
  `User_name`,
  `user_Name`,
  `user_name`,
  `UserId`,
  `Userid`,
  `userId`,
  `userid`,
  `UserName`,
  `Username`,
  `userName`,
  `username`,
];

const pwd_field_list = [
  `pwd`,
  `password`,
  `pass_word`,
  `Password`,
  `Pass_word`,
  `PassWord`,
  `Pass_Word`,
  `passWord`,
  `pass_Word`,
  `pw`,
  `p_w`,
];

/**
 * Creates an instance of the HttpUtil extension module
 */
module.exports = () => {
  const setUri = (
    protocol,
    hostname,
    port = null,
    baseUrl = null,
    relativePath = null
  ) => {
    return ifJoin(
      `/`,
      setProtocol(protocol),
      setHost(hostname, port),
      baseUrl,
      relativePath
    );
  };
  const setProtocol = (protocol) => {
    return notEmpty(protocol) ? `${protocol}:/` : null;
  };
  const setHost = (hostname, port) => {
    return notEmpty(hostname) && notEmpty(port)
      ? ifJoin(`:`, hostname, port)
      : null;
  };
  const setRoute = (...args) => {
    return `/${args?.join(`/`)}`;
  };
  const setAgent = (protocol = `http`) => {
    switch (protocol.toLocaleLowerCase()) {
      case `https`:
        return new https.Agent({
          rejectUnauthorized: false,
        });
      default:
        return new http.Agent({
          rejectUnauthorized: false,
        });
    }
  };
  const setHeaders = (
    contentType = `application/json`,
    uid = null,
    pwd = null,
    token = null
  ) => {
    return {
      "Content-Type": contentType,
      "Private-Token": setAuthorization(uid, pwd, token),
    };
  };
  const setAuthorization = (uid = null, pwd = null, token = null) => {
    if (uid && pwd && !token) {
      // Basic authentication
      return `Basic ${toBase64(getBytes(`${uid}:${pwd}`))}`;
    } else if (token) {
      // Tokenized or Bearer authentication
      return `Bearer ${token}`;
    }
    return ``;
  };
  const getRequestStatusCode = (logType, methodName = null) => {
    let isError = [`error`, `Error`, `ERROR`].includes(logType || ``);
    if (
      [`auth`, `authenticate`, `authentication`, `login`, `logout`].includes(
        (methodName || ``).toLocaleLowerCase().trim()
      ) &&
      isError
    )
      return 401;
    return isError ? 400 : 200;
  };
  const getRequestPacket = (request) => {
    if (notSet(request)) return null;
    const method = request.method;
    const protocol = request.protocol;
    const hostname = request.hostname;
    const port = lastElement(request.socket.server._connectionKey, `:`);
    const baseUrl = request.baseUrl;
    const relativePath = request.path;
    const absoluteUrl = setUri(protocol, hostname, port, baseUrl, relativePath);
    return {
      contentType: request.headers[`content-type`],
      method: method,
      protocol: protocol,
      hostname: hostname,
      port: port,
      baseUrl: baseUrl,
      relativePath: relativePath,
      absoluteUrl: absoluteUrl,
      absoluteUrlWithHttpMethod: `${method}->${absoluteUrl}`,
      anchorName: relativePath.split(`/`).slice(-2, -1)[0],
      action: relativePath.split(`/`).slice(-1)[0],
      parameters: request.params,
      body: request.body,
      client: {
        address: request.connection.remoteAddress,
        useragent: request.headers[`user-agent`],
      },
    };
  };
  const getHttpRequestUriPath = (request) => {
    if (request === null || request === undefined) return null;
    let port = getLastElement(request.socket.server._connectionKey, `:`);
    return `${request.method}->${request.protocol}://${request.hostname}${
      port ? `:${port}` : ``
    }${request.path}`.trim();
  };
  const formatRequestResult = (error = null, data = null, message = null) => {
    return {
      data: data,
      hasError: isSet(error),
      message: message || error?.message || error,
      messageDetailed: error?.stack || error?.stackTrace,
    };
  };
  const getHttpRequestMethodName = (request) => {
    return getLastElement(getHttpRequestUriPath(request), `/`);
  };
  const getRequestQueryParameters = (request) => {
    const parameters = {};
    const queryParams = ifThen(request.query, request.params, request.body) || {};
    Object.keys(queryParams).forEach((key, index) => {
      parameters[key] = Object.values(queryParams)[index];
    });
    return parameters;
  };
  const getRequestQueryParametersWithoutUid = (request) => {
    const parameters = {};
    const queryParams = ifThen(request.query, request.params, request.body) || {};
    Object.keys(queryParams)
      .filter((qp) => !uid_field_list.includes(qp))
      .forEach((key, index) => {
        parameters[key] = Object.values(queryParams)[index];
      });
    return parameters;
  };
  const getRequestUid = (request) => {
    const queryParams = getRequestQueryParameters(request) || {};
    return queryParams[
      Object.keys(queryParams)?.find((qp) => uid_field_list.includes(qp))
    ];
  };
  const getRequestUserCredentials = (request) => {
    const queryParams = getRequestQueryParameters(request) || {};
    const uid =
      queryParams[
        Object.keys(queryParams)?.find((qp) => uid_field_list.includes(qp))
      ];
    const pwd =
      queryParams[
        Object.keys(queryParams)?.find((qp) => pwd_field_list.includes(qp))
      ];
    return { loginId: uid, password: pwd };
  };
  return {
    setUri,
    setProtocol,
    setHost,
    setRoute,
    setAgent,
    setHeaders,
    setAuthorization,
    getRequestStatusCode,
    getRequestPacket,
    getHttpRequestUriPath,
    formatRequestResult,
    getHttpRequestMethodName,
    getRequestUid,
    getRequestQueryParameters,
    getRequestQueryParametersWithoutUid,
    getRequestUserCredentials,
  };
};
