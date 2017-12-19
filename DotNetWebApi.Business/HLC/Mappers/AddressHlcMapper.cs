using DotNetWebApi.Common;
using AutoMapper;
using DotNetWebApi.Business.Customers.HLC.Models;
using DotNetWebApi.Data.Customers.LLC.DataModels;

namespace DotNetWebApi.Business.Customers.HLC.Mappers
{
    public class AddressHlcMapper : CustomMapper<AddressModel, Address>
    {
        public override void ExtendSourceToTargetMapping(IMappingExpression<Address, AddressModel> map)
        {
        }

        public override void ExtendTargetToSourceMapping(IMappingExpression<AddressModel, Address> map)
        {
        }
    }
}