using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io
{
    public interface IFileIO
    {
        void Copy(string target);
        void Create(string content);
        void Delete();
        bool Exists();
        void Move(string target);
        void Rename(string target);
        void Replace(string content);
    }
    internal class FileIO : IFileIO
    {
        public string? Path { get; set; }
        public bool IsPathSet => Path?.Trim().Length != 0;
        public FileIO() { }
        public FileIO(string? path):this()
        {
            Path = path;
        }

        public void Copy(string target)
        {
            throw new NotImplementedException();
        }

        public void Create(string? content)
        {
            if(IsPathSet && content?.Trim().Length != 0)
            {
                File.WriteAllText(Path!, content);
            }
        }

        public void Delete()
        {
            if (Exists()) { File.Delete(Path!); }
        }

        public bool Exists()
        {
            return IsPathSet && File.Exists(Path);
        }

        public void Move(string target)
        {
            throw new NotImplementedException();
        }

        public void Rename(string target)
        {
            throw new NotImplementedException();
        }

        public void Replace(string content)
        {
            Delete();
            Create(content);
        }
    }
}
