using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.configuration
{
    public class ProjectScaffoldingSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ProjectScaffoldingElementCollection ProjectScaffolding
        {
            get
            {
                ProjectScaffoldingElementCollection projectScaffoldingElements = (ProjectScaffoldingElementCollection)base[""];
                return projectScaffoldingElements;
            }
        }
    }

    public class ProjectScaffoldingElementCollection : ConfigurationElementCollection
    {
        public ProjectScaffoldingElementCollection()
        {
            ProjectScaffoldingElement projectScaffoldingElement = (ProjectScaffoldingElement)CreateNewElement();
            if (projectScaffoldingElement != null && projectScaffoldingElement.Name != "")
            {
                Add(projectScaffoldingElement);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProjectScaffoldingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProjectScaffoldingElement)element).Name;
        }

        public ProjectScaffoldingElement this[int index]
        {
            get
            {
                return (ProjectScaffoldingElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public ProjectScaffoldingElement this[string name]
        {
            get { return (ProjectScaffoldingElement)BaseGet(name); }
        }

        public int IndexOf(ProjectScaffoldingElement ProjectScaffoldingElement)
        {
            return BaseIndexOf(ProjectScaffoldingElement);
        }

        public void Add(ProjectScaffoldingElement ProjectScaffoldingElement)
        {
            BaseAdd(ProjectScaffoldingElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override string ElementName => "project";
    }

    public class ProjectScaffoldingElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("projectType", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string ProjectType
        {
            get { return (string)this["projectType"]; }
            set { this["projectType"] = value; }
        }

        [ConfigurationProperty("author", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Author
        {
            get { return (string)this["author"]; }
            set { this["author"] = value; }
        }
        
        [ConfigurationProperty("description", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Description
        {
            get { return (string)this["description"]; }
            set { this["description"] = value; }
        }

        [ConfigurationProperty("environments", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Environments
        {
            get { return (string)this["environments"]; }
            set { this["environments"] = value; }
        }

        [ConfigurationProperty("entryClass", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string EntryClass
        {
            get { return (string)this["entryClass"]; }
            set { this["entryClass"] = value; }
        }
        
        [ConfigurationProperty("githubAccount", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string GithubAccount
        {
            get { return (string)this["githubAccount"]; }
            set { this["githubAccount"] = value; }
        }
        
        [ConfigurationProperty("keywords", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Keywords
        {
            get { return (string)this["keywords"]; }
            set { this["keywords"] = value; }
        }
        
        [ConfigurationProperty("licenseType", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string LicenseType
        {
            get { return (string)this["licenseType"]; }
            set { this["licenseType"] = value; }
        }
        
        [ConfigurationProperty("version", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Version
        {
            get { return (string)this["version"]; }
            set { this["version"] = value; }
        }

        [ConfigurationProperty("rootDirectory", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string RootDirectory
        {
            get { return (string)this["rootDirectory"]; }
            set { this["rootDirectory"] = value; }
        }

        [ConfigurationProperty("enableSwagger", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool EnableSwagger
        {
            get { return (bool)this["enableSwagger"]; }
            set { this["enableSwagger"] = value; }
        }

        [ConfigurationProperty("canUseMsSqlDb", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool CanUseMsSqlDb
        {
            get { return (bool)this["canUseMsSqlDb"]; }
            set { this["canUseMsSqlDb"] = value; }
        }

        [ConfigurationProperty("canUseMongoDb", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool CanUseMongoDb
        {
            get { return (bool)this["canUseMongoDb"]; }
            set { this["canUseMongoDb"] = value; }
        }

        [ConfigurationProperty("canUsePostgreSqlDb", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool CanUsePostgreSqlDb
        {
            get { return (bool)this["canUsePostgreSqlDb"]; }
            set { this["canUsePostgreSqlDb"] = value; }
        }

        [ConfigurationProperty("isActive", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool IsActive
        {
            get { return (bool)this["isActive"]; }
            set { this["isActive"] = value; }
        }

        [ConfigurationProperty("isGateway", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool IsGateway
        {
            get { return (bool)this["isGateway"]; }
            set { this["isGateway"] = value; }
        }

        [ConfigurationProperty("usesAuthentication", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public bool UsesAuthentication
        {
            get { return (bool)this["usesAuthentication"]; }
            set { this["usesAuthentication"] = value; }
        }

        [ConfigurationProperty("apiPort", IsRequired = true)]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string ApiPort
        {
            get { return (string)this["apiPort"]; }
            set { this["apiPort"] = value; }
        }

        [ConfigurationProperty("directories", IsRequired = false)]
        public ProjectScaffoldingDirectoryElementCollection Directories
        {
            get { return (ProjectScaffoldingDirectoryElementCollection)this["directories"]; }
        }

        [ConfigurationProperty("dataModels", IsRequired = false)]
        public ProjectScaffoldingDataModelElementCollection DataModels
        {
            get { return (ProjectScaffoldingDataModelElementCollection)this["dataModels"]; }
        }
    }

    public class ProjectScaffoldingDirectoryElementCollection : ConfigurationElementCollection
    {
        public new ProjectScaffoldingDirectoryElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (ProjectScaffoldingDirectoryElement)BaseGet(name);
            }
        }

        public ProjectScaffoldingDirectoryElement this[int index]
        {
            get { return (ProjectScaffoldingDirectoryElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();
            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name) return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProjectScaffoldingDirectoryElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProjectScaffoldingDirectoryElement)element).Name;
        }

        protected override string ElementName => "directory";
    }

    public class ProjectScaffoldingDirectoryElement : ConfigurationElement
    {
        public ProjectScaffoldingDirectoryElement() { }

        public ProjectScaffoldingDirectoryElement(string name, string anchorDirectory) : this()
        {
            this.Name = name;
            this.AnchorDirectory = anchorDirectory;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("anchorDirectory", IsRequired = false, IsKey = false, DefaultValue = "")]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string AnchorDirectory
        {
            get { return (string)this["anchorDirectory"]; }
            set { this["anchorDirectory"] = value; }
        }

        [ConfigurationProperty("directories", IsRequired = false)]
        public ProjectScaffoldingDirectoryElementCollection Directories
        {
            get { return (ProjectScaffoldingDirectoryElementCollection)this["directories"]; }
        }
    }

    public class ProjectScaffoldingDataModelElementCollection : ConfigurationElementCollection
    {
        public new ProjectScaffoldingDataModelElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (ProjectScaffoldingDataModelElement)BaseGet(name);
            }
        }

        public ProjectScaffoldingDataModelElement this[int index]
        {
            get { return (ProjectScaffoldingDataModelElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();
            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name) return idx;
            }
            return -1;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProjectScaffoldingDataModelElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProjectScaffoldingDataModelElement)element).Name;
        }

        protected override string ElementName => "dataModel";
    }

    public class ProjectScaffoldingDataModelElement : ConfigurationElement
    {
        public ProjectScaffoldingDataModelElement() { }

        public ProjectScaffoldingDataModelElement(string name) : this()
        {
            this.Name = name;
        }

        [ConfigurationProperty("name", IsRequired = true, IsKey = true, DefaultValue = "")]
        //[StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }
}
