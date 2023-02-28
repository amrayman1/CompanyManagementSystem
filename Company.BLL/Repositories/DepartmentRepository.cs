using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly CompanyDbContext? _context;

        public DepartmentRepository(CompanyDbContext context):base(context)
        {
            _context = context;
        }

        //public int Create(Department department)
        //{
        //    _context.Departments.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Departments.Remove(department);
        //    return _context.SaveChanges();
        //}

        //public Department Get(int? id)
        //    => _context.Departments.FirstOrDefault(D => D.Id == id);

        //public IEnumerable<Department> GetAll()
        //    => _context.Departments.ToList();

        //public int Update(Department department)
        //{
        //    _context.Departments.Update(department);
        //    return _context.SaveChanges();
        //}


    }
}
