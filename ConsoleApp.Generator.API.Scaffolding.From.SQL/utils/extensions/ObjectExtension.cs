using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions
{
    public static class ObjectExtension
    {
        public static bool IsSet(this object obj)
        {
            return obj != null;
        }
        public static string ToStringWithTrim(this object obj)
        {
            return obj?.ToString()?.Trim()!;
        }
        public static string ToStringWithTrimStart(this object obj)
        {
            return obj?.ToString()?.TrimStart()!;
        }
        public static string ToStringWithTrimEnd(this object obj)
        {
            return obj?.ToString()?.TrimEnd()!;
        }
        public static object IfThen(this object obj, object value, object value2)
        {
            return obj.ToStringWithTrim() == value.ToStringWithTrim() ? value2 : obj.ToStringWithTrim() == value2.ToStringWithTrim() ? value : obj;
        }
        public static string AppendMongoDBRowTab(this int index)
        {
            return index != 0 ? "\r\t" : "\t";
        }
        public static string AppendPostgresDBRowTab(this int index)
        {
            return index != 0 ? "\r\t\t" : "\t\t";
        }
        public static string GetMongoDBDataType(this object obj)
        {
            switch (obj.ToString())
            {
                case "System.Bool":
                case "System.Boolean": return "types.Boolean";
                case "System.DateTime": return "types.Date";
                case "System.Guid": return "types.UUID";
                case "System.Byte":
                case "System.SByte":
                case "System.Decimal":
                case "System.Double":
                case "System.Float":
                case "System.Int":
                case "System.UInt":
                case "System.Int16":
                case "System.Int32":
                case "System.UInt16":
                case "System.Long":
                case "System.ULong":
                case "System.Short":
                case "System.UShort": return "types.Number";
                default: return "types.String";
            }
        }
        public static string GetPostgresDBDataType(this object obj)
        {
            switch (obj.ToString())
            {
                case "System.Bool":
                case "System.Boolean": return "bit";
                case "System.DateTime": return "timestamp with time zone";
                case "System.Guid": return "varchar";
                case "System.Byte":
                case "System.SByte":
                case "System.Decimal":
                case "System.Double":
                case "System.Float":
                case "System.Int":
                case "System.UInt":
                case "System.Int16":
                case "System.Int32":
                case "System.UInt16":
                case "System.Long":
                case "System.ULong":
                case "System.Short":
                case "System.UShort": return "number";
                default: return "varchar";
            }
        }
        public static string FormatRouteEntityName(this object obj)
        {
            if (!obj.IsSet()) return obj?.ToString()!;
            return string.Join("", obj.ToString()!.Split(' ', '.', '_', '-').Select(e => e.SplitCamelCase()).Select(e => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(e.ToLower())).ToArray());
        }
        public static string FormatRouteEntityDirectoryName(this object obj)
        {
            return obj.IsSet() ? $"route.{obj?.ToString()!.Replace('_', '.').Replace('-', '.').Replace(' ', '.').Trim()}" : obj?.ToString()!;
        }
    }
}
