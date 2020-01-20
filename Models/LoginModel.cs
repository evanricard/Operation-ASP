using System.ComponentModel.DataAnnotations;
using System;
namespace OperationASP.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
