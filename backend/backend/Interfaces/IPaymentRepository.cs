using backend.Models;
using backend.RepositoryResults;

namespace backend.Interfaces
{
    public interface IPaymentRepository
    {
        bool Save();
        RepositoryResult<Payment> MakePayment(string UserId, Payment payment);
        RepositoryResult<Payment> GetPayments();
        RepositoryResult<Payment> GetPaymentsByUserId(string UserId);
    }
}
