using Assignment1.Custom_Filters;
using Assignment1.DAL.Repository;
using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment1.Controllers
{
    public class OrderDetailsController : Controller
    {
        private IGenericRepository repository = null;
        private string IsAdoEnabled=null;
        public OrderDetailsController()
        {
           
            this.IsAdoEnabled =(System.Configuration.ConfigurationManager.AppSettings["IsAdoNetEnabled"]);
            if (IsAdoEnabled=="true")
            {
                this.repository = new AdoRepository();
            }
            else
                this.repository = new GenericRepository();
        }
        public OrderDetailsController(IGenericRepository repository)
        {
            this.repository = repository;
        }
        //
        // GET: /OrderDetails/
        [ExceptionFilter]

        public ActionResult Index()
        {
            return View();
        }

        #region Customer Details
        /// <summary>
        /// Get the customer details from the model
        /// </summary>
        /// <returns></returns>
        [ExceptionFilter]
        public ActionResult GetCustomerDetails()
        {

            return View();
        }


        [ExceptionFilter]
        [AllowAnonymous]
        [Authorize(Users = "Anjani")]
        [AccessDeniedAuthorize]
        public ActionResult GetTransactionDetails()
        {
            if (ModelState.IsValid)
            {
                ViewBag.Update = true;

                return View(repository.GetOrderFullDetails());

            }
            return View();
        }

        [HttpPost]

        public ActionResult GetTransactionDetails(List<OrderDetails> model, string command)
        {
            var data = command.Split('_');
            var ID = Convert.ToInt16(data[1]);
            ModelState.Clear();
            if (data[0] == "Update")
            {
                if (ModelState.IsValid)
                {
                    var result = model.Where(x => x.OrderID == ID).FirstOrDefault();
                    if (result != null)
                    {
                        if (repository.UpdateDetails(result, ID))
                        {
                            ViewBag.Success = "Reocrds updated successfully";
                        }
                        else
                        {
                            ViewBag.Success = "Something went wrong. Please try later";
                        }
                        ViewBag.Update = true;
                    }
                }
                else
                {
                    ViewBag.UpdateSuccessFully = false;
                    return View(model);
                }

            }

            if (data[0] == "Delete")
            {

                if (repository.DeleteOrderDetails(ID))
                {
                    ViewBag.Success = "Reocrds Deleted successfully";
                }
                else
                {
                    ViewBag.Success = "Something went wrong. Please try later";
                }
            }


            //ModelState.Clear();
            return View(repository.GetOrderFullDetails());
        }

        #endregion

        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult AddCustomer(OrderDetails model)
        {
            if (ModelState.IsValid)
            {
                if (repository.AddCustomer(model))
                {
                    ViewBag.Success = "Records inserted successfully";

                   return RedirectToAction("GetTransactionDetails", "OrderDetails");
                }
                else
                {
                    ViewBag.Success = "Something went wrong. Please try later";
                }

            }
            return View();
        }
    }
}
