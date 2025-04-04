using DocumentDbPoc.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDbPoc.Service.Business.Interface
{
    public interface ICustomerService
    {
        public Customer GetCustomer(string id);
        public List<Customer> GetAllCustomers();
        public bool CreateCustomer(Customer customer);
        public bool UpdateCustomer(Customer customer);
        public bool DeleteCustomer(string id);
    }
}
