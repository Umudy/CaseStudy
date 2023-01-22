using AutoMapper;
using TemplateCore.Domain.Dtos;
using TemplateCore.Domain.Entities;

namespace TemplateCore.Domain.AutoMapper
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            SourceMemberNamingConvention = new PascalCaseNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<InvoiceDto, Invoice>();
            
         
        }
    }
}
