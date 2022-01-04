using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteMVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage ="Your name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Your email address is required.")]
        [EmailAddress]
        public string Email { get; set; }
        
        [UIHint("MultilineText")]
        [Required(ErrorMessage = "A Message is required.")]
        public string Message { get; set; }
    }
}