using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon_Nutrex.Utils
{
    using System.Text.Json;

    public static class FileStorage
    {
        public static void Save<T>(string path, List<T> data)
        {
            string? directory = Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory))
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }


        public static List<T> Load<T>(string path)
        {
            if (!File.Exists(path))
                return new List<T>();

            var json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return new List<T>();

            // Returns a new list if Deserialize returns null
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

    }
}
