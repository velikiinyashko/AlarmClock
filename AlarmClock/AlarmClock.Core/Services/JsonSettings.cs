using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlarmClock.Core.Services
{
    public class JsonSettings<T>
    {

        public T load { get; set; }
        private readonly IEventAggregator _eventAggregator;

        public JsonSettings(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public async Task<T> LoadConfig(string fileName, T def)
        {
            string _configPath = $"{Directory.GetCurrentDirectory()}\\{fileName}";

            if (File.Exists(_configPath))
            {
                try
                {
                    using (FileStream stream = File.OpenRead(_configPath))
                    {
                        return await JsonSerializer.DeserializeAsync<T>(stream);
                        //_eventAggregator.GetEvent<GetGlobalConfig>().Publish(T);
                        //return Settings;
                    }
                }
                catch (Exception ex)
                {
                    using (FileStream stream = File.Create(_configPath))
                    {
                        await JsonSerializer.SerializeAsync(stream, def);
                        //_eventAggregator.GetEvent<GetGlobalConfig>().Publish(ConfigDefault());
                        return def;
                    }
                }
            }
            else
            {
                using (FileStream stream = File.Create(_configPath))
                {
                    await JsonSerializer.SerializeAsync(stream, def);
                    //_eventAggregator.GetEvent<GetGlobalConfig>().Publish(ConfigDefault());
                    return def;
                }
            }
        }

        public async Task SaveConfig(string fileName ,T settings)
        {
            try
            {
                string _configPath = $"{Directory.GetCurrentDirectory()}\\{fileName}";

                using (FileStream stream = File.Create(_configPath))
                {
                    await JsonSerializer.SerializeAsync(stream, settings);
                    //_eventAggregator.GetEvent<GetGlobalConfig>().Publish(settings);
                }
            }
            catch (Exception ex)
            {

            }
        }

        //private Settings ConfigDefault()
        //{
        //    Settings settings = new Settings();
        //    settings.Server = "http://localhost";
        //    settings.Port = 5000;
        //    settings.TokenSecurity = Guid.NewGuid().ToString();
        //    Settings = settings;
        //    return settings;
        //}





    }
}
