using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.Service.DataAccess.Contexts;
using Payment.Service.Enums;
using Shared;
using PaymentObject = Payment.Service.DataAccess.Entities.Payment;

namespace Payment.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(AppDbContext _context) : ControllerBase
    {
        [HttpPost("prepare_payment")]
        public async Task<bool> PrepareOrder(TransactionData transactionData)
        {
            try
            {
                PaymentObject payment = new()
                {
                    OrderNumber = transactionData.OrderNumber,
                    PaymentMode = PaymentStatus.PENDING,
                    TransactionId = transactionData.TransactionId,
                };

                await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        [HttpPost("commit_payment")]
        public async Task<bool> CommitOrder(Guid transactionId)
        {
            PaymentObject payment = await _context.Payments.FirstOrDefaultAsync(x => x.TransactionId == transactionId);

            if (payment is { PaymentMode: PaymentStatus.PENDING })
            {
                payment.PaymentMode = PaymentStatus.APPROVED;
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;

        }

        [HttpPost("rollback_payment")]
        public async Task<bool> RollbackOrder(Guid transactionId)
        {
            PaymentObject payment = await _context.Payments.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if (payment is not null)
            {
                payment.PaymentMode = PaymentStatus.ROLLBACK;
                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
