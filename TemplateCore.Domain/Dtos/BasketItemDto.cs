using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateCore.Domain.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public CategoryDto Category { get; set; }
    }
}
