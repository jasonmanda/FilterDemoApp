using System;
using System.ComponentModel.DataAnnotations;

namespace FilterDemoApp.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
