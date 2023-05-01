using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreWebAPI.DTOs;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.DataAccess
{
    public class ProductDB : IProducts
    {
        private readonly ApplicationDataContext _dbcontext;
        private readonly IMapper _mapper;
        public ProductDB(ApplicationDataContext dbcontext, IMapper mapper )
        {
            this._dbcontext = dbcontext;
            this._mapper = mapper;
        }

        public async Task<bool> CreateOrUpdateProduct(ProductsDTO products)
        {
            var dataCreation = _mapper.Map<Products>(products);

            dataCreation.CreatedTime = DateTime.Now;
            if (products.ProductId > 0)
            {

                dataCreation.ModifiedTime = DateTime.Now;
                _dbcontext.Products.Update(dataCreation);

                
            }
            else
            {
                 await _dbcontext.Products.AddAsync(dataCreation);
            }
            

            
             await _dbcontext.SaveChangesAsync();

            return true;

        }

        public async Task<List<ProductsDTO>> GetAll()
        {

            List<Products> objList =   await _dbcontext.Products.ToListAsync();

            if(objList.Count == 0)
                return null;

            return _mapper.Map<List<ProductsDTO>>(objList);
           

        }

        public async Task<Products> GetProductByID(int ProductID)
        {
            var getProductByID = await _dbcontext.Products.Where(x => x.ProductId == ProductID).SingleOrDefaultAsync();

            //ProductsDTO dataProd = _mapper.Map<ProductsDTO>(getProductByID);
            return getProductByID;
        }
    }
}
