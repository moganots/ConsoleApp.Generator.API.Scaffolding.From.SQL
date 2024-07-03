/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : No such controller interceptor module
|--------------------------------------------------------------------------------------------------------------------------------------------
 */

/**
 * Creates an instance of the NoSuchController interceptor module
 * @param {*} logger an instance of the logger module
 * @param {*} controllerName the name of the controller
 * @returns NoSuchController
 */
module.exports = (logger, controllerName) => {
  const create = (request, response) => {
    return noSuchControllerLogger(request, response);
  };
  const getAll = (request, response) => {
    return noSuchControllerLogger(request, response);
  };
  const getById = (request, response) => {
    return noSuchControllerLogger(request, response);
  };
  const getBy = (request, response) => {
    return noSuchControllerLogger(request, response);
  };
  const update = (request, response) => {
    return noSuchControllerLogger(request, response);
  };
  const _delete = (request, response) => {
    return noSuchControllerLogger(request, response);
  };
  const noSuchControllerLogger = (request, response) => {
    return logger.onHttpRequestCompleted(__filename, request, response, {
      hasError: true,
      message: `NoSuchController: ${controllerName}, does not exist or has not been created or defined`,
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
