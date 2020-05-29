using JWT_SampleApp.DtoModels;
using JWT_SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JWT_SampleApp.Services
{
    public class ProductService
    {
        public ProductModel GetProductDetails(int productId)
        {
            try
            {
                ProductModel model = null;
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    model = dbContext.Products.Where(a => a.ProductId == productId).Select(a => new ProductModel
                    {
                        ProductId = a.ProductId,
                        Quantity = a.Quantity
                    }).FirstOrDefault();
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetProductDetails: " + ex.Message);
            }
        }
        public List<ProductModel> GetProductList()
        {
            try
            {
               
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                  return dbContext.Products.Select(a => new ProductModel
                    {
                        ProductId = a.ProductId,
                        Quantity = a.Quantity,
                        Category=a.Category,
                        Description=a.Description,
                        Price=a.Price,
                        Name=a.Name,
                        ProductImagePath=a.ProductImagePath
                    }).ToList();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetProductList: " + ex.Message);
            }
        }

        public int GetExistingProductQuantity(int productId)
        {
            using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
            {
                return dbContext.Products.Where(a => a.ProductId == productId).Select(a => a.Quantity).FirstOrDefault();
            }
        }
        public bool ReduceProductQuantity(int productId, int itemsCount)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var product = dbContext.Products.Where(a => a.ProductId == productId).FirstOrDefault();
                    if (product != null)
                    {
                        product.Quantity = product.Quantity - itemsCount;
                        dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ReduceProductQuantity: " + ex.Message);
            }
        }
        public bool AddProductQuantity(int productId, int itemsCount)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var product = dbContext.Products.Where(a => a.ProductId == productId).FirstOrDefault();
                    if (product != null)
                    {
                        product.Quantity = product.Quantity + itemsCount;
                        dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddProductQuantity: " + ex.Message);
            }
        }
    }
}