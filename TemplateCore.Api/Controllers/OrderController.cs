using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateCore.Api.Infrastructure;
using TemplateCore.Business.Abstract;
using TemplateCore.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TemplateCore.Api.Controllers
{
    /// <summary>
    /// Ordering process
    /// </summary>
    public class OrderController : BaseApiController
    {   
         
        private CustomerDto _activeCustomer; 
        private readonly List<CustomerDto> _customers;
        private readonly List<BasketDto> _baskets;
        private readonly IOrderBusiness _orderBusiness;

        public OrderController(IOrderBusiness orderBusiness, ICustomer customers, IBasket baskets)
        {
            _orderBusiness = orderBusiness;
            _customers = customers.GetCustomers(); //create test customers
            _baskets = baskets.GetBaskets(); //create test baskets
        }

        /// <summary>
        /// Get Invoice by Id from DB in case of DB connection
        /// </summary>
        /// <param name="id">Invoice id value</param>
        /// <response code="200">Success.</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvoiceDto))]
        public ActionResult<InvoiceDto> Get(int id)
        {
            return Ok(_orderBusiness.GetById(id));
        }


        /// <summary>
        /// Create Invoice from a bill
        /// </summary>
        /// <param name="basketDto">Basket data</param>
        /// <response code="201">Successfully registered.</response>
        /// <response code="400">Incorrect billing information</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InvoiceDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public IActionResult Post([FromBody] BasketDto basketDto)
        {
            InvoiceDto invoice = new InvoiceDto();
            // determine Discount Percentage 
            invoice.Discount.Percentage = _activeCustomer.CustomerProperty.Any(z => z.Contains("employee")) ? 30 :
               _activeCustomer.CustomerProperty.Any(z => z.Contains("affiliate")) ? 10
               : _activeCustomer.CustomerProperty.Any(z => z.Contains("customerovertwoyears")) ? 5 : 0;
       
            invoice.Id = basketDto.Id;
            invoice.Items = basketDto.Items;
            invoice.TotalPrice = basketDto.Items.Select(x => x.UnitPrice * x.Quantity).Sum();
            //calculate Discount Amount
            invoice.Discount.Amount = basketDto.Items.Where(z => z.Category.Id != 301).Select(x => x.UnitPrice * x.Quantity).Sum() * (invoice.Discount.Percentage / 100) + (int)(invoice.TotalPrice / 100) * 5;
            invoice.Customer = _activeCustomer;

            var createdData = _orderBusiness.Insert(invoice); //Insert invoice to DB in case of DB connection 
            return CreatedAtAction(nameof(Get), new { id = createdData.Id }, createdData);

        }

        private InvoiceDto GetInvoiceUnitTest([FromBody] BasketDto basketDto)
        {
            InvoiceDto invoice = new InvoiceDto();
            // determine Discount Percentage 
            invoice.Discount.Percentage = _activeCustomer.CustomerProperty.Any(z => z.Contains("employee")) ? 30 : 
                _activeCustomer.CustomerProperty.Any(z => z.Contains("affiliate")) ? 10 
                : _activeCustomer.CustomerProperty.Any(z => z.Contains("customerovertwoyears")) ? 5 : 0;

            invoice.Id = basketDto.Id;
            invoice.Items = basketDto.Items;
            invoice.TotalPrice = basketDto.Items.Select(x => x.UnitPrice * x.Quantity).Sum();
            //calculate Discount Amount
            invoice.Discount.Amount =  basketDto.Items.Where(z => z.Category.Id != 301).Select(x => x.UnitPrice * x.Quantity).Sum() * (invoice.Discount.Percentage / 100) + (int)(invoice.TotalPrice / 100) * 5 ;
            invoice.Customer = _activeCustomer;
            
            return invoice;
        }

        /// <summary>
        /// Test all fake baskets on all fake customers
        /// </summary>
        /// <response code="200">Success.</response>
        /// <returns></returns>
        [HttpPost("test")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InvoiceDto>))]
        public ActionResult<List<InvoiceDto>> UnitTestPost()
        {
            var invoices = new List<InvoiceDto>();
            foreach (var customer in _customers) 
            {   
                _activeCustomer = customer;  // set active test customer
                foreach (var basket in _baskets) 
                {
                    invoices.Add(GetInvoiceUnitTest(basket)); //create invoices for all basket versions on active customer
                }
            }
         
            return Ok(invoices); 
        }
    }
}
