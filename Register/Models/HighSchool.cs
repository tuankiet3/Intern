using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models
{
    public class HighSchool
    {
        [Key]
        public int HighSchoolId { get; set; }
        public string? HighSchoolName { get; set; }
        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public Province? Province { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
