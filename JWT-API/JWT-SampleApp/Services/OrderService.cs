using JWT_SampleApp.DtoModels.Order;
using JWT_SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JWT_SampleApp.Services
{
    public class OrderService
    {
        public bool CreateOrder(OrderModel model)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    ProductService productService = new ProductService();

                    Order order = new Order();
                    order.Address1 = model.Address1;
                    order.Address2 = model.Address2;
                    order.City = model.City;
                    order.Country = model.Country;
                    order.CreatedOn = DateTime.Now;
                    order.OrderStatus = OrderStatus.Success.ToString();
                    order.PhoneNumber = model.PhoneNumber;
                    order.PostalCode = model.PostalCode;
                    order.UpdatedOn = DateTime.Now;
                    order.UserId = model.UserId;
                  
                    dbContext.Orders.Add(order);

                    foreach (OrderDetailsModel orderDetails in model.OrderDetailsList)
                    {
                        int existingQty = productService.GetExistingProductQuantity(orderDetails.ProductId);

                        if (orderDetails.Quantity > existingQty)
                        {
                            throw new Exception("Not adequate quantity available.Please reduce quantity");
                        }
                        OrderDetails details = new OrderDetails();
                        details.OrderId = order.OrderId;
                        details.ProductId = orderDetails.ProductId;
                        details.Quantity = orderDetails.Quantity;

                        dbContext.OrderDetails.Add(details);

                        //Reduce product quantity
                        var product = dbContext.Products.Where(a => a.ProductId == orderDetails.ProductId).FirstOrDefault();
                        if (product != null)
                        {
                            product.Quantity = product.Quantity - orderDetails.Quantity;
                            order.TotalOrderPrice = order.TotalOrderPrice + (orderDetails.Quantity * product.Price);
                        }
                    }

                    //Check CartId >0 if yes,this order is placing from cart;then delete cart items

                    if (model.CartId > 0)
                    {
                        var cartItemList = dbContext.CartItems.Where(a => a.ShoppingCartId == model.CartId).ToList();
                        if (cartItemList != null && cartItemList.Count > 0)
                        {
                            foreach (CartItems items in cartItemList)
                            {
                                dbContext.CartItems.Remove(items);
                            }
                        }
                        var cartEntry = dbContext.ShoppingCart.Where(a => a.ShoppingCartId == model.CartId).FirstOrDefault();
                        if (cartEntry != null)
                        {
                            dbContext.ShoppingCart.Remove(cartEntry);
                        }
                    }
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error on create order Method:" + ex.Message);
            }
        }

        public List<OrderModel> ViewAllOrders(int userId)
        {
            try
            {
                List<OrderModel> model = null;
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var orders = dbContext.Orders.Where(a => a.UserId == userId).OrderByDescending(a => a.CreatedOn).ToList();
                    if (orders != null && orders.Count > 0)
                    {
                        model = new List<OrderModel>();

                        foreach (Order order in orders)
                        {
                            OrderModel orderModel = new OrderModel();
                            orderModel.OrderId = order.OrderId;
                            orderModel.UserId = order.UserId;
                            orderModel.Address1 = order.Address1;
                            orderModel.Address2 = order.Address2;
                            orderModel.City = order.City;
                            orderModel.Country = order.Country;
                            orderModel.CreatedOn = order.CreatedOn;
                            orderModel.OrderStatus = order.OrderStatus;
                            orderModel.PhoneNumber = order.PhoneNumber;
                            orderModel.PostalCode = order.PostalCode;
                            orderModel.UpdatedOn = order.UpdatedOn;
                            orderModel.OrderDetailsList = GetOrderDetailsList(order.OrderId);
                            orderModel.TotalOrderPrice = GetSubTotalValue(orderModel.OrderDetailsList);
                            model.Add(orderModel);
                        }
                        //model.CartItemsList = GetShoppingCartDetails(model.ShoppingCartId);
                        //model.SubTotal = GetSubTotalValue(model.CartItemsList);
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching ShoppingCart:" + ex.Message);
            }
        }

        public bool CancelOrder(int orderId)
        {
            try
            {
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var order = dbContext.Orders.Where(a => a.OrderId == orderId).FirstOrDefault();
                    if (order != null)
                    {
                        order.OrderStatus = OrderStatus.Cancelled.ToString();

                        //Move products to products table
                        var orderDetailsList = GetOrderDetailsList(orderId);
                        if(orderDetailsList!=null& orderDetailsList.Count > 0)
                        {
                            foreach (var item in orderDetailsList)
                            {
                                var product = dbContext.Products.Where(a => a.ProductId == item.ProductId).FirstOrDefault();
                                if (product != null)
                                {
                                    product.Quantity = product.Quantity + item.Quantity;
                                }
                            }
                        }
                        dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CancelOrder:" + ex.Message);
            }
        }


        private List<OrderDetailsModel> GetOrderDetailsList(int orderId)
        {
            try
            {
                List<OrderDetailsModel> model = null;
                using (JWTSampleDbContext dbContext = new JWTSampleDbContext())
                {
                    var orders = dbContext.OrderDetails.Where(a => a.OrderId == orderId).ToList();
                    if (orders != null && orders.Count > 0)
                    {
                        //model = new List<OrderDetailsModel>();
                        model = orders.Select(a => new OrderDetailsModel
                        {
                            OrderDetailsId = a.OrderDetailsId,
                            Name = a.Product.Name,
                            Price = a.Product.Price,
                            Quantity=a.Quantity,
                            ProductId = a.ProductId,
                            ProductImagePath = a.Product.ProductImagePath,
                        }).ToList();
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching GetOrderDetailsList:" + ex.Message);
            }
        }


        private decimal GetSubTotalValue(List<OrderDetailsModel> orders)
        {
            decimal subTotal = 0;
            foreach (var item in orders)
            {
                decimal val = item.Price * item.Quantity;
                subTotal += val;
            }
            return subTotal;
        }
    }
}