using Coordinator.Service.Models;
using Coordinator.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Coordinator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinatorController(ITransactionService _transactionService) : ControllerBase
    {
        [HttpPost("initiate_2pc")]
        public async Task InitiateTwoPhaseCommit(Data data)
        {
            Guid transactionId = _transactionService.CreateTransaction();
            TransactionData transactionData = new()
            {
                Item = data.Item,
                OrderNumber = data.OrderNumber,
                Price = data.Price,
                TransactionId = transactionId
            };
            // 1. aşama
            var isPrepareResult = await _transactionService.CallPreparePhase(transactionData);
            if (!isPrepareResult)
            {
                await _transactionService.CallRollback(transactionId);
                return;
            }

            // 2. aşama
            if (isPrepareResult)
            {
                var isCommitResult = await _transactionService.CallCommitPhase(transactionId);
                if (!isCommitResult)
                {
                    await _transactionService.CallRollback(transactionId);
                }
            }
            
        }
    }
}
