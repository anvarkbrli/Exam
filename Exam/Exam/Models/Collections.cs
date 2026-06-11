using Exam.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Collections: BaseEntity
    {
        public string Image { get; set; }
        [MinLength(3,ErrorMessage ="Name must contain minimum 3 characters!")]
        [MaxLength(15, ErrorMessage = "Name must contain maximum 15 characters!")]
        public string Name { get; set; }
        public int Stock { get; set; }
        [MinLength(6, ErrorMessage = "Category must contain minimum 6 characters!")]
        [MaxLength(20, ErrorMessage = "Category must contain maximum 20 characters!")]
        public string Category { get; set; }

    }
}
