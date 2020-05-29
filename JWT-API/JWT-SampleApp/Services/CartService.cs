using JWT_SampleApp.DtoModels;
using JWT_SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JWT_SampleApp.Services
{
    public class ShoppingCartService
    {
        public ShoppingCartModel GetMyShoppingCartItems(int userId)
        {
            try
            {
                ShoppingCartModel model = null;
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var ShoppingCart = dbContext.ShoppingCart.Where(a => a.UserId == userId).FirstOrDefault();
                    if (ShoppingCart != null)
                    {
                        model = new ShoppingCartModel();
                        model.ShoppingCartId = ShoppingCart.ShoppingCartId;
                       
                        model.CreatedOn = ShoppingCart.CreatedOn;
                        model.UpdatedOn = ShoppingCart.UpdatedOn;
                        model.UserId = ShoppingCart.UserId;

                        model.CartItemsList = GetShoppingCartDetails(model.ShoppingCartId);
                        model.SubTotal = GetSubTotalValue(model.CartItemsList);
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching ShoppingCart:" + ex.Message);
            }
        }

        public bool AddToCart(int userId, int productId, int qty)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    ProductService productService = new ProductService();
                    int existingQty = productService.GetExistingProductQuantity(productId);

                    if (qty > existingQty)
                    {
                        throw new Exception("Not adequate quantity available.Please reduce quantity");
                    }

                    int cartId = GetUserCart(userId);
                    if (cartId == 0)
                    {
                        ShoppingCart cart = new ShoppingCart
                        {
                            UserId = userId,
                            CreatedOn = DateTime.Now,
                            UpdatedOn=DateTime.Now
                        };
                        dbContext.ShoppingCart.Add(cart);
                        cartId = cart.ShoppingCartId;

                    }
                    //bool isAddedToCartItem = AddCartItem(productId, qty, cartId);
                    CartItems _cartItem = CheckProductExistsIncart(productId, cartId);
                    if (_cartItem != null)
                    {
                        _cartItem.Quantity += 1;
                        //unitOfWork.Repository<CartItems>().Update(_cartItem);
                        dbContext.CartItems.Attach(_cartItem);
                        dbContext.Entry(_cartItem).State = EntityState.Modified;
                    }
                    else
                    {
                        CartItems cartItem = new CartItems
                        {
                            ProductId = productId,
                            Quantity = qty,
                            ShoppingCartId = cartId
                        };
                        dbContext.CartItems.Add(cartItem);

                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error on Add Cart Method:" + ex.Message);
            }
          
        }

        public bool DeleteCartItem(int userId, int cartItemId)
        {
            try
            {
                int cartId = GetUserCart(userId);
                if (cartId > 0)
                {
                    using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                    {
                        var cartItemList = dbContext.CartItems.Where(a => a.ShoppingCartId == cartId).ToList();
                        if (cartItemList != null && cartItemList.Count > 0)
                        {
                            var cartItem = dbContext.CartItems.Where(a => a.CartItemId == cartItemId).FirstOrDefault();
                            dbContext.CartItems.Remove(cartItem);
                        }
                        else
                        {

                            var cart = dbContext.ShoppingCart.Where(a => a.ShoppingCartId == cartId).FirstOrDefault();
                            dbContext.ShoppingCart.Remove(cart);
                        }
                        dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception("Error on DeleteCartItem Method:" + ex.Message);
            }
           
           
        }
        private List<CartItemsModel> GetShoppingCartDetails(int ShoppingCartId)
        {
            List<CartItemsModel> ShoppingCartDetailsModels = null;
            using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
            {
                var ShoppingCart = dbContext.CartItems.Where(a => a.ShoppingCartId == ShoppingCartId).ToList();
                if (ShoppingCart != null)
                {
                    ShoppingCartDetailsModels = new List<CartItemsModel>();
                    foreach (CartItems details in ShoppingCart)
                    {
                        CartItemsModel model = new CartItemsModel();
                        model.CartItemId = details.CartItemId;
                        model.ProductId = details.ProductId;
                        model.Quantity = details.Quantity;
                        model.Price = details.Product.Price;
                        model.Name = details.Product.Name;
                        model.ProductImagePath = details.Product.ProductImagePath;
                        ShoppingCartDetailsModels.Add(model);
                    }
                }
            }
            return ShoppingCartDetailsModels;
        }
        private decimal GetSubTotalValue(List<CartItemsModel> CartItemList)
        {
            decimal subTotal = 0;
            foreach (var item in CartItemList)
            {
                decimal val = item.Price * item.Quantity;
                subTotal += val;
            }
            return subTotal;
        }

        private int GetUserCart(int userId)
        {
            using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
            {
                return dbContext.ShoppingCart.Where(a => a.UserId == userId).Select(a => a.ShoppingCartId).FirstOrDefault();
            }
           
        }
        //private bool AddCartItem(int productId, int qty, int shoppingCartId, JWTSampleDbContext dbContext)
        //{
        //    CartItems _cartItem = CheckProductExistsIncart(productId, shoppingCartId);
        //    if (_cartItem != null)
        //    {
        //        _cartItem.Quantity += 1;
        //        //unitOfWork.Repository<CartItems>().Update(_cartItem);
        //        //dbContext.db.
        //    }
        //    else
        //    {
        //        CartItems cartItem = new CartItems
        //        {
        //            ProductId = Utilities.ConvertStringToInt(productId),
        //            Quantity = Utilities.ConvertStringToInt(qty),
        //            CreatedDate = DateTime.Now,
        //            CartID = cartId
        //        };

        //        unitOfWork.Repository<CartItems>().Insert(cartItem);
        //    }

        //    return true;
        //}
        private CartItems CheckProductExistsIncart(int productId, int cartId)
        {
            CartItems cartItem = null;
            try
            {
                if (productId > 0)
                {
                    using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                    {
                        cartItem= dbContext.CartItems.Where(a => a.ShoppingCartId == cartId &&a.ProductId== productId).FirstOrDefault();
                    }
                   
                }
                return cartItem;
            }
            catch (Exception ex)
            {
                return cartItem;
            }
        }

        //private bool DeleteCartItems(int cartId, int cartItemId)
        //{
           
           

        //}
    }
}