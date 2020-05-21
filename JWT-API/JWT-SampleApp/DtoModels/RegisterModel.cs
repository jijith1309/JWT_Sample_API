using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.DtoModels
{
    public  class RegisterModel
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "User Name should not exceed 20 characters")]
        public string Username { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MaxLength(10, ErrorMessage = "Password should not exceed 10 characters")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is required")]
        [MaxLength(20, ErrorMessage = "First Name should not exceed 20 characters")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}