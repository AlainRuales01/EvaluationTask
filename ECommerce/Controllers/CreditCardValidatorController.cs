using ECommerce.Controllers;
using ECommerce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerceBackEnd.Models;
using System.Diagnostics;

namespace ECommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardValidatorController : ControllerBase
    {
        private static DateTime ACTUAL_DATE = DateTime.Now.Date;

        [HttpPost(Name = "ValidateCreditCard")]
        public ResponseModel ValidateCreditCard(CreditCardInformationModel creditCard)
        {
            int statusCode = 0;
            string responseMessage = "The credit card is valid";

            var validateCVV = 3;
            string[] americanExpressStart = { "34", "37" };
            try
            {
                bool isCreditCardInvalid = DateTime.Compare(creditCard.ExpiryDate, ACTUAL_DATE) < 0;
                if (isCreditCardInvalid)
                {
                    statusCode = 1;
                    responseMessage = "The credit card has expired";
                }

                var panStart = creditCard.PAN.Substring(0, 2);

                if (americanExpressStart.Contains(panStart))
                {
                    validateCVV = 4;
                }

                if (creditCard.CVV.Length != validateCVV)
                {
                    statusCode = 1;
                    responseMessage = "The CVV is incorrect";
                }

                var creditCardPanLenght = creditCard.PAN.Length;
                if (creditCardPanLenght != 16 && creditCardPanLenght != 19)
                {
                    statusCode = 1;
                    responseMessage = "The PAN lenght is not valid";
                }

                
            }
            catch (Exception ex)
            {
                statusCode = 1;
                responseMessage = ex.Message;
            }

            return new ResponseModel()
            {
                ResponseMessage = responseMessage,
                StatusCode = statusCode
            };




        }
    }
}
