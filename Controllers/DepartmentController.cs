using Company.BLL.Interfaces;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize(Roles = "Admin, HR")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAll();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var department = await _repository.Get(id);

            if(department == null)
                return NotFound();

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.Create(department);
                TempData["Message"] = "Department is Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var department = await _repository.Get(id);

            if (department == null)
                return NotFound();

            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Department department)
        {
            if(id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(department);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View(department);
                }
            }

            return View(department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var department = await _repository.Get(id);

            if (department == null)
                return NotFound();

            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id,Department department)
        {
            if (id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Delete(department);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View(department);
                }
            }

            return View(department);
        }

    }
}
