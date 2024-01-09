using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Model;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IDepartmentRep department;
        private readonly IMapper mapper;
        private readonly IEmployeeRep employee;
        private readonly ICityRep city;
        private readonly IDistrictRep district;

        public EmployeeController(IDepartmentRep department, IMapper mapper, IEmployeeRep employee, ICityRep city, IDistrictRep district)
        {
            this.department = department;
            this.mapper = mapper;
            this.employee = employee;
            this.city = city;
            this.district = district;
        }

        public async Task<IActionResult> Index(string? SearchValue = null)
        {
            
            if (SearchValue == null)
            {
                var data = await employee.Get(x => x.IsActive == true && x.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
            else
            {
                var data = await employee.Get(x => x.IsActive == true && x.IsDeleted == false && x.Name.Contains(SearchValue));
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }

        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.ImageName = FileUploader.UploadFile(obj.Image,"Images");
                    obj.CvName = FileUploader.UploadFile(obj.Cv, "Docs");

                    var data = mapper.Map<Employee>(obj);
                    await employee.Create(data);
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
            var data = await employee.GetById(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            //var DepartmentList = mapper.Map<IEnumerable<DepartmentVM>>(await department.Get());
            //ViewBag.DepartmentList = new SelectList(DepartmentList, "Id", "Name", result.DepartmentId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(obj);
                    await employee.Update(data);
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
            var data = await employee.GetById(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            //var DepartmentList = mapper.Map<IEnumerable<DepartmentVM>>(await department.Get());
            //ViewBag.DepartmentList = new SelectList(DepartmentList, "Id", "Name", result.DepartmentId);
            return View(result);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await employee.GetById(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            //var DepartmentList = mapper.Map<IEnumerable<DepartmentVM>>(await department.Get());
            //ViewBag.DepartmentList = new SelectList(DepartmentList, "Id", "Name", result.DepartmentId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeVM obj)
        {
            FileUploader.RemoveFile("Images", obj.ImageName);
            FileUploader.RemoveFile("Docs", obj.CvName);
            try
            {

                var data = mapper.Map<Employee>(obj);
                await employee.Delete(data.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
                return View(obj);
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetCitiesByCountryId(int countryId)
        {
            var data = await city.Get(x => x.CountryId == countryId);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> GetDistrictsByCityId(int cityId)
        {
            var data = await district.Get(x => x.CityId == cityId);
            var result = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(result);
        }
    }
}
