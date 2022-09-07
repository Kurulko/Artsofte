using Artsofte.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Artsofte.Extensions
{
    public static class EmployeeExtensions
    {
        public static void IncludeLanguageAndDepartment(this Employee employee, ArtsofteContext context)
        {
            SqlParameter parameterLanguageId = new("@LanguageId", employee.LanguageId);
            employee.Language = context.Languages.FromSqlRaw("GetLanguagesById @LanguageId", parameterLanguageId).AsEnumerable()?.First();

            SqlParameter parameterDepartmentId = new("@DepartmentId", employee.DepartmentId);
            employee.Department = context.Departments.FromSqlRaw("GetDepartmentsById @DepartmentId", parameterDepartmentId).AsEnumerable()?.First();

        }
    }
}
