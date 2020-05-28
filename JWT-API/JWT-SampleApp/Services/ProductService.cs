using JWT_SampleApp.DtoModels;
using JWT_SampleApp.Models;
using System;
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
                        ProductId = a.ProductId
                    }).FirstOrDefault();
                }
                return model;
            }

            catch (Exception ex)
            {
                throw new Exception("Error in GetProductDetails: "+ex.Message);
            }
        }
    }
}