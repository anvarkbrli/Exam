using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels
{
    public class LoginVM
    {
        [MinLength(5, ErrorMessage = "Username Or Email must contain minimum 5 characters!")]
        [MaxLength(25, ErrorMessage = "Username Or Email must contain maximum 25 characters!")]
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }


    }
}
