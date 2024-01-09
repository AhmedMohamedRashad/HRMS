using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Model;
using Demo.BL.Repository;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRep department,IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var data = await department.Get();
            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(obj);
                    await department.Create(data);
                    return RedirectToAction("Index");
                }
                TempData["msg"] = "Validation Error ";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                return View(obj);

            }
        }
        public async Task<IActionResult> Update(int id)
        {
            var data = await department.GetById(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(obj);
                    await department.Update(data);
                    return RedirectToAction("Index");
                }
                TempData["msg"] = "Validation Error ";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                return View(obj);

            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var data = await department.GetById(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await department.GetById(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentVM obj)
        {
            try
            {
                var data = mapper.Map<Department>(obj);
                await department.Delete(data.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                return View(obj);

            }
        }


    }
}

