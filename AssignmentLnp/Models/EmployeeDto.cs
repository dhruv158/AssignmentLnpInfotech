using System.ComponentModel.DataAnnotations;

namespace AssignmentLnp.Models
{
    public class EmployeeDto
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Mobile { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public bool IsActive { get; set; } = true;
        public string DepartmentName { get; set; } = string.Empty;
    }
}
