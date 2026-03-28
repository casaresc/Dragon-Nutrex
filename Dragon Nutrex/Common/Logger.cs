using System;
using System.IO;

namespace Dragon_Nutrex.Common
{
    public static class Logger
    {
        private static readonly string logPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "logs.txt"
            );

        public static void Log(Exception ex)
        {
            try
            {
                string mensaje =
                    $"{DateTime.Now} | ERROR | {ex.Message} | {ex.StackTrace}";

                File.AppendAllText(logPath, mensaje + Environment.NewLine);
            }
            catch
            {
                // Evitar que falle el logger
            }
        }
    }
}