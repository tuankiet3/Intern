using System.ComponentModel.DataAnnotations;

namespace Register.Models
{
    public class Aspiration
    {
        [Key]
        public int AspirationId { get; set; }
        public string? AspirationName { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
