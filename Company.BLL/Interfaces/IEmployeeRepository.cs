using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string Name);
        Task<IEnumerable<Employee>> Search(string Name);
    }
}
