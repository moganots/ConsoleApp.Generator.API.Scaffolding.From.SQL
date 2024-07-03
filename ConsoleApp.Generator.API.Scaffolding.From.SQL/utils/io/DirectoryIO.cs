using ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.utils.io
{
    public interface IDirectoryIO
    {
        void Copy(string target, bool recursive = false);
        void Create();
        void Delete(bool recursive = false);
        bool Exists();
        void Move(string target, bool recursive = false);
        void Rename(string target);
        void Recreate(bool recursive = false);
    }
    internal class DirectoryIO : IDirectoryIO
    {
        public string? Path { get; set; }
        public bool IsPathSet => Path?.Trim().Length != 0;
        public DirectoryIO() { }
        public DirectoryIO(string? path):this()
        {
            Path = path;
        }
        public void Copy(string target, bool recursive = false) { }
        public void CopyRecursively(string targetPath, SearchOption searchOption = SearchOption.TopDirectoryOnly, string? author = null, string? dateCreated = null, bool? canUseMongoDb = false, bool? canUseMsSqlDb = false, bool? canUsePostgreSqlDb = true)
        {
            bool isDbDirectory = targetPath.IsDBDirectory();

            if (!isDbDirectory)
            {
                Directory.CreateDirectory(targetPath);
            }else if (isDbDirectory && targetPath.IsDBMongoDirectory() && (bool)canUseMongoDb!)
            {
                Directory.CreateDirectory(targetPath);
            }
            else if (isDbDirectory && targetPath.IsDBMsSqlDirectory() && (bool)canUseMsSqlDb!)
            {
                Directory.CreateDirectory(targetPath);
            }
            else if (isDbDirectory && targetPath.IsDBPostgreSqlDirectory() && (bool)canUsePostgreSqlDb!)
            {
                Directory.CreateDirectory(targetPath);
            }

            //Now Create all of the sub (child) directories
            foreach (string dirPath in Directory.GetDirectories(Path!, "*", searchOption))
            {
                string subDirectoryPath = dirPath.Replace(Path!, targetPath);
                isDbDirectory = subDirectoryPath.IsDBDirectory();
                if (!isDbDirectory)
                {
                    Directory.CreateDirectory(subDirectoryPath);
                }
                else if (isDbDirectory && targetPath.IsDBMongoDirectory() && (bool)canUseMongoDb!)
                {
                    Directory.CreateDirectory(subDirectoryPath);
                }
                else if (isDbDirectory && targetPath.IsDBMsSqlDirectory() && (bool)canUseMsSqlDb!)
                {
                    Directory.CreateDirectory(subDirectoryPath);
                }
                else if (isDbDirectory && targetPath.IsDBPostgreSqlDirectory() && (bool)canUsePostgreSqlDb!)
                {
                    Directory.CreateDirectory(subDirectoryPath);
                }
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(Path!, "*.*", searchOption))
            {
                string content = File.ReadAllText(newPath).Replace("@Author", author).Replace("@DateCreated", dateCreated);
                string copyTo = newPath.Replace(Path!, targetPath);
                string copyToDirectory = Directory.GetParent(copyTo).FullName;
                bool copyToDirectoryExists = Directory.Exists(copyToDirectory);
                isDbDirectory = copyTo.IsDBDirectory();

                if (!isDbDirectory && copyToDirectoryExists)
                {
                    new FileIO(copyTo).Replace(content);
                }
                else if (isDbDirectory && copyToDirectoryExists && copyTo.IsDBMongoDirectory() && (bool)canUseMongoDb!)
                {
                    new FileIO(copyTo).Replace(content);
                }
                else if (isDbDirectory && copyToDirectoryExists && copyTo.IsDBMsSqlDirectory() && (bool)canUseMsSqlDb!)
                {
                    new FileIO(copyTo).Replace(content);
                }
                else if (isDbDirectory && copyToDirectoryExists && copyTo.IsDBPostgreSqlDirectory() && (bool)canUsePostgreSqlDb!)
                {
                    new FileIO(copyTo).Replace(content);
                }
                else if (isDbDirectory && copyToDirectoryExists)
                {
                    new FileIO(copyTo).Replace(content);
                }
            }
        }

        public void Create() {
            if (!Exists())
            {
                Directory.CreateDirectory(Path!);
            }
        }
        public void Delete(bool recursive = false)
        {
            if (Exists())
            {
                Directory.Delete(Path!, recursive);
            }
        }
        public bool Exists()
        {
            return Directory.Exists(Path);
        }
        public void Move(string target, bool recursive = false) { }
        public void Rename(string target) { }
        public void Recreate(bool recursive = false)
        {
            Delete(recursive);
            Create();
        }
    }
}
