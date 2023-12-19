
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Service.DataAccess.Contexts;
using Order.Service.Enums;
using Shared;
using OrderObject =  Order.Service.DataAccess.Entities.Order;

namespace Order.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(AppDbContext _context) : ControllerBase
    {
        [HttpPost("prepare_order")]
        public async Task<bool> PrepareOrder(TransactionData transactionData)
        {
            try
            {
                OrderObject order = new()
                {
                    OrderNumber = transactionData.OrderNumber,
                    Item = transactionData.Item,
                    PreparationStatus = OrderPreparationStatus.PREPARING,
                    TransactionId = transactionData.TransactionId,
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        [HttpPost("commit_order")]
        public async Task<bool> CommitOrder(Guid transactionId)
        {
            OrderObject order = await _context.Orders.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            
            if(order is { PreparationStatus : OrderPreparationStatus.PREPARING })
            {
                order.PreparationStatus = OrderPreparationStatus.COMMITTED;
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

        [HttpPost("rollback_order")]
        public async Task<bool> RollbackOrder(Guid transactionId)
        {
            OrderObject order = await _context.Orders.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            if(order is not null)
            {
                order.PreparationStatus = OrderPreparationStatus.ROLLBACK;
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
