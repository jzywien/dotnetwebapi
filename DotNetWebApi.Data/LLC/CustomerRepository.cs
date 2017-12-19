using DotNetWebApi.Common;
using DotNetWebApi.Data.Customers.LLC.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebApi.Data.Customers.LLC
{
    public interface ICustomerRepository
    {
        Customer GetById(Guid guid);
        Task<IEnumerable<Customer>> GetAll();
        Customer Add(Customer customer);
    }

    public class CustomerRepository : ICustomerRepository, ICachingRepository
    {
        private List<Customer> _customers = new List<Customer>();
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await Task.Run(async () =>
            {
                await Task.Delay(500);
                // Test Exceptions being thrown in lower level code
                // should be handled in BaseController.HandleException
                // throw new CustomException("Exception!");
                return _customers;
            });

            return customers;
        }

        public Customer GetById(Guid guid)
        {
            return _customers.FirstOrDefault(c => c.Guid.Equals(guid));
        }

        public Customer Add(Customer customer)
        {
            if (!Guid.Empty.Equals(customer.Guid))
            {
                throw new CustomException("Customer Already Exists!");
            }

            Guid id = Guid.NewGuid();
            customer.Guid = id;
            _customers.Add(customer);
            return _customers.FirstOrDefault(c => c.Guid.Equals(id));
        }
    }
}