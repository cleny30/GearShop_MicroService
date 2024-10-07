using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessObject
{
    public class DataResult
    {
        public DataResult() { }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public List<Error>? Errors { get; set; }
        public object? Result { get; set; }

        public void SetValidateResult(ValidateResult validate)
        {
            IsSuccess = validate.IsValid;
            Errors = validate.Errors;
        }
    }
}
