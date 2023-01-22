using TemplateCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateCore.DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Invoice>
    { 
    }
}
