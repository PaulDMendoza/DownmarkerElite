using System.Collections.Generic;
using System.Linq;
using DownMarker.Core;

namespace DownMarker.Tests
{
   class FakeRegistry : IRegistry
   {
       private Dictionary<string, Dictionary<string, object>> values =
           new Dictionary<string, Dictionary<string, object>>();

       private void Prepare(string key)
       {
           if (!values.ContainsKey(key))
           {
               values[key] = new Dictionary<string, object>();
           }
       }

       public T GetValue<T>(string key, string name)
       {
           Prepare(key);
           if (!values[key].ContainsKey(name))
           {
               return default(T);
           }
           else
           {
               return (T)values[key][name];
           }
       }

       public void SetValue(string key, string name, object value)
       {
           Prepare(key);
           values[key][name] = value;
       }

       public IEnumerable<NamedValue> GetValues(string key)
       {
           Prepare(key);
           return values[key]
                // order in descending order to test that PersistentState class
                // sorts recently used in ascending order
               .OrderByDescending(x => x.Key) 
               .Select(x => new NamedValue { Name = x.Key, Value = x.Value });
       }
   }
}
