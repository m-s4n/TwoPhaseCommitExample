using Shared;

namespace Coordinator.Service.Services
{
    public interface ITransactionService
    {
        Guid CreateTransaction();
        Task<bool> CallPreparePhase(TransactionData transactionData);

        Task<bool> CallCommitPhase(Guid transactionId);

        Task CallRollback(Guid transactionId);

    }
}
