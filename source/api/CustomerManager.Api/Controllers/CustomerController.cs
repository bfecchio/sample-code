﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerManager.Core.Entities;
using CustomerManager.Core.Services;
using FluentValidation;

namespace CustomerManager.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Private Read-Only Fields

        private readonly ICustomerService _customerService;

        #endregion

        #region Constructors

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _customerService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _customerService.Get(id);
            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Customer customer)
        {
            if (customer == null)
                return new BadRequestResult();
            
            try
            {
                await _customerService.Create(customer);
                return new OkObjectResult(customer);
            }
            catch (ValidationException) { }

            return new BadRequestResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Customer customer)
        {
            var entity = await _customerService.Get(id);
            if (entity == null) return NotFound();
            
            try
            {
                await _customerService.Update(id, customer);
                return new OkObjectResult(customer);
            }
            catch (ValidationException) { }

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var entity = await _customerService.Get(id);
            if (entity == null) return NotFound();
            
            await _customerService.Delete(id);

            return Ok();
        }

        #endregion
    }
}