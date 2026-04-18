namespace Dragon_Nutrex_Web.Common
{
    public static class GlobalExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            Logger.Log(ex);

        }
    }
}