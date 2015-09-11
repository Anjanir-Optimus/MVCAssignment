using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1.DAL.Repository
{
    public class GenericRepository : IGenericRepository
    {

        private Optimus_TradeEntities ObjEntity = null;

        public GenericRepository()
        {
            this.ObjEntity = new Optimus_TradeEntities();
        }
        /// <summary>
        /// Get the order Full details by customer id
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public List<OrderDetails> GetOrderFullDetails()
        {
            try
            {



                var resDetails = ObjEntity.ClientDetails.Join(ObjEntity.ContractDetails, x => x.ID, y => y.Customer_ID, (x, y) => new { x, y }).
                        Join(ObjEntity.OrderItemDetails, orderDetails => orderDetails.y.ID, orderItemDetails => orderItemDetails.Order_ID,
                        (orderDetails, orderItemDetails) => new { orderDetails, orderItemDetails }).Join
                        (ObjEntity.ProductDetails, orderFullDetails => orderFullDetails.orderItemDetails.Product_ID, productDetails => productDetails.ID,
                        (orderFullDetails, productDetails) => new { orderFullDetails, productDetails }).Where(x=>x.orderFullDetails.orderDetails.y.IsActive==true).Select(x => new OrderDetails
                        {
                            CustomerName = x.orderFullDetails.orderDetails.x.Name,
                            Email = x.orderFullDetails.orderDetails.x.Email,
                            Number = x.orderFullDetails.orderDetails.x.Number,
                            ShippingAddress = x.orderFullDetails.orderDetails.x.ShippingAddress,
                            BillingAddress = x.orderFullDetails.orderDetails.x.BillingAddress,
                            CustomerID = x.orderFullDetails.orderDetails.x.ID,
                            OrderID = x.orderFullDetails.orderDetails.y.ID,
                            OrderAmount = x.orderFullDetails.orderDetails.y.OrderAmount,
                            Discount = x.orderFullDetails.orderDetails.y.Discount,
                            Tax = x.orderFullDetails.orderDetails.y.Tax,
                            Quantity = x.orderFullDetails.orderDetails.y.Quantity,
                            NetAmount = x.orderFullDetails.orderDetails.y.NetAmount,
                            ProductID = x.orderFullDetails.orderItemDetails.ProductDetail.ID,
                            ProductName = x.productDetails.Name,
                            Stockleft = x.productDetails.StockLeft,
                            OrderItemID = x.orderFullDetails.orderItemDetails.ID,
                            TransactionDate = x.orderFullDetails.orderDetails.y.TransactionDate

                        }).ToList();

                return resDetails;
            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return null;
            }
        }
        public List<CustomerDetails> GetClientDetails()
        {
            try
            {

                var customerDetails = ObjEntity.ClientDetails.Select(x => new CustomerDetails
                {
                    CustomerName = x.Name,
                    Number = x.Number,
                    BillingAddress = x.BillingAddress,
                    ShippingAddress =
                    x.ShippingAddress,
                    Email = x.Email,
                    CustomerID = x.ID
                }).ToList();

                return customerDetails;
            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return null;
            }

        }

        public List<Product> GetProductData()
        {
            try
            {
                var ObjEntity = new Optimus_TradeEntities();
                var productData = ObjEntity.ProductDetails.Select(x => new Product { Name = x.Name, ID = x.ID }).ToList();
                return productData;
            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return null;
            }

        }

        /// <summary>
        /// update the order data
        /// </summary>
        /// <param name="model"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public bool UpdateDetails(OrderDetails model, int productID)
        {
            try
            {
                var objCustomerDetails = ObjEntity.ClientDetails.Where(x => x.ID == model.CustomerID).FirstOrDefault();


                objCustomerDetails.Name = model.CustomerName;
                objCustomerDetails.Email = model.Email;
                objCustomerDetails.BillingAddress = model.BillingAddress;
                objCustomerDetails.ShippingAddress = model.ShippingAddress;
                objCustomerDetails.Number = model.Number;
                

                var objContractDetail = ObjEntity.ContractDetails.Where(x => x.ID == model.OrderID).FirstOrDefault();

                objContractDetail.NetAmount = model.NetAmount;
                objContractDetail.Quantity = model.Quantity;
                objContractDetail.OrderAmount = model.OrderAmount;
                objContractDetail.Tax = model.Tax;
                objContractDetail.Discount = model.Discount;


                var OrderItemDetails = ObjEntity.OrderItemDetails.Where(x => x.ID == model.OrderID).FirstOrDefault();
                OrderItemDetails.Rate = model.Rate;
                OrderItemDetails.Quantity = model.Quantity.Value;
                OrderItemDetails.Amount = model.Amount;
                
                ObjEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return false;
            }
        }
        /// <summary>
        /// delete the order data according to the order no
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool DeleteOrderDetails(int orderNo)
        {
            var result = ObjEntity.ContractDetails.Where(x => x.ID == orderNo).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
                ObjEntity.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// add the 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public bool AddCustomer(OrderDetails model)
        {
            try
            {
                var objCustomerDetails = new ClientDetail();

                objCustomerDetails.Name = model.CustomerName;
                objCustomerDetails.Email = model.Email;
                objCustomerDetails.BillingAddress = model.BillingAddress;
                objCustomerDetails.ShippingAddress = model.ShippingAddress;
                objCustomerDetails.Number = model.Number;
                ObjEntity.ClientDetails.Add(objCustomerDetails);
                ObjEntity.SaveChanges();

                var objContractDetail = new ContractDetail();
                objContractDetail.NetAmount = model.NetAmount;
                objContractDetail.Quantity = model.Quantity;
                objContractDetail.OrderAmount = model.OrderAmount;
                objContractDetail.Tax = model.Tax;
                objContractDetail.Discount = model.Discount;
                objContractDetail.IsActive = true;
                objContractDetail.TransactionDate = DateTime.Now;
                objContractDetail.Customer_ID = objCustomerDetails.ID; ;
                ObjEntity.ContractDetails.Add(objContractDetail);
               int OrderNo= ObjEntity.SaveChanges();
                var OrderItemDetails = new OrderItemDetail();
                OrderItemDetails.Rate = model.Rate;
                OrderItemDetails.Order_ID = objContractDetail.ID;
                OrderItemDetails.Product_ID = 4;
                OrderItemDetails.Quantity = model.Quantity.Value;
                
                OrderItemDetails.Amount = model.Amount;
                ObjEntity.OrderItemDetails.Add(OrderItemDetails);
                ObjEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return false;
            }
        }


    }

    
}