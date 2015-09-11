using Assignment1.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{


    public class OrderDetails : CustomerDetails
    {


        public int OrderID{ get; set; }

        
        public Nullable<decimal> OrderAmount { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{1,3}(,[0-9]{3})*(([\\.,]{1}[0-9]*)|())$", ErrorMessage = "Discount can't have more than 2 decimal places")]
        public Nullable<decimal> Discount { get; set; }
        [Required]

        [RegularExpression(@"^[0-9]{1,3}(,[0-9]{3})*(([\\.,]{1}[0-9]*)|())$", ErrorMessage = "Tax can't have more than 2 decimal places")]
        public Nullable<decimal> Tax { get; set; }
         [RegularExpression(@"^[0-9]{1,3}(,[0-9]{3})*(([\\.,]{1}[0-9]*)|())$", ErrorMessage = "NetAmount can't have more than 2 decimal places")]
        public Nullable<decimal> NetAmount { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }

        public int ProductID { get; set; }
        [Required]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "Please Enter proper Product Name")]
        public string ProductName { get; set; }

        
        public decimal Price { get; set; }
        public Nullable<int> Stockleft { get; set; }

        public int OrderItemID { get; set; }
        public decimal Rate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public class CustomerDetails
    {
        [Required(ErrorMessage = "Please provide Customer Name")]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "Please Enter proper Customer Name")]
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        [Required]
        [RegularExpression("^[0-9A-Za-z ]+$", ErrorMessage = "Please Enter proper Shipping Address")]
        public string ShippingAddress { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        
        
        [RegularExpression("^[0-9]*$", ErrorMessage = "Entered phone format is not valid.")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("^[0-9A-Za-z ]+$")]
        public string BillingAddress { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class ProductDetails
    {
        public List<OrderDetails> orderDetails;
        //  public List<Product> productData;

    }
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}