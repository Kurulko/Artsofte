using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace Artsofte.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter the {0}!")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter the {0}!")]
        [StringLength(20, MinimumLength = 3)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please, enter the {0}!")]
        [Range(18,115,ErrorMessage = "{0} must be over {1} and under {2}")]
        public int Age { get; set; }

        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public void Deconstruct(out SqlParameter pId,out SqlParameter pName,out SqlParameter pSurname,out SqlParameter pAge,out SqlParameter pLanguageId, out SqlParameter pDepartmentId)
        {
            pId = new("@Id", Id);
            pName = new("@Name", Name);
            pSurname = new("@Surname", Surname);
            pAge = new("@Age", Age);
            pLanguageId = new("@LanguageId", LanguageId);
            pDepartmentId = new("@DepartmentId", DepartmentId);
        }
    }
}
