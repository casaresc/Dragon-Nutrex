using System;
using System.IO;

namespace Dragon_Nutrex.Common
{
    public static class DataConfig
    {
        public static string GetStoragePath(string fileName)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo? root = new DirectoryInfo(currentDir);

            while (root != null && root.GetFiles("*.sln*").Length == 0)
            {
                root = root.Parent;
            }

            if (root != null)
            {
                var dataDir = root.GetDirectories("Data", SearchOption.AllDirectories)
                                  .FirstOrDefault();

                if (dataDir != null)
                {
                    return Path.Combine(dataDir.FullName, fileName);
                }

                string manualPath = Path.Combine(root.FullName, "Dragon Nutrex", "Data");
                if (!Directory.Exists(manualPath))
                {
                    Directory.CreateDirectory(manualPath);
                }
                return Path.Combine(manualPath, fileName);
            }

            return Path.Combine(currentDir, "Data", fileName);
        }
    }
}