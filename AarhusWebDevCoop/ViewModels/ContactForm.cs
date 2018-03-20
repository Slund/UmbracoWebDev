using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please write a subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please write a message")]
        public string Message { get; set; }
    }
}