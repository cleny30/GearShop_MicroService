using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Error
    {
        public string Message { get; set; } = string.Empty;
        public string? Property { get; set; }
        public Error() { }
        public Error(string message)
        {
            Message = message;
        }
        public Error(string property, string message)
        {
            Message = message;
            Property = property;
        }
    }
}
