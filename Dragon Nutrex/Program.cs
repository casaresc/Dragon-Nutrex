using System;
using System.Windows.Forms;
using Dragon_Nutrex.Common;
using Dragon_Nutrex.Views;

namespace Dragon_Nutrex
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Manejo global de excepciones en UI
            Application.ThreadException += (sender, args) =>
            {
                GlobalExceptionHandler.Handle(args.Exception);
            };

            // Manejo global de excepciones fuera del hilo UI
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception ex = (Exception)args.ExceptionObject;
                GlobalExceptionHandler.Handle(ex);
            };

            Application.Run(new ConsumoVsMetaForm());
        }
    }
}