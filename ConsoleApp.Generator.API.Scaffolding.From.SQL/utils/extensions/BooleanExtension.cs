using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions
{
    public static class BooleanExtension
    {
        public static string ifNotActive(this bool flag,  string value)
        {
            value = value.IfEmpty();
            return !flag ? value : string.Empty;
        }
    }
}
