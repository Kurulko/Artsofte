using System.ComponentModel.DataAnnotations;

namespace Artsofte.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Department name")]
        [Required(ErrorMessage = "Please, enter the {0}!")]
        public string Name { get; set; }

        [Display(Name = "Department floor")]
        [Required(ErrorMessage = "Please, enter the {0}!")]
        public int Floor { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
