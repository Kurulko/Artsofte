using System.ComponentModel.DataAnnotations;

namespace Artsofte.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Display(Name = "Programming language")]
        [Required(ErrorMessage = "Please, enter the {0}!")]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
