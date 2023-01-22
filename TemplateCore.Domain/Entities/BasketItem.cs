using System;
using System.Collections.Generic;
using System.Text;
using TemplateCore.Domain.Entities;

namespace TemplateCore.Domain.Dtos
{
    public class BasketItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public Category Category { get; set; }
    }
}
