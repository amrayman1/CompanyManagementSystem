using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Entities;
using Company.PL.Helper;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Data;

namespace Company.PL.Controllers
{
    [Authorize(Roles = "Admin, HR")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue = "")
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var employess = await unitOfWork.EmployeeRepository.GetAll();
                var mappedEmployee = mapper.Map<IEnumerable<EmployeeViewModel>>(employess);
                return View(mappedEmployee);
            }
            else
            {
                var employess = await unitOfWork.EmployeeRepository.Search(SearchValue);
                var mappedEmployee = mapper.Map<IEnumerable<EmployeeViewModel>>(employess);
                return View(mappedEmployee);
            }
        }

        public async Task<IActionResult> Create()
        {
            // 1. ViewData 
            // is a dictionary object [.Net Framework 3.5 ]
            // helps us to transfer the data from controller to view
            //ViewData["Message"] = "Hello World";

            // 2. ViewBag is a dynamic property [.Net Framework 4]
            // helps us to transfer the data from controller to view

            //ViewBag.Message = "Hello World 2";

            // 3.TempData
            // is a dictionary object [.Net Framework 3.5 ]
            // it is used to transfer data between to consecunsive requests
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            //var mappedEmployee = new Employee()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    Email = employee.Email,
            //    HireDate = employee.HireDate,
            //    Salary = employee.Salary,
            //    PhoneNumber = employee.PhoneNumber,
            //    IsActive = employee.IsActive 
            //};

            if (ModelState.IsValid)
            {
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "imgs");

                var mappedEmployee = mapper.Map<Employee>(employee);
                await unitOfWork.EmployeeRepository.Create(mappedEmployee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();
            return View(employee);

        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
                return NotFound();

            var mappedEmployee = mapper.Map<EmployeeViewModel>(employee);


            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();

            return View(mappedEmployee);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeViewModel employee)
        {
            if(id != employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = mapper.Map<Employee>(employee);

                    await unitOfWork.EmployeeRepository.Update(mappedEmp);

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View(employee);
                }
            }

            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAll();

            return View(employee);
        }
    }
}
