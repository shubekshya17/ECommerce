using ECommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ECommerce.Controllers
{
    public class OnlinePaymentController : Controller
    {
        [HttpGet]
        public ActionResult Success(string pidx, string transaction_id, string tidx, int amount)
        {
            return View();
        }

        [HttpPost]
        [Route("KhaltiPayment")]
        public async Task<object> KhaltiPayment([FromBody] KhaltiPaymentVM khaltiPaymentVM)
        {
            string khalti_pri_key = "23c3db9b228f4b88ac1ca9c2ecfa3b95";
            var url = "https://dev.khalti.com/api/v2/epayment/initiate/";
            var payload = new
            {
                return_url = khaltiPaymentVM.RedirectUrl + "/OnlinePayment/Success",
                website_url = khaltiPaymentVM.RedirectUrl,
                amount = khaltiPaymentVM.Amount * 100,
                purchase_order_id = Guid.NewGuid().ToString(),
                purchase_order_name = Guid.NewGuid().ToString(),
                merchant_info = new
                {
                    name = "Test",
                    email = "test@gmail.com"
                },
                customer_info = new
                {
                    name = "Himalaya Ecommerce",
                    address = "Lalitpur"
                },
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "key " + khalti_pri_key);
            var response = await client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            return Ok(new
            {
                Success = response.StatusCode == HttpStatusCode.OK,
                Message = "OK",
                Data = responseContent
            });
        }
    }
}
