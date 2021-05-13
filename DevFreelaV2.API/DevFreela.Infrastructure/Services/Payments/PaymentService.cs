using DevFreela.Domain.DTOs;
using DevFreela.Domain.Services.Payments;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        //private readonly IMessageBusService _messageBusService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentsBaseUrl;
        private const string QUEUE_NAME = "Payments";

        //public PaymentService(IMessageBusService messageBusService)
        //{
        //    _messageBusService = messageBusService;
        //}

        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }

        //Processando o pagamento utilizando microsserviço com serviço de mensageria RabbitMQ
        //public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        //{
        //    var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
        //    var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

        //    _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        //}

        //Processando o pagamento utilizando microsserviço com protocolo HTTP
        public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var url = $"{_paymentsBaseUrl}/api/payments"; //Chamando a URL do microsserviço de pagamento
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);

            var paymentInfoContent = new StringContent(paymentInfoJson, Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Payments");

            var response = await httpClient.PostAsync(url, paymentInfoContent);

            return response.IsSuccessStatusCode;
        }
    }
}
