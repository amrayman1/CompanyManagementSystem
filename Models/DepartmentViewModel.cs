using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Company.PL.Models
{
    public class DepartmentViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}