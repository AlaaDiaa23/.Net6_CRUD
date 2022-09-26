using System.ComponentModel.DataAnnotations;

namespace TestCRUD.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Department Name ")]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;

    }
}
