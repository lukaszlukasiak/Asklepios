using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Asklepios.Web.ServiceClasses
{
    public static class AppSettingsServices
    {
        public static void UpdateAppSetting(string key, string value)
        {
            var configJson = File.ReadAllText("appsettings.json");
            var config = JsonSerializer.Deserialize<Dictionary<string, object>>(configJson);
            config[key] = value;
            var updatedConfigJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("appsettings.json", updatedConfigJson);
        }

    }
}
