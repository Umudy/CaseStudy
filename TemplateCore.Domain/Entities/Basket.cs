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

    public class Basket : IEntity
    {
        public int Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    }
}
