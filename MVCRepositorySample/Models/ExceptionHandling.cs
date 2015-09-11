using Assignment1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Assignment1.Models
{
    public static class ExceptionHandling
    {
        #region Save Exception in the Database
        /// <summary>
        /// Save exception in the database
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool SaveException(Exception ex)
        {
            var objEntity = new Optimus_TradeEntities();
           
            var objErrorTracer = new ErrorTracer()
            {
                ErrorMessage=ex.Message,
                ErrorDate=DateTime.Now,
                InnerException=ex.InnerException==null?null:ex.InnerException.ToString(),
                StackTrace=ex.StackTrace
            };
            objEntity.ErrorTracers.Add(objErrorTracer);
            objEntity.SaveChanges();
            return true;
        }
        #endregion
    }
}