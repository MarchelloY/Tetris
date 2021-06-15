using System;
using System.Text;

namespace Utils
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception exception)
        {
            var builder = new StringBuilder();

            if (exception != null)
            {
                builder.Append(GetExceptionMessage(exception));

                var innerException = exception.InnerException;

                while (innerException != null)
                {
                    builder.Append($" ---> {GetExceptionMessage(innerException)}");
                    innerException = innerException.InnerException;
                }
            }

            return builder.ToString();
        }

        private static string GetExceptionMessage(Exception exception)
        {
            return $"{exception.GetType().FullName} : {exception.Message}";
        }
    }
}