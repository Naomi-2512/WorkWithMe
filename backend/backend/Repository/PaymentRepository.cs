using backend.Data;
using backend.Interfaces;
using backend.Models;
using backend.RepositoryResults;

namespace backend.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext dataContext;

        public PaymentRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool Save() => dataContext.SaveChanges() > 1;

        public RepositoryResult<Payment> MakePayment(string UserId, Payment payment)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<Payment>(false, "User identification is invalid", new List<Payment>());

            User? user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (user == null) return new RepositoryResult<Payment>(false, "This action is not authorised, login to continue", new List<Payment>());

            dataContext.Payments.Add(payment);

            if (!Save()) return new RepositoryResult<Payment>(false, "Unable to create payment at the moment", new List<Payment>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => job.JobId == payment.JobId);

            if (job == null) return new RepositoryResult<Payment>(false, "Unable to activate job, payment successfully made. Await for changes", new List<Payment>());

            job.IsActivated = true;

            if (!Save()) return new RepositoryResult<Payment>(Save(), "Unable to activate job at the moment.", new List<Payment>());

            return new RepositoryResult<Payment>(Save(), "Payment successfully retrieved, your job is now activated.", new List<Payment>());
        }

        public RepositoryResult<Payment> GetPaymentsByUserId(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<Payment>(false, "User identification is invalid.", new List<Payment>());

            User? user = dataContext.Users.FirstOrDefault(user => user.Equals(UserId));

            if (user == null) return new RepositoryResult<Payment>(false, "You are not authorised to access this information.", new List<Payment>());

            List<Payment> payments = dataContext.Payments.OrderBy(payment => payment.PaymentId).ToList();

            if (payments == null) return new RepositoryResult<Payment>(false, $"{user.Fullname.Split(' ')[0]}, your payments for this job are not found.", new List<Payment>());

            if (payments.Count == 0) return new RepositoryResult<Payment>(false, $"{user.Fullname.Split(' ')[0]}, you have no payments at the moment.", payments);

            return new RepositoryResult<Payment>(true, "Payments are successfully retrieved", payments);
        }

        public RepositoryResult<Payment> GetPayments()
        {
            List<Payment> payments = dataContext.Payments.OrderBy(payment => payment.PaymentId).ToList();

            if (payments == null) return new RepositoryResult<Payment>(false, $"Unable to locate payments at the moment.", new List<Payment>());

            if (payments.Count == 0) return new RepositoryResult<Payment>(false, $"No payments available at the moment.", payments);

            return new RepositoryResult<Payment>(true, "Payments are successfully retrieved", payments);
        }
    }
}
