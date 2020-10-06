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
    public class CustomersController : ControllerBase
    {
        CustomerRepository customerRepository = new CustomerRepository();

        [HttpGet]
        public async Task<IEnumerable<CustomerInfo>> GetCustomers()
        {
            return await customerRepository.GetCustomers();
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<CustomerInfo> GetCustomerById(int id)
        {
            return await customerRepository.GetCustomerById(id);
        }
        [HttpPost]
        public async Task<bool> CreateCustomer(CustomerInfo customer)
        {
            return await customerRepository.CreateCustomer(customer);
        }
        [HttpPut]
        [Route("updateCustomer")]
        public async Task<bool> UpdateCustomer(CustomerInfo updatedCustomer)
        {
            return await customerRepository.UpdateCustomer(updatedCustomer);
        }
        [HttpDelete]
        [Route("DeleteById/{id}")]
        public async Task<bool> DeleteCustomer(int id)
        {
            return await customerRepository.DeleteCustomer(id);
        }
    }
}
