using Ryujinx.Ui.App.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Ryujinx.Ui.Common.Helper
{
    public static class ApplicationDataSerialiser
    {
        private static readonly string _cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ryujinx_app_data");
        private static readonly string _appCache = Path.Combine(_cacheFolder, "app_cache");

        public static void SaveCache(this List<ApplicationData> data)
        {
            Directory.CreateDirectory(_cacheFolder);
            var buffer = JsonSerializer.Serialize(data);
            File.WriteAllText(_appCache, buffer);
        }

        public static void LoadCache(this List<ApplicationData> data)
        {
            if (!File.Exists(_appCache))
            {
                return;
            }
            var buffer = File.ReadAllText(_appCache);
            var loadedApps = JsonSerializer.Deserialize<List<ApplicationData>>(buffer);
            data.AddRange(loadedApps);
        }

        public static void ClearAppDataCache()
        {
            File.Delete(_appCache);
        }
    }
}
