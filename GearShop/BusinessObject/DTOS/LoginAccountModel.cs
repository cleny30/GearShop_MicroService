using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class LoginAccountModel
    {
        [MaxLength(50)]
        [MinLength(6)]
        [Required]
        public string Username { get; set; } = string.Empty;

        [MaxLength(32)]
        [MinLength(6)]
        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
