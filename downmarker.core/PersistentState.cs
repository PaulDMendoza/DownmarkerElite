using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Drawing;

namespace DownMarker.Core
{
   public class PersistentState
   {
      private readonly IRegistry registry;
      private const string REGKEY = @"software\downmarker";
      private const string REGKEY_RECENTLY_USED = REGKEY + @"\recentlyused";
      private const int MAX = 10;

      public PersistentState(IRegistry registry)
      {
         this.registry = registry;
      }

      public ViewOrientation EditorOrientation
      {
         get
         {
            return registry.GetValue<ViewOrientation>(REGKEY, "EditorOrientation");
         }
         set
         {
            registry.SetValue(REGKEY, "EditorOrientation", value);
         }
      }

      public Rectangle MainViewBounds 
      {
         get
         {
            return new Rectangle(
               registry.GetValue<int>(REGKEY, "MainViewX"),
               registry.GetValue<int>(REGKEY, "MainViewY"),
               registry.GetValue<int>(REGKEY, "MainViewWidth"),
               registry.GetValue<int>(REGKEY, "MainViewHeight"));
         }
         set
         {
            registry.SetValue(REGKEY, "MainViewX", value.X);
            registry.SetValue(REGKEY, "MainViewY", value.Y);
            registry.SetValue(REGKEY, "MainViewWidth", value.Width);
            registry.SetValue(REGKEY, "MainViewHeight", value.Height);
         }
      }

      public int EditorSize
      {
         get
         {
            return registry.GetValue<int>(REGKEY, "EditorSize");
         }
         set
         {
            registry.SetValue(REGKEY, "EditorSize", value);
         }
      }

      public bool PlainStyle
      {
         get
         {
            return registry.GetValue<string>(REGKEY, "Style") == "plain";
         }
         set
         {
            string s = value ? "plain" : "kburke";
            registry.SetValue(REGKEY, "Style", s);
         }
      }

      public IEnumerable<string> RecentlyUsedDocuments
      {
         get
         {
            return registry.GetValues(REGKEY_RECENTLY_USED)
                .OrderBy(x => x.Name) // name is 0 for most recent, then 1, etc.
                .Select(x => x.Value as string);
         }
      }

      public void TouchRecentlyUsed(string uri)
      {
         // add or bump uri to the top
         var uris = new List<string>(this.RecentlyUsedDocuments);
         if (uris.Contains(uri))
         {
            uris.Remove(uri);
         }
         uris.Insert(0, uri);
         
         // save to registry
         foreach (var namedValue in 
             uris.Take(MAX).Select(
                (value,index)=>new {Name=index.ToString(),Value=value}))
         {
            this.registry.SetValue(REGKEY_RECENTLY_USED, namedValue.Name, namedValue.Value);
         }
      }

   }
}
