using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.classes
{
    public interface IProxyProject
    {
        string Name { get; set; }
        string ApiPort { get; set; }
        bool IsActive { get; set; }
    }
    public class ProxyProject : IProxyProject
    {
        public string Name { get; set; }
        public string ApiPort { get; set; }
        public bool IsActive { get; set; }
    }
}
