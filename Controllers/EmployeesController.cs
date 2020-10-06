using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperApi.Models;
using DapperApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        EmployeeRepository employeeRespository = new EmployeeRepository();

        [HttpGet]
        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            return await employeeRespository.GetEmployees();
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<EmployeeModel> GetEmployee(int id)
        {
            return await employeeRespository.SingleEmployee(id);
        }

        [HttpPost]
        public async Task<bool> CreateEmployee(EmployeeInfo employee)
        {
            if (ModelState.IsValid)
            {
                return await employeeRespository.CreateEmployee(employee);
            }
            return true;
        }
        [HttpPut]
        [Route("updateEmp")]
        public async Task<bool> EditEmployee(EmployeeModel employee)
        {
            return await employeeRespository.EditEmployee(employee);
        }

        [HttpDelete]
        [Route("DeleteById/{id}")]
        public async Task<bool> DeleteEmployee(int id)
        {
            return await employeeRespository.DeleteEmployee(id);
        }
    }
}
