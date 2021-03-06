﻿using System.Threading.Tasks;
using System.Collections.Generic;
using CustomerManager.Core.Entities;
using FluentValidation;

namespace CustomerManager.Core.Services
{
    public interface IGenericService<TEntity, TValidator>
        where TEntity : class, IEntity
        where TValidator : class, IValidator<TEntity>
    {
        #region IGenericService Members

        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Create(TEntity entity);
        Task<bool> Update(string id, TEntity entity);
        Task<bool> Delete(string id);

        #endregion
    }
}
