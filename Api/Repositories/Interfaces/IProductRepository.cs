using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Entities.Dto;
using Entities.Http;

namespace Api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetOneProduct(string ID);
        Task<bool> InsertProduct(Products products);
        Task<bool> UpdateProduct(Products products);
        Task<bool> DeleteProduct(string ID);
        Task<List<ProductDto>> SearchByID(ProductFilter filter);
    }
}