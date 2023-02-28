using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string Name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> Search(string Name)
            => await _context.Employees.Where(E => E.Name.Contains(Name)).ToListAsync();
    }
}
