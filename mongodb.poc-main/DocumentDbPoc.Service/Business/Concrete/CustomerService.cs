using DocumentDbPoc.Domain.Business;
using DocumentDbPoc.Service.Business.Interface;
using DocumentDbPoc.Service.DocumentDbServices;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDbPoc.Service.Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(MongoDbService mongoDbService)
        {
            _customers = mongoDbService.Database.GetCollection<Customer>("customers");
        }

        public bool CreateCustomer(Customer customer)
        {
            _customers.InsertOne(customer);
            return true;
        }

        public bool DeleteCustomer(string id)
        {
            _customers.DeleteOne(c => c.Id == id);
            return true;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers.Find(FilterDefinition<Customer>.Empty).ToList();
        }

        public Customer GetCustomer(string id)
        {
            return _customers.Find(c => c.Id == id.ToString()).FirstOrDefault();
        }

        public bool UpdateCustomer(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, customer.Id);
            var update = Builders<Customer>.Update.Set(x => x, customer);
            _customers.UpdateOne(filter, update);
            return true;
        }
    }
}
