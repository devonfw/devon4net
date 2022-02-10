using System;

namespace Devon4Net.Infrastructure.Log
{
    public static class Devon4NetLogger
    {

        #region exception
        public static void Debug(Exception exception)
        {
            Serilog.Log.Debug(GetExceptionMessage(ref exception));
        }

        public static void Information(Exception exception)
        {
            Serilog.Log.Information(GetExceptionMessage(ref exception));
        }

        public static void Error(Exception exception)
        {
            Serilog.Log.Error(GetExceptionMessage(ref exception));
        }
        public static void Fatal(Exception exception)
        {            
            Serilog.Log.Fatal(GetExceptionMessage(ref exception));
        }
        #endregion

        #region messages
        public static void Debug(string message)
        {
            Serilog.Log.Debug(message);
            Console.WriteLine(message);
        }

        public static void Information(string message)
        {
            Serilog.Log.Information(message);
            Console.WriteLine(message);
        }

        public static void Error(string message)
        {
            Serilog.Log.Error(message);
            Console.WriteLine(message);
        }

        public static void Fatal(string message)
        {
            Serilog.Log.Fatal(message);
            Console.WriteLine(message);
        }
        #endregion

        private static string GetExceptionMessage(ref Exception exception)
        {
            var message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "No Exception Message";
            var innerException = exception.InnerException != null && exception.InnerException.Message != null ? exception.InnerException.Message : "No InnerException exception data found";
            var fullMessage = $"Exception Type: {exception.GetType().Name} | Message: {message} | InnerException: {innerException}";
            Console.WriteLine(fullMessage);
            return fullMessage;
        }

    }
}
