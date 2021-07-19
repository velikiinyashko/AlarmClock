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
    /// <summary>
    /// класс работы с json файлами
    /// </summary>
    /// <typeparam name="T">класс для сериалицации данных</typeparam>
    public class JsonSettings<T>
    {

        public T load { get; set; }
        private readonly IEventAggregator _eventAggregator;

        public JsonSettings(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        /// <summary>
        /// загрузка и десериализация данныз
        /// </summary>
        /// <param name="fileName">имя файла в корне приложения</param>
        /// <param name="def">сущность по умолчанию</param>
        /// <returns></returns>
        public async Task<T> LoadConfig(string fileName, T def)
        {
            string _configPath = $"{Directory.GetCurrentDirectory()}\\{fileName}";

            if (File.Exists(_configPath))
            {
                try
                {
                    using (FileStream stream = File.OpenRead(_configPath))
                    {
                        var result = await JsonSerializer.DeserializeAsync<T>(stream);
                        return result;
                    }
                }
                catch (Exception)
                {
                    using (FileStream stream = File.Create(_configPath))
                    {
                        await JsonSerializer.SerializeAsync(stream, def);
                        return def;
                    }
                }
            }
            else
            {
                using (FileStream stream = File.Create(_configPath))
                {
                    await JsonSerializer.SerializeAsync(stream, def);
                    return def;
                }
            }
        }
        /// <summary>
        /// сохранение данных
        /// </summary>
        /// <param name="fileName">имя файла для записи</param>
        /// <param name="settings">данные для записи</param>
        /// <returns></returns>
        public async Task SaveConfig(string fileName ,T settings)
        {
            try
            {
                string _configPath = $"{Directory.GetCurrentDirectory()}\\{fileName}";

                using (FileStream stream = File.Create(_configPath))
                {
                    await JsonSerializer.SerializeAsync(stream, settings);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
