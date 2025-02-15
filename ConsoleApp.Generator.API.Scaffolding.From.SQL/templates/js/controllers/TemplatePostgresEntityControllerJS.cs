﻿using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.templates.js.controllers
{
    public class TemplatePostgresEntityControllerJS
    {
        public string? Author { get; set; }
        public string? DateCreated { get; set; }
        public string? EntityName { get; set; }
        public string? SchemaName { get; set; }
        public string? ControllerDirectory { get; set; }
        public string FilePath => Path.Combine(ControllerDirectory!, $"{EntityName!.GetEntityControllerFileName()}.js");
        public TemplatePostgresEntityControllerJS() { }
        public TemplatePostgresEntityControllerJS(string author, string dateCreated, string schemaName, string entityName, string controllerDirectory)
        {
            Author = author;
            DateCreated = dateCreated;
            SchemaName = schemaName;
            EntityName = entityName;
            ControllerDirectory = controllerDirectory;
        }
        public void Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| Author        : {Author}");
            sb.AppendLine($"| Date Created  : {DateCreated}");
            sb.AppendLine($"| Description   : PostgreSql Entity (Model) Controller for the [{SchemaName}].[{EntityName}] Table");
            sb.AppendLine("|--------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine(" */");
            sb.AppendLine("");
            sb.AppendLine("/**");
            sb.AppendLine($" * Creates an instance of the [{SchemaName}].[{EntityName}] PostgreSql Entity (Model) Controller");
            sb.AppendLine(" * @param {*} logger an instance of the logger module");
            sb.AppendLine(" * @param {*} dbContext an instance of the DBContext module");
            sb.AppendLine($" * @returns PostgreSql{EntityName!.ProperCase()}Controller");
            sb.AppendLine(" */");
            sb.AppendLine("module.exports = (logger, dbContext) => {");
            sb.AppendLine("  require(`expose-gc`);");
            sb.AppendLine("  const { isSet, notSet, ifThen } = require(`./../../utils/general/object.util`)();");
            sb.AppendLine("  if (notSet(logger)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine($"      `PostgreSql{EntityName!.ProperCase()}Controller - [logger] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  if (notSet(dbContext)) {");
            sb.AppendLine("    throw new Error(");
            sb.AppendLine($"      `PostgreSql{EntityName!.ProperCase()}Controller - [dbContext] instance has not been provided or set`");
            sb.AppendLine("    );");
            sb.AppendLine("  }");
            sb.AppendLine("  const dto =");
            sb.AppendLine($"    require(`./../../models/entities/postgres/{EntityName!.GetEntityFileName()}`);");
            //sb.AppendLine("  const dbContext = require(`./../../middleware/database/context/postgres/db.context`)(");
            //sb.AppendLine("    logger,");
            //sb.AppendLine("    config");
            //sb.AppendLine("  );");
            sb.AppendLine("  const { getRequestUid, getRequestQueryParametersWithoutUid } =");
            sb.AppendLine("    require(`./../../utils/general/http.util`)();");
            sb.AppendLine("");
            sb.AppendLine("  /**");
            sb.AppendLine("   * Creates a new Entity");
            sb.AppendLine("   * @param {*} request an instance of the Client Request object");
            sb.AppendLine("   * @param {*} response an instace of the Client Response object");
            sb.AppendLine("   * @returns any");
            sb.AppendLine("   */");
            sb.AppendLine("  const create = (request, response) => {");
            sb.AppendLine("    try {");
            sb.AppendLine("      dbContext.create(");
            sb.AppendLine("        getRequestUid(request),");
            sb.AppendLine("        dto,");
            sb.AppendLine("        request.body,");
            sb.AppendLine("        (error, data, message) => {");
            sb.AppendLine("          return log(request, response, error, data, message);");
            sb.AppendLine("        }");
            sb.AppendLine("      );");
            sb.AppendLine("    } catch (exception) {");
            sb.AppendLine("      return log(request, response, exception, null, null);");
            sb.AppendLine("    } finally {");
            sb.AppendLine("      global.gc();");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  /**");
            sb.AppendLine("   * Gets all Entities");
            sb.AppendLine("   * @param {*} request an instance of the Client Request object");
            sb.AppendLine("   * @param {*} response an instace of the Client Response object");
            sb.AppendLine("   * @returns any[]");
            sb.AppendLine("   */");
            sb.AppendLine("  const getAll = (request, response) => {");
            sb.AppendLine("    try {");
            sb.AppendLine("      dbContext.getAll(getRequestUid(request), dto, (error, data, message) => {");
            sb.AppendLine("        return log(request, response, error, data, message);");
            sb.AppendLine("      });");
            sb.AppendLine("    } catch (exception) {");
            sb.AppendLine("      return log(request, response, exception, null, null);");
            sb.AppendLine("    } finally {");
            sb.AppendLine("      global.gc();");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  /**");
            sb.AppendLine("   * Filters (queries) and returns an Entity with the specified ID");
            sb.AppendLine("   * @param {*} request an instance of the Client Request object");
            sb.AppendLine("   * @param {*} response an instace of the Client Response object");
            sb.AppendLine("   * @returns any");
            sb.AppendLine("   */");
            sb.AppendLine("  const getById = (request, response) => {");
            sb.AppendLine("    try {");
            sb.AppendLine("      dbContext.getById(");
            sb.AppendLine("        getRequestUid(request),");
            sb.AppendLine("        dto,");
            sb.AppendLine("        request.params.id,");
            sb.AppendLine("        (error, data, message) => {");
            sb.AppendLine("          return log(request, response, error, data, message);");
            sb.AppendLine("        }");
            sb.AppendLine("      );");
            sb.AppendLine("    } catch (exception) {");
            sb.AppendLine("      return log(request, response, exception, null, null);");
            sb.AppendLine("    } finally {");
            sb.AppendLine("      global.gc();");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  /**");
            sb.AppendLine("   * Filters (queries) and returns the Entity(ies) with the specified parameter(s) or predicate");
            sb.AppendLine("   * @param {*} request an instance of the Client Request object");
            sb.AppendLine("   * @param {*} response an instace of the Client Response object");
            sb.AppendLine("   * @returns any");
            sb.AppendLine("   */");
            sb.AppendLine("  const getBy = (request, response) => {");
            sb.AppendLine("    try {");
            sb.AppendLine("      dbContext.getBy(");
            sb.AppendLine("        getRequestUid(request),");
            sb.AppendLine("        dto,");
            sb.AppendLine("        getRequestQueryParametersWithoutUid(request),");
            sb.AppendLine("        (error, data, message) => {");
            sb.AppendLine("          return log(request, response, error, data, message);");
            sb.AppendLine("        }");
            sb.AppendLine("      );");
            sb.AppendLine("    } catch (exception) {");
            sb.AppendLine("      return log(request, response, exception, null, null);");
            sb.AppendLine("    } finally {");
            sb.AppendLine("      global.gc();");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  /**");
            sb.AppendLine("   * Updates (changes or edits) an Entity");
            sb.AppendLine("   * @param {*} request an instance of the Client Request object");
            sb.AppendLine("   * @param {*} response an instace of the Client Response object");
            sb.AppendLine("   * @returns any");
            sb.AppendLine("   */");
            sb.AppendLine("  const update = (request, response) => {");
            sb.AppendLine("    try {");
            sb.AppendLine("      dbContext.update(");
            sb.AppendLine("        getRequestUid(request),");
            sb.AppendLine("        dto,");
            sb.AppendLine("        request.body,");
            sb.AppendLine("        (error, data, message) => {");
            sb.AppendLine("          return log(request, response, error, data, message);");
            sb.AppendLine("        }");
            sb.AppendLine("      );");
            sb.AppendLine("    } catch (exception) {");
            sb.AppendLine("      return log(request, response, exception, null, null);");
            sb.AppendLine("    } finally {");
            sb.AppendLine("      global.gc();");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  /**");
            sb.AppendLine("   * Deletes (removes) an Entity");
            sb.AppendLine("   * @param {*} request an instance of the Client Request object");
            sb.AppendLine("   * @param {*} response an instace of the Client Response object");
            sb.AppendLine("   * @returns any");
            sb.AppendLine("   */");
            sb.AppendLine("  const _delete = (request, response) => {");
            sb.AppendLine("    try {");
            sb.AppendLine("      dbContext.delete(");
            sb.AppendLine("        getRequestUid(request),");
            sb.AppendLine("        dto,");
            sb.AppendLine("        request.params.id,");
            sb.AppendLine("        (error, data, message) => {");
            sb.AppendLine("          return log(request, response, error, data, message);");
            sb.AppendLine("        }");
            sb.AppendLine("      );");
            sb.AppendLine("    } catch (exception) {");
            sb.AppendLine("      return log(request, response, exception, null, null);");
            sb.AppendLine("    } finally {");
            sb.AppendLine("      global.gc();");
            sb.AppendLine("    }");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  const log = (request, response, error, data, message) => {");
            sb.AppendLine("    return logger.onHttpRequestCompleted(__filename, request, response, {");
            sb.AppendLine("      data: data,");
            sb.AppendLine("      hasError: isSet(error),");
            sb.AppendLine("      message: ifThen(error?.message, error, message),");
            sb.AppendLine("      messageDetail: ifThen(error?.stack, error?.stackTrace),");
            sb.AppendLine("    });");
            sb.AppendLine("  };");
            sb.AppendLine("");
            sb.AppendLine("  return {");
            sb.AppendLine("    create,");
            sb.AppendLine("    getAll,");
            sb.AppendLine("    getById,");
            sb.AppendLine("    getBy,");
            sb.AppendLine("    update,");
            sb.AppendLine("    delete: _delete,");
            sb.AppendLine("  };");
            sb.AppendLine("};");
            sb.AppendLine("");
            new DirectoryIO(ControllerDirectory).Create();
            new FileIO(FilePath).Replace(sb.ToString());
        }
    }
}
