using AzureFuncRestApi.Data;
using AzureFuncRestApi.Interfaces;
using AzureFuncRestApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFuncRestApi.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
