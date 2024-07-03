using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions
{
    public static class StringExtensions
    {
        public static string[] dbDirectories = { "database", "connection", "context", "entities", "models", "repository" };
        public static string[] dbMongoNames = { "mongo", "mongodb", "mongo db" };
        public static string[] dbMsSqlNames = { "mssql", "ms sql", "sql" };
        public static string[] dbPostgreSqlNames = { "postgres", "postgresql", "postgres sql", "postgre sql" };
        public static bool IsSet(this string s) { return s != null; }
        public static bool IsEmpty(this string s) { return s.IsSet() && s.Trim().Length == 0; }
        public static bool NotEmpty(this string s) { return !s.IsEmpty(); }
        public static string IfEmpty(this string s, string value = "") { return s.IsEmpty() ? value : s; }
        public static string GetLastElement(this string s, char delimiter = '.') { return s.IsSet() ? s.Split(delimiter).Last() : s; }
        public static string SplitCamelCase(this string s)
        {
            return s.IsSet() ? Regex.Replace(Regex.Replace(s, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2") : s;
        }
        public static string ProperCase(this string s)
        {
            if (!s.IsSet()) return s;
            return string.Join("", s.Split(' ', '.', '_', '-').Select(e => e.SplitCamelCase()).Select(e => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(e.ToLower())).ToArray());
        }
        public static string GetFormattedFL360ProjectCode(this string s, int index = 0)
        {
            return s.IsSet() ? $"FL{string.Join("", s.GetFormattedFL360ProjectName(index).RemoveNumberCodeFromProjectName().Split(' ', '.', '_', '-').Where(e => e.NotEmpty()).Select(e => e[0].ToString().ToUpper()).ToArray())}" : s;
        }
        public static string GetFormattedApiName(this string s, int index = 0)
        {
            return string.Join(".", s.GetFormattedFL360ProjectName(index).RemoveNumberCodeFromProjectName().Split(' ', '.', '_', '-').Where(e => e.NotEmpty()).ToArray());
        }
        public static string GetFormattedFL360ProjectName(this string s, int index = 0)
        {
            return s.IsSet() ? s.RemoveCountryStateCodeFromProjectName() : $"Fl360.Unnamed.Project.{index}";
        }
        public static string RemoveCountryStateCodeFromProjectName(this string s)
        {
            return s.IsSet() ? new Regex("[jhb]|[Jhb]|[JHB]").Replace(s, string.Empty) : s;
        }
        public static string RemoveNumberCodeFromProjectName(this string s)
        {
            return s.IsSet() ? new Regex("[fl]|[FL]|[360]|[api]|[Api]|[API]").Replace(s, string.Empty) : s;
        }
        public static string FormatMsSqlEntityName(this string s)
        {
            return s.ProperCase();
        }
        public static string FormatEntityRouteName(this string s)
        {
            return s.IsSet() ? $"{s.Replace('_', '.').Replace('-', '.').Replace(' ', '.').Trim()}" : s;
        }
        public static string FormatEntityNameRemoveSpecialCharacters(this string s)
        {
            return s.IsSet() ? $"{s.Replace('_', '.').Replace('-', '.').Replace(' ', '.').Trim()}" : s;
        }
        public static string GetEntityFileName(this string s)
        {
            return s.IsSet() ? $"entity.{s.FormatEntityNameRemoveSpecialCharacters()}" : s;
        }
        public static string GetEntityControllerFileName(this string s)
        {
            return s.IsSet() ? $"controller.{s.FormatEntityNameRemoveSpecialCharacters()}" : s;
        }
        public static string GetFormattedProjectDockerImageName(this string s, Dictionary<string, string>? replaceWith)
        {
            string regexReplace = $"/\b(?:{string.Join('|', replaceWith?.Keys?.ToArray()!)})\b/gi";
            return Regex.Replace(s, @regexReplace, matched => replaceWith![matched.Value], RegexOptions.IgnoreCase);
        }
        public static bool IsDBDirectory(this string s, string delimiter = "\\")
        {
            return s.NotEmpty() && s.Split(delimiter).Any(path => dbDirectories.Contains(path));
        }
        public static bool IsDBMongoDirectory(this string s, string delimiter = "\\")
        {
            return s.NotEmpty() && s.Split(delimiter).Any(path => dbMongoNames.Contains(path));
        }
        public static bool IsDBMsSqlDirectory(this string s, string delimiter = "\\")
        {
            return s.NotEmpty() && s.Split(delimiter).Any(path => dbMsSqlNames.Contains(path));
        }
        public static bool IsDBPostgreSqlDirectory(this string s, string delimiter = "\\")
        {
            return s.NotEmpty() && s.Split(delimiter).Any(path => dbPostgreSqlNames.Contains(path));
        }
    }
}
