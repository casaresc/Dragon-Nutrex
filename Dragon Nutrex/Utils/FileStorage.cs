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
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
        }

        public static List<T> Load<T>(string path)
        {
            if (!File.Exists(path))
                return new List<T>();

            var json = File.ReadAllText(path);
            // Returns a new list if Deserialize returns null
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

    }
}
