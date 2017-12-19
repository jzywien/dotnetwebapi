using DotNetWebApi.Common;
using AutoMapper;
using DotNetWebApi.Business.Customers.HLC.Models;
using DotNetWebApi.Data.Customers.LLC.DataModels;
using DotNetWebApi.Common.Extensions;

namespace DotNetWebApi.Business.Customers.HLC.Mappers
{
    public class CustomerHlcMapper : CustomMapper<CustomerModel, Customer>
    {
        public override void ExtendSourceToTargetMapping(IMappingExpression<Customer, CustomerModel> map)
        {
            map.MapMember(t => t.Name, s => new NameModel
            {
                First = s.FirstName,
                Last = s.LastName
            });
        }

        public override void ExtendTargetToSourceMapping(IMappingExpression<CustomerModel, Customer> map)
        {
            map.MapMember(s => s.FirstName, t => t.Name.First);
            map.MapMember(s => s.LastName, t => t.Name.Last);
        }
    }
}