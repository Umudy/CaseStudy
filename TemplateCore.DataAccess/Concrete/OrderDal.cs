using TemplateCore.DataAccess.Abstract;
using TemplateCore.DataAccess.Concrete.Contexts;
using TemplateCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TemplateCore.Domain.Dtos;

namespace TemplateCore.DataAccess.Concrete
{
    public class OrderDal : EntityRepositoryBase<Invoice>, IOrderDal
    {
        public OrderDal(TemplateCoreContext dbContext) : base(dbContext)
        {
        }
         
    }
}
