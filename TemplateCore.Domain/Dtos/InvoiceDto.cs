using TemplateCore.Domain.Entities;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace TemplateCore.Domain.Dtos
{

    public class InvoiceDto
    {
        public InvoiceDto()
        {
            this.Discount = new DiscountDto();
            this.Customer = new CustomerDto();
        }
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public decimal TotalPrice { get; set; }
        public DiscountDto Discount { get; set; }

        public decimal DiscountedOverallPrice => TotalPrice - Discount.Amount ;

        public CustomerDto Customer { get; set; }

    }
}
