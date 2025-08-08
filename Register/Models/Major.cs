using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models
{
    public class Major
    {
        [Key]
        public int MajorId { get; set; }
        public string? MajorName { get; set; }
        public ICollection<Student>? Students { get; set; }

    }

}
