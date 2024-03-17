using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ReflectionPropertyFactory
{
    internal static class TestDataHelper
    {
        internal static Dictionary<string, Type> CreateIntProperties()
        {
            var dictionary = new Dictionary<string, Type>
            {
                { "Intproperty1", typeof(int) },
                { "Intproperty2", typeof(int) },
                { "Intproperty3", typeof(int) }
            };

            return dictionary;
        }

        internal static Dictionary<string, Type> CreateStringProperties()
        {
            var dictionary = new Dictionary<string, Type>
            {
                { "Stringproperty1", typeof(string) },
                { "Stringproperty2", typeof(string) },
                { "Stringproperty3", typeof(string) }
            };

            return dictionary;
        }

        internal static Dictionary<string, Type> CreateDoubleProperties()
        {
            var dictionary = new Dictionary<string, Type>
            {
                { "Doubleproperty1", typeof(double) },
                { "Doubleproperty2", typeof(double) },
                { "Doubleproperty3", typeof(double) }
            };

            return dictionary;
        }

        internal static Dictionary<string, Type> CreateMixedProperties()
        {
            var dictionary = new Dictionary<string, Type>
            {
                { "Intproperty1", typeof(int) },
                { "Stringproperty2", typeof(string) },
                { "Doubleproperty3", typeof(double) }
            };

            return dictionary;
        }
    }
}
