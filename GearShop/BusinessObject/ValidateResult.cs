using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ValidateResult
    {
        public ValidateResult() { }
        public bool IsValid { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();
        public void AddError(string? property, string message)
        {
            IsValid = false;
            Errors.Add(new()
            {
                Property = property,
                Message = message
            });
        }
    }
}
