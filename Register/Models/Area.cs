using System.ComponentModel.DataAnnotations;

namespace Register.Models
{
    public class Area
    {
        [Key]
        public int AreaId { get; set; }
        public string? AreaName { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
