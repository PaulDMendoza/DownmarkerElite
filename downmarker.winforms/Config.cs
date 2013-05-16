using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DownMarker.WinForms
{
    public class Config
    {
        public static void SetSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app = config.AppSettings;
            app.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static string Read(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection app = config.AppSettings;

            KeyValueConfigurationElement keyValueConfigurationElement = app.Settings[key];
            if (keyValueConfigurationElement == null)
                return null;
            return keyValueConfigurationElement.Value;
        }
    }
}
