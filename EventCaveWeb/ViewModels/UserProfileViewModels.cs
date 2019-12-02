using EventCaveWeb.Models;
using EventCaveWeb.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventCaveWeb.ViewModels
{
    public class DetailUserProfileViewModel
    {
        public string UserName { get; set; }
        public ImgurImage Picture;
        public string Bio { get; set; }
        public DateTime RegisteredAt { get; set; }
        public ICollection<Event> HostedEvents { get; set; }
        public ICollection<Event> EventsEnrolledIn { get; set; }
    }

    public class EditUserProfileViewModel
    {
        public string Username { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Picture { get; set; }

        public string Bio { get; set; }
    }

    public class ChangeUserPasswordViewModel
    {
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm your password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat your new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and repeated password do not match.")]
        public string RepeatedNewPassword { get; set; }
    }
}