﻿using CustomerManager.Core.Data;
using CustomerManager.Core.Entities;

namespace CustomerManager.Core.Repositories.Impl
{
    public sealed class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        #region Constructors

        public CustomerRepository(IDbContext dbContext)
            : base(dbContext)
        { }

        #endregion
    }
}
