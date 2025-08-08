using System.ComponentModel.DataAnnotations;

namespace Register.Models
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }
        public string? ProvinceName { get; set; }
        public ICollection<Student>? Student { get; set; } 
        public ICollection<HighSchool>? HighSchools { get; set; }

    }
}
