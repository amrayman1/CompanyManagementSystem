using Company.DAL.Entities;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Company.PL.Models
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50, ErrorMessage ="Maximum Length of Name is 50 Chars")]
        [MinLength(6, ErrorMessage = "Minimum Length of Name is 6 Chars")]
        public string? Name { get; set; }
        [Range(18,60 , ErrorMessage ="Age Must be between 18 and 60")]
        public int? Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000,8000, ErrorMessage ="Salary must be between 4000 and 8000")]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentViewModel? Department { get; set; }
    }
}
