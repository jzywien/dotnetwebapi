using DotNetWebApi.Common;
using DotNetWebApi.Customers.Delegates;
using DotNetWebApi.Customers.Delegates.ViewModels;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DotNetWebApi.Controllers
{
    [RoutePrefix("api")]
    public class CustomersController : BaseController
    {
        private readonly ICustomerDelegate _customerDelegate;

        public CustomersController(ICustomerDelegate customerDelegate)
        {
            _customerDelegate = customerDelegate;
        }

        /// <summary>
        /// This is the summary of what the API does. <strong>Returns an array of Customers</strong>
        /// </summary>
        /// <remarks>
        /// These are the implementation details of the API call. <strong>It can also contain html tags</strong>
        /// </remarks>
        [Route("customers")]
        [SwaggerOperation("GetAllCustomers")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<CustomerViewModel>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "See Error Response", Type = typeof(CustomError))]
        public Task<IHttpActionResult> GetAllCustomers()
        {
            return TryAsync(() => _customerDelegate.GetAllCustomers());
        }

        /// <summary>
        /// This Method returns a Customer and his Address (if applicable). Under the hood it uses entity framework to call a stored procedure.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("customers/{id}")]
        [SwaggerOperation("GetCustomerById")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CustomerViewModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "See Error Response", Type = typeof(CustomError))]
        public IHttpActionResult GetCustomerById(Guid id)
        {
            return Try(() => _customerDelegate.GetCustomerById(id));
        }

        /// <summary>
        /// Insert a new customer into the database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("customers")]
        [SwaggerOperation("AddCustomer")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CustomerViewModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "See Error Response", Type = typeof(CustomError))]
        public IHttpActionResult AddCustomer([FromBody] CustomerViewModel customer)
        {
            return Try(() => _customerDelegate.AddCustomer(customer));
        }
    }
}