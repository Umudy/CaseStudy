using TemplateCore.Domain.Dtos;

using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateCore.Business.Abstract
{
    public interface IBasket
    {

        List<BasketDto> GetBaskets();

     
    }
}
