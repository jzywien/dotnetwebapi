using AutoMapper;
using DotNetWebApi.Business.Customers.HLC.Models;
using DotNetWebApi.Data.Customers.LLC;
using DotNetWebApi.Data.Customers.LLC.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetWebApi.Business.Customers.HLC
{
    public interface ICustomerHlc
    {
        CustomerModel GetCustomer(Guid guid);
        Task<IEnumerable<CustomerModel>> GetCustomers();
        CustomerModel AddCustomer(CustomerModel model);
    }
    public class CustomerHlc : ICustomerHlc
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerHlc(
            ICustomerRepository customerRepository,
            IMapper mapper
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public CustomerModel AddCustomer(CustomerModel model)
        {
            Customer customer = _mapper.Map<Customer>(model);
            Customer newCustomer = _customerRepository.Add(customer);
            var result = _mapper.Map<CustomerModel>(newCustomer);
            return result;
        }

        public CustomerModel GetCustomer(Guid guid)
        {
            Customer customer = _customerRepository.GetById(guid);
            var result = _mapper.Map<CustomerModel>(customer);
            return result;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAll();
            var results = _mapper.Map<IEnumerable<CustomerModel>>(customers);
            return results;
        }
    }
}