using DotNetWebApi.Common;
using DotNetWebApi.Customers.Delegates.ViewModels;
using System.Linq;
using AutoMapper;
using DotNetWebApi.Common.Extensions;
using DotNetWebApi.Business.Customers.HLC.Models;

namespace DotNetWebApi.Customers.Delegates.Mappers
{
    public class CustomerDelegateMapper : CustomMapper<CustomerViewModel, CustomerModel>
    {
        public override void ExtendSourceToTargetMapping(IMappingExpression<CustomerModel, CustomerViewModel> map)
        {
            map.MapMember(t => t.Name, s => s.Name.Full);
        }

        public override void ExtendTargetToSourceMapping(IMappingExpression<CustomerViewModel, CustomerModel> map)
        {
            map.MapMember(s => s.Name, t => new NameModel {
                First = t.Name.Split(' ').ElementAt(0),
                Last = t.Name.Split(' ').ElementAt(1)
            });
        }
    }
}