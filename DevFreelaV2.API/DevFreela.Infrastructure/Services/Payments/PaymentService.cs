using DevFreela.Domain.DTOs;
using DevFreela.Domain.Services;
using DevFreela.Domain.Services.MessageBus;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";

        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
