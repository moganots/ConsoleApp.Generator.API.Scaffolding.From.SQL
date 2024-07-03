/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Main routing module
|------------------------------------------------------------------------------------------------------------------
*/

/*
|------------------------------------------------------------------------------------------------------------------
| module.exports
|------------------------------------------------------------------------------------------------------------------
*/
module.exports = (config, logger, app) => {
	/*
	|------------------------------------------------------------------------------------------------------------------
	| Dependency(ies)
	|------------------------------------------------------------------------------------------------------------------
	*/
	const express = require(`express`);
	const router = express.Router();
	const routeRoot = require(`./@core/root`)(config, logger, app, dbContext, router);
	const routeLookupCategory = require(`./entity.lookup.category/api`)(config, logger, app, dbContext, router);
	const routeLookupValue = require(`./entity.lookup.value/api`)(config, logger, app, dbContext, router);
	const routeApplication = require(`./entity.application/api`)(config, logger, app, dbContext, router);
	const routeApplicationConfiguration = require(`./entity.application.configuration/api`)(config, logger, app, dbContext, router);
	const routeApplicationEndpoint = require(`./entity.application.endpoint/api`)(config, logger, app, dbContext, router);
	return router;
};
