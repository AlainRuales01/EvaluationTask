using ECommerceFrontEnd.Models;
using ECommerceFrontEnd.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerceFrontEnd.Controllers
{
    public class CreditCardPaymentController : Controller
    {
        public static string API_BASEURL = "https://localhost:44342/";
        public static string VALIDATE_CREDITCARD_URL = "api/CreditCardValidator";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost(Name = "Index")]
        public IActionResult Index(CreditCardInformationModel model)
        {
            try
            {
                var response = InvokeClass.Post(API_BASEURL, VALIDATE_CREDITCARD_URL, model);
                if (string.IsNullOrEmpty(response))
                {
                    ViewBag.Error = "An error occurred while validating credit card information";
                }
                else
                {
                    var responseData = JsonConvert.DeserializeObject<ResponseModel>(response);

                    if (responseData.StatusCode == 0)
                    {
                        ViewBag.SuccessMessage = responseData.ResponseMessage;
                    }
                    else
                    {
                        ViewBag.Error = responseData.ResponseMessage;
                    }
                }                
            }
            catch (Exception)
            {
                ViewBag.Error = "An error occurred while validating credit card information";
            }

            return View(model);

        }
    }
}
