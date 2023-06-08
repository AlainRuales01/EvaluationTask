using ECommerce.Controllers;
using ECommerce;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerceBackEnd.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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

            var creditCardPan = creditCard.PAN;
            var creditCardCvv = creditCard.CVV;
            var creditCardExpiryDate = creditCard.ExpiryDate;

            try
            {
                if (!IsNumericValues(creditCardPan, creditCardCvv))
                {
                    return new ResponseModel()
                    {
                        ResponseMessage = "Please, insert only numbers",
                        StatusCode = 1
                    };
                }

                if (IsCreditCardExpired(creditCardExpiryDate))
                {
                    return new ResponseModel()
                    {
                        ResponseMessage = "The credit card has expired",
                        StatusCode = 1
                    };

                }

                if (!ValidatePanLength(creditCardPan))
                {
                    return new ResponseModel()
                    {
                        ResponseMessage = "The PAN lenght is not valid",
                        StatusCode = 1
                    };
                }

                if (!ValidateCvvLenght(creditCardPan, creditCardCvv))
                {
                    return new ResponseModel()
                    {
                        ResponseMessage = "The CVV is incorrect",
                        StatusCode = 1
                    };
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


        /// <summary>
        /// Function to validate if a credit card has expired
        /// </summary>
        /// <param name="creditCardExpireDate"></param>
        /// <returns></returns>
        private bool IsCreditCardExpired(DateTime creditCardExpireDate) => DateTime.Compare(creditCardExpireDate, ACTUAL_DATE) < 0;

        /// <summary>
        /// Function to validate if a credit card belongs to American Express
        /// </summary>
        /// <param name="creditCardPan"></param>
        /// <returns></returns>
        private bool IsAmericanExpress(string creditCardPan)
        {
            string[] americanExpressStart = { "34", "37" };
            var panStart = creditCardPan.Substring(0, 2);

            return americanExpressStart.Contains(panStart);
        }

        /// <summary>
        /// Function to validate CVV Lenght
        /// </summary>
        /// <param name="creditCardCvv"></param>
        /// <returns></returns>
        private bool ValidateCvvLenght(string creditCardPan, string creditCardCvv)
        {
            var validateCVV = 3;
            if (IsAmericanExpress(creditCardPan))
            {
                validateCVV = 4;
            }

            if (creditCardCvv.Length != validateCVV)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Function to validate PAN Lenght
        /// </summary>
        /// <param name="creditCardPan"></param>
        /// <returns></returns>
        private bool ValidatePanLength(string creditCardPan)
        {
            var creditCardPanLenght = creditCardPan.Length;
            if (creditCardPanLenght != 16 && creditCardPanLenght != 19)
            {
                return false;
            }
            return true;
        }

        private bool IsNumericValues(string creditCardPan, string creditCardCvv)
        {
            bool isPanNumeric = Regex.IsMatch(creditCardPan, @"^\d+$");
            bool isCvvNumeric = Regex.IsMatch(creditCardCvv, @"^\d+$");

            if (isPanNumeric && isCvvNumeric)
            {
                return true;
            }
            return false;
        }
    }
}
