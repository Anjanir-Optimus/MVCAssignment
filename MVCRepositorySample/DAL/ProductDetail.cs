//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment1.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            this.OrderItemDetails = new HashSet<OrderItemDetail>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> StockLeft { get; set; }
    
        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
