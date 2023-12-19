using Shared;

namespace Coordinator.Service.Services
{
    public class TransactionService : ITransactionService
    {
        public Task<bool> CallCommitPhase(Guid transactionId)
        {
            try
            {
                bool isOrderSuccess = CallServices("commit_order", new());
                bool isPaymentSuccess = CallServices("commit_payment", new());

                return Task.FromResult(isOrderSuccess && isPaymentSuccess);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> CallPreparePhase(TransactionData transactionData)
        {
            try
            {
                bool isOrderSuccess = CallServices("prepare_order", new());
                bool isPaymentSuccess = CallServices("prepare_payment", new());

                return Task.FromResult(isOrderSuccess && isPaymentSuccess);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task CallRollback(Guid transactionId)
        {
            try
            {
                bool isOrderSuccess = CallServices("rollback_order", new());
                bool isPaymentSuccess = CallServices("rollback_payment", new());

                return Task.FromResult(isOrderSuccess && isPaymentSuccess);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Guid CreateTransaction()
        {
            return Guid.NewGuid();
        }

        private bool CallServices(string endPoint, TransactionData transactionData)
        {
            return true;
        }



        
    }
}
