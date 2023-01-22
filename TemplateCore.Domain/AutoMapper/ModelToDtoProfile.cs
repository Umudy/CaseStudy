using System.Collections.Generic;
using AutoMapper;
using TemplateCore.Domain.Dtos;
using TemplateCore.Domain.Entities;

namespace TemplateCore.Domain.AutoMapper
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            SourceMemberNamingConvention = new PascalCaseNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<Invoice, InvoiceDto>();


        }
    }
}
