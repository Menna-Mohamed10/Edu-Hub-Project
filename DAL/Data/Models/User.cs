﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LMS.DAL.Data.Models
{
    public class User : IdentityUser<int>
    {
        //[Key]
        //public int Id { get; set; }

        //[Required]
        //public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //[Required]
        //public string Email { get; set; }

        //[Required]
        //public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }
        public Student Student { get; set; }
        public Professor Professor { get; set; }
    }
}
