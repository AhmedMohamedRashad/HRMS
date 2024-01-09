using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Model;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRep employee, IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }
        [DisableCors]
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var data = await employee.Get(x => x.IsActive == true && x.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>
                {
                    Code = "200",
                    Message = "Data found",
                    Status = "Succeed",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Message = "Data Not found",
                    Status = "Not Found",
                    Data = ex.Message
                });
            }
               
       
        }
        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var data = await employee.GetById(x => x.Id==id && x.IsActive == true && x.IsDeleted == false );
                if (data == null)
                    throw new Exception(" this ID Not Exists");
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>
                {
                    Code = "200",
                    Message = "Data found",
                    Status = "Succeed",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Message = "Data Not found",
                    Status = "Not Found",
                    Data = ex.Message
                });
            }


        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeVM obj)
        {
            try
            {
                var data = mapper.Map<Employee>(obj);
                var result = await employee.Create(data);
                return Ok(new ApiResponse<Employee>
                {
                    Code = "201",
                    Status = "Created",
                    Message = "Data Saved",
                    Data = result
                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Data Not found",
                    Data = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeVM obj)
        {
            try
            {
                var data = mapper.Map<Employee>(obj);
                await employee.Update(data);
                return Ok(new ApiResponse<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Upadted",
                    Data = "Data Upadted"
                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Data Not found",
                    Data = ex.Message
                });
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await employee.Delete(id);
                return Ok(new ApiResponse<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Deleted",
                    Data = "Data Deleted"
                });
            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Data Not found",
                    Data = ex.Message
                });
            }
        }

    }
}
