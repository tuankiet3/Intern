using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, enter the full name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please, enter your student code")]
        public int StudentCode { get; set; }
        [Required(ErrorMessage = "Please, enter your birth date")]
        [DisplayName("Birth Date")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDay { get; set; }
        [Required(ErrorMessage = "Please, enter your email")]
        [EmailAddress(ErrorMessage = "Please, enter a valid email address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please, enter your phone number")]
        [Phone]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
