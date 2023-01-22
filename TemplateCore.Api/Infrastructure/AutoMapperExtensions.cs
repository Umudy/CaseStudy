using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateCore.Api.Infrastructure
{
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> configure)
        {
            var mappingConfig = new MapperConfiguration(configure);

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
