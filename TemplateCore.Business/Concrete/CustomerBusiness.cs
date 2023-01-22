using AutoMapper;
using TemplateCore.Business.Abstract;
using TemplateCore.DataAccess.Abstract;
using TemplateCore.Domain.Dtos;

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.InteropServices.ComTypes;

namespace TemplateCore.Business.Concrete
{
    public class CustomerBusiness : ICustomer
    {
        

        public List<CustomerDto> GetCustomers()
        {
            return CreateCustomers();
        }

        private List<CustomerDto> CreateCustomers()
        {
            var customers = new List<CustomerDto>
             {
              new CustomerDto{Id=1,Name="Alexander",CustomerProperty= new string[] { "employee", "affiliate" }  },
              new CustomerDto{Id=2,Name="Mike",CustomerProperty= new string[] { "affiliate", "customerovertwoyears" }  },
              new CustomerDto{Id=3,Name="Laura",CustomerProperty= new string[] { "customerovertwoyears" } },
              new CustomerDto{Id=4,Name="Kate",CustomerProperty= new string[] { "" }}
            };
           return customers;
        }
       
    }
}
