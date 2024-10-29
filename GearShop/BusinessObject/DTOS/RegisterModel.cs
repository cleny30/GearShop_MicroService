using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOS
{
    public class RegisterModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }
 
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
