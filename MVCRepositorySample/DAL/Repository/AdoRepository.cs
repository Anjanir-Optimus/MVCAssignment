using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Assignment1.DAL.Repository
{
    public class AdoRepository : IGenericRepository
    {

        private string connectionString = null;

        public AdoRepository()
        {
            connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
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
                List<OrderDetails> resDetails = null;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("GetAllOrderDetails", sqlConnection))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataTable dtOrderDetails = new DataTable();
                        adp.Fill(dtOrderDetails);

                        if (dtOrderDetails != null)
                        {

                            resDetails = Global.ConvertDataTable<OrderDetails>(dtOrderDetails);
                        }
                    }
                }

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
                List<CustomerDetails> resDetails = null;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("GetClientDetails", sqlConnection))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataTable dtOrderDetails = new DataTable();
                        adp.Fill(dtOrderDetails);

                        if (dtOrderDetails != null)
                        {

                            resDetails = Global.ConvertDataTable<CustomerDetails>(dtOrderDetails);
                        }
                    }
                }

                return resDetails;

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
                List<Product> resDetails = null;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("GetProductDetail", sqlConnection))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataTable dtOrderDetails = new DataTable();
                        adp.Fill(dtOrderDetails);

                        if (dtOrderDetails != null)
                        {

                            resDetails = Global.ConvertDataTable<Product>(dtOrderDetails);
                        }
                    }
                }

                return resDetails;
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

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("updateOrderDetails", sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.Add("@customerName", SqlDbType.NVarChar).Value = model.CustomerName;
                        sqlcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                        sqlcmd.Parameters.Add("@BillingAddress", SqlDbType.NVarChar).Value = model.BillingAddress;
                        sqlcmd.Parameters.Add("@ShippingAddress", SqlDbType.NVarChar).Value = model.ShippingAddress;
                        sqlcmd.Parameters.Add("@Number", SqlDbType.NVarChar).Value = model.Number;
                        sqlcmd.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = model.NetAmount;
                        sqlcmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = model.Quantity;
                        sqlcmd.Parameters.Add("@OrderAmount", SqlDbType.Decimal).Value = model.OrderAmount;
                        sqlcmd.Parameters.Add("@Tax", SqlDbType.Decimal).Value = model.Tax;
                        sqlcmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = model.Discount;
                        sqlcmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = model.Rate;
                        sqlcmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = model.Amount;
                        sqlcmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = model.CustomerID;
                        sqlcmd.Parameters.Add("@Orderno", SqlDbType.Int).Value = model.OrderID;
                        sqlcmd.Parameters.Add("@OrderItemNumber", SqlDbType.Int).Value = model.OrderItemID;
                        int Success = sqlcmd.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (Success > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }


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
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlcmd = new SqlCommand("DeleteCustomer", sqlConnection);
            try
            {


                sqlConnection.Open();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.Add("@OderNo", SqlDbType.Int).Value = orderNo;
                int Success = sqlcmd.ExecuteNonQuery();

                if (Success > 0)
                {
                    return true;
                }
                return false;


            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return false;
            }
            finally
            {
                if (sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }

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
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("AddNewCustomer", sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.Add("@customerName", SqlDbType.NVarChar).Value = model.CustomerName;
                        sqlcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = model.Email;
                        sqlcmd.Parameters.Add("@BillingAddress", SqlDbType.NVarChar).Value = model.BillingAddress;
                        sqlcmd.Parameters.Add("@ShippingAddress", SqlDbType.NVarChar).Value = model.ShippingAddress;
                        sqlcmd.Parameters.Add("@Number", SqlDbType.NVarChar).Value = model.Number;
                        sqlcmd.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = model.NetAmount;
                        sqlcmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = model.Quantity;
                        sqlcmd.Parameters.Add("@OrderAmount", SqlDbType.Decimal).Value = model.OrderAmount;
                        sqlcmd.Parameters.Add("@Tax", SqlDbType.Decimal).Value = model.Tax;
                        sqlcmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = model.Discount;
                        sqlcmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = model.Rate;
                        sqlcmd.Parameters.Add("@amount", SqlDbType.Decimal).Value = model.Amount;
                        sqlcmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = model.CustomerID;
                        sqlcmd.Parameters.Add("@Orderno", SqlDbType.Int).Value = model.OrderID;
                        sqlcmd.Parameters.Add("@OrderItemNumber", SqlDbType.Int).Value = model.OrderItemID;
                        sqlcmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = 4;
                        int Success = sqlcmd.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (Success > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHandling.SaveException(ex);
                return false;
            }
        }

    }
}