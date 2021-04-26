﻿using L5T2UnitOfWork.Interfaces;
using L5T2UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace L5T2UnitOfWork.Repositories
{
    public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }
    }
}