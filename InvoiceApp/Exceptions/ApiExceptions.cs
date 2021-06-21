using System;
using System.Globalization;

namespace InvoiceApp.Exceptions
{
    public class ApiExceptions :Exception
    {
        public ApiExceptions()
        {
        }
        
        public ApiExceptions(string message) : base(message)
        {
        }

        public ApiExceptions(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}