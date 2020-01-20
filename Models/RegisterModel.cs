using System;
using System.ComponentModel.DataAnnotations;

namespace OperationASP.Models
{
    public class RegisterModel
    {
        //[Obsolete()] ? ???
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}