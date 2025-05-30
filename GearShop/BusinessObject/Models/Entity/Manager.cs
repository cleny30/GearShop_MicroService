﻿using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Entity
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Fullname { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(11)]
        public string SSN { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public bool IsAdmin { get; set; }
    }
}
