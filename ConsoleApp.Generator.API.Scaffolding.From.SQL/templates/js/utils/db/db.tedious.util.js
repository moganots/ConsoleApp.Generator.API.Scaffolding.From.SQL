/*
|------------------------------------------------------------------------------------------------------------------
| Author        : @Author
| Date Created  : @DateCreated
| Description   : Tedious extenstion utililty class
|------------------------------------------------------------------------------------------------------------------
 */

module.exports = () => {
  const { toLocaleLowerCaseWithTrim } = require(`./../general/string.util`)();
  const Connection = require(`tedious`).Connection;
  const TYPES = require(`tedious`).TYPES;
  const getConnection = (connectionString) => {
    return new Connection(connectionString);
  };
  const closeConnection = (connection, request) => {
    if (connection && request) {
      connection.closeConnection(request.__connection);
    }
  };
  const getTediousMsSqlDataType = (typeName) => {
    switch (toLocaleLowerCaseWithTrim(typeName || ``)) {
      case `bit`:
        return TYPES.Bit;
      case `tinyint`:
        return TYPES.TinyInt;
      case `smallint`:
        return TYPES.SmallInt;
      case `int`:
        return TYPES.Int;
      case `bigint`:
        return TYPES.BigInt;
      case `numeric`:
        return TYPES.Numeric;
      case `decimal`:
        return TYPES.Decimal;
      case `smallmoney`:
        return TYPES.SmallMoney;
      case `money`:
        return TYPES.Money;
      case `float`:
        return TYPES.Float;
      case `real`:
        return TYPES.Real;
      case `smalldatetime`:
        return TYPES.SmallDateTime;
      case `datetime`:
        return TYPES.DateTime;
      case `datetime2`:
        return TYPES.DateTime2;
      case `datetimeoffset`:
        return TYPES.DateTimeOffset;
      case `time`:
        return TYPES.Time;
      case `date`:
        return TYPES.Date;
      case `char`:
        return TYPES.Char;
      case `varchar`:
        return TYPES.VarChar;
      case `text`:
        return TYPES.Text;
      case `nchar`:
        return TYPES.NChar;
      case `nvarchar`:
        return TYPES.NVarChar;
      case `ntext`:
        return TYPES.NText;
      case `binary`:
        return TYPES.Binary;
      case `varbinary`:
        return TYPES.VarBinary;
      case `image`:
        return TYPES.Image;
      case `null`:
        return TYPES.Null;
      case `tvp`:
        return TYPES.TVP;
      case `udt`:
        return TYPES.UDT;
      case `uniqueidentifier`:
        return TYPES.UniqueIdentifier;
      case `variant`:
        return TYPES.Variant;
      case `xml`:
        return TYPES.xml;
    }
  };
  const getJSMsSqlDataType = (typeName) => {
    switch (toLocaleLowerCaseWithTrim(typeName || ``)) {
      case `boolean`:
        return TYPES.Bit;
      case `date`:
        return TYPES.DateTime;
      case `number`:
        return TYPES.BigInt;
      default:
        return TYPES.NVarChar;
    }
  };
  return {
    getConnection,
    getTediousMsSqlDataType,
    getJSMsSqlDataType,
    closeConnection,
  };
};
