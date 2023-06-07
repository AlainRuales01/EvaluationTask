namespace ECommerceBackEnd.Models
{
    public class ResponseModel
    {
        public string ResponseMessage { get; set; }
        public int StatusCode {get;set;} // 0: OK, 1: ERROR
    }
}
