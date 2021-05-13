using DevFreela.Domain.DTOs;
using System.Threading.Tasks;

namespace DevFreela.Domain.Services.Payments
{
    public interface IPaymentService
    {
        //void ProcessPayment(PaymentInfoDTO paymentInfoDTO);
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
