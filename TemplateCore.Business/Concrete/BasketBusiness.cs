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
    public class BasketBusiness : IBasket
    {
       
        public List<BasketDto> GetBaskets()
        {
            return CreateBaskets();
        }

        private List<BasketDto> CreateBaskets()
        {
            var baskets = new List<BasketDto>
            {
             new BasketDto {Id=100, Items = new List<BasketItemDto> {
                  new BasketItemDto { Id =200,Name= "TV", UnitPrice=100, Quantity=1, Category = new CategoryDto{Id=300, Name="Electronics" }  },
                  new BasketItemDto { Id =201,Name= "Apple", UnitPrice=10, Quantity=1, Category = new CategoryDto{Id=301, Name="Grocery" }  },
                  new BasketItemDto { Id =202,Name= "Chair", UnitPrice=20, Quantity=1, Category = new CategoryDto{Id=302, Name="Furniture" }  },
                  new BasketItemDto { Id =203,Name= "Banana", UnitPrice=2, Quantity=1, Category = new CategoryDto{Id=301, Name="Grocery" }  }
             }},
              new BasketDto {Id=101, Items = new List<BasketItemDto> {
                  new BasketItemDto { Id =200,Name= "TV", UnitPrice=100, Quantity=2, Category = new CategoryDto{Id=300, Name="Electronics" }  },
                  new BasketItemDto { Id =204,Name= "Computer", UnitPrice=200, Quantity=1, Category = new CategoryDto{Id=300, Name="Electronics" }  },
                  new BasketItemDto { Id =205,Name= "Carpet", UnitPrice=50, Quantity=1, Category = new CategoryDto{Id=302, Name="Furniture" }  },
                  new BasketItemDto { Id =206,Name= "Pen", UnitPrice=5, Quantity=10, Category = new CategoryDto{Id=303, Name="Stationery" }  }
             }},

            };
            return baskets;
        }
      
    }
}
