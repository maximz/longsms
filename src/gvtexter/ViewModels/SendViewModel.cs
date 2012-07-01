using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using gvtexter.Helpers;
using System.Web.Mvc;

namespace gvtexter.ViewModels
{
    public class SendViewModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        [AllowHtml]
        public string Username
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter your password.")]
        [AllowHtml]
        public string Password
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter the phone number you are texting.")]
        [AllowHtml]
        public string To
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter a message.")]
        [AllowHtml]
        public string Text
        {
            get;
            set;
        }
    }
}