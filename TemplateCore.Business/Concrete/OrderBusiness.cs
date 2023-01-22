using AutoMapper;
using TemplateCore.Business.Abstract;
using TemplateCore.DataAccess.Abstract;
using TemplateCore.Domain.Dtos;
//using TemplateCore.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.InteropServices.ComTypes;
using TemplateCore.Domain.Entities;

namespace TemplateCore.Business.Concrete
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderDal _orderDal;   
        private readonly IMapper _mapper;
       
        public OrderBusiness(IOrderDal orderDal, IMapper mapper)
        {
            _orderDal = orderDal;         
            _mapper = mapper;
   
        }

        public InvoiceDto GetById(int invoiceId)
        {

            var data = _orderDal.Entity
            .Include(t => t.Discount)
            .Include(t => t.Customer)
            .Include(b => b.Items)      
            .SingleOrDefault(y => y.Id == invoiceId);

            return _mapper.Map<InvoiceDto>(data);
        }


        public InvoiceDto Insert(InvoiceDto invoiceDto)
        {
          
            var entity = _mapper.Map<Invoice>(invoiceDto);

            var added = _orderDal.Insert(entity);  //insert entity to DB in case DB connection

            return _mapper.Map<InvoiceDto>(added);
        }

      
    }
}
