using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1.DAL.Repository
{
    public interface IGenericRepository
    {
        /// <summary>
        /// delclare the interface that will implement
        /// </summary>
        /// <returns></returns>
        List<OrderDetails> GetOrderFullDetails(); // get full order details
        List<CustomerDetails> GetClientDetails(); // get the all the clients
        List<Product> GetProductData(); // get the all product from the master tbale
        bool UpdateDetails(OrderDetails objOrderDetail, int product_ID); // update the orderd details on the basis of product number
        bool DeleteOrderDetails(int orderNumber); // delete the order from the order number

        bool AddCustomer(OrderDetails objOrderDetails); // add the new customer
    }
}