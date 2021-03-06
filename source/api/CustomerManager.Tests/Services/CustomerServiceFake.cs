﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CustomerManager.Core.Entities;
using CustomerManager.Core.Services;
using CustomerManager.Core.Validators;
using CustomerManager.Core.Validators.Impl;
using CustomerManager.Core.Enumeration.Customer;
using FluentValidation;

namespace CustomerManager.Tests.Services
{
    public class CustomerServiceFake : ICustomerService
    {
        #region Private Read-Only Fields

        private List<Customer> _customers;
        private ICustomerValidator _validator;

        #endregion

        #region Constructors

        public CustomerServiceFake()
        {
            this._validator = new CustomerValidator();

            this._customers = new List<Customer> {
                new Customer(){
                    Id = "5c7432b01d39ea2b383e6ac3",
                    Name = "Pessoa A",
                    Birthday = DateTime.Now.AddYears(-33),
                    DocumentId = "32.365.778-4",
                    SocialSecurityId = "334.457.685-40",
                    Phones = new CustomerPhone[]{
                        new CustomerPhone("(+5511) 99999-9999", PhoneType.CellPhone),
                        new CustomerPhone("(+5511) 5555-5555", PhoneType.Residential),
                    },
                    Addresses = new CustomerAddress[]{
                        new CustomerAddress("Rua A", AddressType.Commercial),
                        new CustomerAddress("Rua B", AddressType.Residential),
                    },
                    Facebook = "https://fb.com/pessoa-a",
                    LinkedIn = "https://linkedin.com/in/pessoa-a",
                    Twitter = "https://twitter.com/pessoa-a",
                    Instagram = "https://instagram.com/pessoa-a",
                    DateCreated = DateTime.Now                    
                },
                new Customer(){
                    Id = "6c8972b01d39ea2b383e6ab5",
                    Name = "Pessoa B",
                    Birthday = DateTime.Now.AddYears(-15),
                    DocumentId = "25.233.978-3",
                    SocialSecurityId = "451.339.254-20",
                    Phones = new CustomerPhone[]{
                        new CustomerPhone("(+5511) 98888-8888", PhoneType.CellPhone),
                        new CustomerPhone("(+5511) 5555-5555", PhoneType.Residential),
                    },
                    Addresses = new CustomerAddress[]{
                        new CustomerAddress("Avenida C", AddressType.Commercial),
                        new CustomerAddress("Alameda D", AddressType.Residential),
                    },
                    Facebook = "https://fb.com/pessoa-b",
                    LinkedIn = "https://linkedin.com/in/pessoa-b",
                    Twitter = "https://twitter.com/pessoa-b",
                    Instagram = "https://instagram.com/pessoa-b",
                    DateCreated = DateTime.Now.AddDays(-5),
                    DateUpdated = DateTime.Now.AddHours(-8) 
                }
            };
        }

        #endregion

        #region ICustomerRepository Members
       
        public async Task Create(Customer entity)
        {
            await _validator.ValidateAndThrowAsync(entity);            
            await Task.Run(() => {
                entity.Id = Guid.NewGuid().ToString().Replace("-", "");
                entity.DateCreated = DateTime.Now;

                _customers.Add(entity);
            });
        }

        public async Task<bool> Delete(string id)
        {
            return await Task.Run(() => {
                var exists = _customers.FirstOrDefault(x => x.Id.Equals(id));
                if (exists == null) return false;

                _customers = _customers.Where(x => !x.Id.Equals(id)).ToList();
                return true;
            });
        }

        public async Task<Customer> Get(string id)
        {
            return await Task.Run(() => _customers.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await Task.Run(() => _customers.ToArray());
        }

        public async Task<bool> Update(string id, Customer entity)
        {
            await _validator.ValidateAndThrowAsync(entity);

            return await Task.Run(() => {
                var exists = _customers.FirstOrDefault(x => x.Id.Equals(id));
                if (exists == null) return false;
                
                exists.Name = entity.Name;
                exists.Birthday = entity.Birthday;
                exists.DocumentId = entity.DocumentId;
                exists.SocialSecurityId = entity.SocialSecurityId;
                exists.Phones = entity.Phones;
                exists.Addresses = entity.Addresses;
                exists.Facebook = entity.Facebook;
                exists.LinkedIn = entity.LinkedIn;
                exists.Twitter = entity.Twitter;
                exists.Instagram = entity.Instagram;
                entity.DateUpdated = entity.DateUpdated;
                                
                return true;
            });
        }

        #endregion
    }
}

