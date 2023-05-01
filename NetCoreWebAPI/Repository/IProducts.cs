using NetCoreWebAPI.DTOs;
using NetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Repository
{
    public interface IProducts
    {
        Task<bool> CreateOrUpdateProduct(ProductsDTO products);

        Task<List<ProductsDTO>> GetAll();

        Task<Products> GetProductByID(int ProductID);
    }
}
