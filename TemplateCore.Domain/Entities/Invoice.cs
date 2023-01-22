
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using TemplateCore.Domain.Dtos;

namespace TemplateCore.Domain.Entities
{

    public class Invoice : IEntity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public decimal TotalPrice { get; set; }
        public Discount Discount { get; set; }

        public decimal DiscountedOverallPrice => TotalPrice - Discount.Amount;

        public Customer Customer { get; set; }
    }
}
