using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string? StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        [Required]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ.")]
        [Required]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? CCCD { get; set; }

        [Required]
        public bool Prioritize { get; set; }

        [Required]
        public bool Gender { get; set; }


        [Required]
        [ForeignKey("Province")]
        public int? ProvinceId { get; set; }
        [Required]
        [ForeignKey("HighSchool")]
        public int? HighSchoolId { get; set; }
        [Required]
        [ForeignKey("Area")]
        public int? AreaId { get; set; }
        [Required]
        [ForeignKey("Major")]
        public int? MajorId { get; set; }
        [ForeignKey("Aspiration")]
        public int? AspirationId { get; set; }


        public Province? Province { get; set; }
        public HighSchool? HighSchool { get; set; }
        public Area? Area { get; set; }
        public Major? Major { get; set; }
        public Aspiration? Aspiration { get; set; }
    }
}