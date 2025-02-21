using System.ComponentModel.DataAnnotations;

namespace Web2212025.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }

        public int Age { get; set; }
        [Required]
        public required string Class { get; set; }
    }
}
