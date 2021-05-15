using DevFreela.Domain.DTOs;
using System.Threading.Tasks;

namespace DevFreela.Domain.Services.Payments
{
    public interface IPaymentService
    {
        //Comunicação assíncrona, via mensageria
        void ProcessPayment(PaymentInfoDTO paymentInfoDTO);
        
        //Comunicação via HTTP - Forma síncrona        
        //Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
