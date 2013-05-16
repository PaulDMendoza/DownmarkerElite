using System.Collections;
using System.Collections.Generic;
namespace DownMarker.Core
{
    public class NamedValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public interface IRegistry
    {
       T GetValue<T>(string key, string name);
       void SetValue(string key, string name, object value);
       IEnumerable<NamedValue> GetValues(string key);
    }

}
