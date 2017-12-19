using DotNetWebApi.Common;
using DotNetWebApi.Customers.Delegates.ViewModels;
using System.Collections.Generic;
using AutoMapper;
using DotNetWebApi.Common.Extensions;
using DotNetWebApi.Business.Customers.HLC.Models;

namespace DotNetWebApi.Customers.Delegates.Mappers
{
    public class AddressDelegateMapper : CustomMapper<AddressViewModel, AddressModel>
    {
        public override void ExtendSourceToTargetMapping(IMappingExpression<AddressModel, AddressViewModel> map)
        {
            map.MapMember(t => t.FullStreetAddress, s => string.Join(", ", new List<string> { s.Street, s.City, s.State }));
        }

        public override void ExtendTargetToSourceMapping(IMappingExpression<AddressViewModel, AddressModel> map)
        {
        }
    }
}