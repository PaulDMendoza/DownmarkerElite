using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace DownMarker.Core
{
   public class CurrentUserRegistry : IRegistry
   {

      public T GetValue<T>(string key, string name)
      {
         var regkey = Registry.CurrentUser.OpenSubKey(key, false);
         if (regkey == null)
         {
            return default(T);
         }
         else
         {
            try
            {
               object value = regkey.GetValue(name);
               if (value == null)
               {
                  return default(T);
               }
               else
               {
                  return (T)value;
               }
            }
            catch (InvalidCastException)
            {
               return default(T);
            }
         }
      }

      public void SetValue(string key, string name, object value)
      {
        if (value.GetType().IsEnum)
        {
            value = (int)value;
        }
         var regkey = Registry.CurrentUser.CreateSubKey(key);
         regkey.SetValue(name, value);
      }

      public IEnumerable<NamedValue> GetValues(string key)
      {
         var regkey = Registry.CurrentUser.OpenSubKey(key, false);
         if (regkey == null)
         {
            yield break;
         }
         else
         {
            foreach (string name in regkey.GetValueNames())
            {
               yield return new NamedValue()
               { 
                  Name = name,
                  Value = regkey.GetValue(name)
               };
            }
         }
      }
   }
}
