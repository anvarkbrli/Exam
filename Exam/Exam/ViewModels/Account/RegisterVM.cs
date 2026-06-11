using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels
{
    public class RegisterVM
    {
        [MinLength(3, ErrorMessage = "Name must contain minimum 3 characters!")]
        [MaxLength(15, ErrorMessage = "Name must contain maximum 15 characters!")]
        public string Name { get; set; }
        [MinLength(6, ErrorMessage = "Surname must contain minimum 6 characters!")]
        [MaxLength(20, ErrorMessage = "Surname must contain maximum 20 characters!")]
        public string Surname { get; set; }
        [MinLength(5, ErrorMessage = "Username must contain minimum 5 characters!")]
        [MaxLength(25, ErrorMessage = "Username must contain maximum 25 characters!")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
