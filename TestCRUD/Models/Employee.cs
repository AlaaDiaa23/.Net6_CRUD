
namespace TestCRUD.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd-MMMM-yyyy}")]
        public DateTime Birthdate { get; set; }
        public decimal Salary { get; set; }
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }
        [MaxLength(14)]
        [MinLength(14)]
        [Display(Name ="National Id")]
        public string NationalId { get; set; }
        public int DepartId { get; set; }
        [ForeignKey(nameof(DepartId))]
        public Department ?Department { get; set; }


    }
}
