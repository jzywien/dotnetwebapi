using AutoMapper;
using DotNetWebApi.Business.Customers.HLC;
using DotNetWebApi.Business.Customers.HLC.Models;
using DotNetWebApi.Customers.Delegates.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetWebApi.Customers.Delegates
{
    public interface ICustomerDelegate
    {
        CustomerViewModel AddCustomer(CustomerViewModel customer);
        Task<IEnumerable<CustomerViewModel>> GetAllCustomers();
        CustomerViewModel GetCustomerById(Guid id);
    }

    public class CustomerDelegate : ICustomerDelegate
    {
        private readonly ICustomerHlc _customerService;
        private readonly IMapper _mapper;

        public CustomerDelegate(ICustomerHlc customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public CustomerViewModel AddCustomer(CustomerViewModel customer)
        {
            CustomerModel customerModel = _mapper.Map<CustomerModel>(customer);
            var newCustomer = _customerService.AddCustomer(customerModel);
            var result = _mapper.Map<CustomerViewModel>(newCustomer);
            return result;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomers()
        {
            IEnumerable<CustomerModel> customers = await _customerService.GetCustomers();
            var results = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return results;
        }

        public CustomerViewModel GetCustomerById(Guid id)
        {
            CustomerModel customer = _customerService.GetCustomer(id);
            var result = _mapper.Map<CustomerViewModel>(customer);
            return result;
        }

    }
}