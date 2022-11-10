using Serilog;

namespace Devon4Net.Infrastructure.Common
{
    public static class Devon4NetLogger
    {
        public static void Debug(Exception exception)
        {
            var message = GetExceptionMessage(ref exception);
            Log.Debug(message);
        }

        public static void Debug(string message)
        {
            Console.WriteLine(message);
            Log.Debug(message);
        }
        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static void Error(Exception exception, string message = null)
        {
            Log.Error(CombineMessages(message, GetExceptionMessage(ref exception)));
        }

        public static void Fatal(string message)
        {
            Log.Fatal(message);
        }

        public static void Fatal(Exception exception, string message = null)
        {
            var localMessage = CombineMessages(message, GetExceptionMessage(ref exception));
            Log.Fatal(localMessage);
        }

        public static void Information(string message)
        {
            Log.Information(message);
        }

        public static void Information(Exception exception, string message = null)
        {
            var localMessage = CombineMessages(message, GetExceptionMessage(ref exception));
            Log.Information(localMessage);
        }

        public static void Warning(string message)
        {
            Log.Warning(message);
        }

        public static void Warning(Exception exception, string message = null)
        {
            var localMessage = CombineMessages(message, GetExceptionMessage(ref exception));
            Log.Warning(localMessage);
        }

        private static string GetExceptionMessage(ref Exception exception)
        {
            var message = !string.IsNullOrEmpty(exception.Message) ? exception.Message : "No Exception Message";
            var innerException = exception.InnerException?.Message != null ? exception.InnerException.Message : "No InnerException exception data found";
            var fullMessage = $"Exception Type: {exception.GetType().Name} | Message: {message} | InnerException: {innerException} | StackTrace = {exception.StackTrace}";
            return fullMessage;
        }

        private static string CombineMessages(string messageA, string messageB)
        {
            return string.IsNullOrEmpty(messageA) ? messageB : $"{messageA} | {messageB}";
        }
    }
}
