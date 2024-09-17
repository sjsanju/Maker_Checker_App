﻿using System.ComponentModel.DataAnnotations;

namespace DotNetCoreMVC_MakerChecker.Models
{
    public class Login
    {        
            [Key]
            public int UserId { get; set; }

            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string Role { get; set; }


        }
    
}
