using DocumentDbPoc.Domain.Business;
using DocumentDbPoc.Service.Business.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentDbPoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomers", Name = "GetAllCustomers")]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [HttpGet("GetCustomer/{id}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(string id)
        {
            return Ok(_customerService.GetCustomer(id));
        }

        [HttpPost("CreateCustomer", Name = "CreateCustomer")]
        public ActionResult<Customer> Create([FromBody]Customer customer)
        {
            return Ok(_customerService.CreateCustomer(customer));
        }

        [HttpPut("UpdateCustomer", Name = "UpdateCustomer")]
        public ActionResult<Customer> Update([FromBody]Customer customer)
        {
            return Ok(_customerService.UpdateCustomer(customer));
        }

        [HttpDelete("DeleteCustomer/{id}", Name = "DeleteCustomer")]
        public ActionResult<Customer> Delete(string id)
        {
            return Ok(_customerService.DeleteCustomer(id));
        }
    }
}
