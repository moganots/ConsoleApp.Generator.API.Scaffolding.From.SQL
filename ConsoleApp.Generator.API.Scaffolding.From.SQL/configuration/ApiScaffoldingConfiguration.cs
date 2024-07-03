using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Generator.API.Scaffolding.From.SQL.configuration
{
    public class ApiScaffoldingInstanceElement: ConfigurationElement
    {
        // Create a property to store the name of the API Scaffolding Instance
        // - The "name" is the name of the XML attribute for the property
        // - The IsKey setting specifies that this field is used to identify
        //   element uniquely
        // - The IsRequired setting specifies that a value is required
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                // Return the value of the 'name' attribute as a string
                return (string)base["name"];
            }
            set
            {
                // Set the value of the 'name' attribute
                base["name"] = value;
            }
        }

        // Create a property to store the root directory of the API Scaffolding Instance
        // - The "rootDirectory" is the name of the XML attribute for the property
        // - The IsRequired setting specifies that a value is required
        [ConfigurationProperty("rootDirectory", IsRequired = true)]
        public string RootDirectory
        {
            get
            {
                // Return the value of the 'rootDirectory' attribute as a string
                return (string)base["rootDirectory"];
            }
            set
            {
                // Set the value of the 'rootDirectory' attribute
                base["rootDirectory"] = value;
            }
        }
    }

    public class ApiScaffoldingInstanceCollection : ConfigurationElementCollection
    {
        // Create a property that lets us access an element in the
        // collection with the int index of the element
        public ApiScaffoldingInstanceElement this[int index]
        {
            get
            {
                // Gets the ApiScaffoldingInstanceElement at the specified
                // index in the collection
                return (ApiScaffoldingInstanceElement)BaseGet(index);
            }
            set
            {
                // Check if a ApiScaffoldingInstanceElement exists at the
                // specified index and delete it if it does
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                // Add the new ApiScaffoldingInstanceElement at the specified
                // index
                BaseAdd(index, value);
            }
        }

        // Create a property that lets us access an element in the
        // colleciton with the name of the element
        public new ApiScaffoldingInstanceElement this[string key]
        {
            get
            {
                // Gets the ApiScaffoldingInstanceElement where the name
                // matches the string key specified
                return (ApiScaffoldingInstanceElement)BaseGet(key);
            }
            set
            {
                // Checks if a ApiScaffoldingInstanceElement exists with
                // the specified name and deletes it if it does
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                // Adds the new ApiScaffoldingInstanceElement
                BaseAdd(value);
            }
        }

        // Method that must be overriden to create a new element
        // that can be stored in the collection
        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiScaffoldingInstanceElement();
        }

        // Method that must be overriden to get the key of a
        // specified element
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApiScaffoldingInstanceElement)element).Name;
        }
    }

    public class ApiScaffoldingDefaultDirectoryElement : ConfigurationElement
    {
        // Create a property to store the name of the API Scaffolding Default Directory
        // - The "name" is the name of the XML attribute for the property
        // - The IsKey setting specifies that this field is used to identify
        //   element uniquely
        // - The IsRequired setting specifies that a value is required
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                // Return the value of the 'name' attribute as a string
                return (string)base["name"];
            }
            set
            {
                // Set the value of the 'name' attribute
                base["name"] = value;
            }
        }

        // Create a property to store the anchorDirectory of the API Scaffolding Default Directory
        // - The "anchorDirectory" is the name of the XML attribute for the property
        // - The IsRequired setting specifies that a value is required
        [ConfigurationProperty("anchorDirectory", IsRequired = true)]
        public string AnchorDirectory
        {
            get
            {
                // Return the value of the 'anchorDirectory' attribute as a string
                return (string)base["anchorDirectory"];
            }
            set
            {
                // Set the value of the 'anchorDirectory' attribute
                base["anchorDirectory"] = value;
            }
        }
    }

    public class ApiScaffoldingDefaultDirectoryElementCollection : ConfigurationElementCollection
    {
        // Create a property that lets us access an element in the
        // collection with the int index of the element
        public ApiScaffoldingDefaultDirectoryElement this[int index]
        {
            get
            {
                // Gets the ApiScaffoldingDefaultDirectoryElement at the specified
                // index in the collection
                return (ApiScaffoldingDefaultDirectoryElement)BaseGet(index);
            }
            set
            {
                // Check if a ApiScaffoldingDefaultDirectoryElement exists at the
                // specified index and delete it if it does
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                // Add the new ApiScaffoldingDefaultDirectoryElement at the specified
                // index
                BaseAdd(index, value);
            }
        }

        // Create a property that lets us access an element in the
        // colleciton with the name of the element
        public new ApiScaffoldingDefaultDirectoryElement this[string key]
        {
            get
            {
                // Gets the ApiScaffoldingDefaultDirectoryElement where the name
                // matches the string key specified
                return (ApiScaffoldingDefaultDirectoryElement)BaseGet(key);
            }
            set
            {
                // Checks if a ApiScaffoldingDefaultDirectoryElement exists with
                // the specified name and deletes it if it does
                if (BaseGet(key) != null)
                    BaseRemoveAt(BaseIndexOf(BaseGet(key)));

                // Adds the new ApiScaffoldingDefaultDirectoryElement
                BaseAdd(value);
            }
        }

        // Method that must be overriden to create a new element
        // that can be stored in the collection
        protected override ConfigurationElement CreateNewElement()
        {
            return new ApiScaffoldingDefaultDirectoryElement();
        }

        // Method that must be overriden to get the key of a
        // specified element
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ApiScaffoldingDefaultDirectoryElement)element).Name;
        }
    }

    public class ApiScaffoldingConfig : ConfigurationSection
    {
        // Create a property that lets us access the collection
        // of ApiScaffoldingInstanceElements

        // Specify the name of the element used for the property
        [ConfigurationProperty("instances")]
        // Specify the type of elements found in the collection
        [ConfigurationCollection(typeof(ApiScaffoldingInstanceCollection))]
        public ApiScaffoldingInstanceCollection ApiScaffoldingInstances
        {
            get
            {
                // Get the collection and parse it
                return (ApiScaffoldingInstanceCollection)this["instances"];
            }
        }

        // Specify the name of the element used for the property
        [ConfigurationProperty("defaultDirectories")]
        // Specify the type of elements found in the collection
        [ConfigurationCollection(typeof(ApiScaffoldingDefaultDirectoryElementCollection))]
        public ApiScaffoldingDefaultDirectoryElementCollection ApiScaffoldingDefaultDirectories
        {
            get
            {
                // Get the collection and parse it
                return (ApiScaffoldingDefaultDirectoryElementCollection)this["defaultDirectories"];
            }
        }
    }

}
