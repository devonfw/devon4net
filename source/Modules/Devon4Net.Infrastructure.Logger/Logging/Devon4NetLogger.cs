
namespace Devon4Net.Infrastructure.Logger.Logging
{
    public static class Devon4NetLogger
    {
        public static void Debug(Exception exception)
        {
            var message = GetExceptionMessage(ref exception);
            Serilog.Log.Debug(message);
            Console.WriteLine(message);
        }

        public static void Debug(string message)
        {
            Serilog.Log.Debug(message);
            Console.WriteLine(message);
        }
        public static void Error(string message)
        {
            Serilog.Log.Error(message);
            Console.WriteLine(message);
        }

        public static void Error(Exception exception)
        {
            Serilog.Log.Error(GetExceptionMessage(ref exception));
        }

        public static void Fatal(string message)
        {
            Serilog.Log.Fatal(message);
            Console.WriteLine(message);
        }

        public static void Fatal(Exception exception)
        {
            var message = GetExceptionMessage(ref exception);
            Serilog.Log.Fatal(message);
            Console.WriteLine(message);
        }

        public static void Information(string message)
        {
            Serilog.Log.Information(message);
            Console.WriteLine(message);
        }

        public static void Information(Exception exception)
        {
            var message = GetExceptionMessage(ref exception);
            Serilog.Log.Information(message);
            Console.WriteLine(message);
        }

        public static void Warning(string message)
        {
            Serilog.Log.Warning(message);
            Console.WriteLine(message);
        }

        public static void Warning(Exception exception)
        {
            var message = GetExceptionMessage(ref exception);
            Serilog.Log.Warning(message);
            Console.WriteLine(message);
        }

        private static string GetExceptionMessage(ref Exception exception)
        {
            var message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "No Exception Message";
            var innerException = exception.InnerException?.Message != null ? exception.InnerException.Message : "No InnerException exception data found";
            var fullMessage = $"Exception Type: {exception.GetType().Name} | Message: {message} | InnerException: {innerException} | StackTrace = {exception.StackTrace}";
            Console.WriteLine(fullMessage);
            return fullMessage;
        }
    }
}
