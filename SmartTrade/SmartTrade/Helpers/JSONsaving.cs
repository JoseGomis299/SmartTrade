using Avalonia;
using SmartTrade.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;

namespace SmartTrade.Helpers
{
    public class JSONsaving
    {
        public static async Task WriteToJsonFileAsync(object? item, string fileName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName+".json");

                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                }else if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to JSON file: {ex.Message}");
            }
        }

        public static async Task<T?> ReadFromJsonFileAsync<T>(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), fileName+".json");

                if (File.Exists(filePath))
                {
                    var json = await File.ReadAllTextAsync(filePath);
                    return JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    Console.WriteLine("JSON file not found.");
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return default;
            }
        }
    }
}
