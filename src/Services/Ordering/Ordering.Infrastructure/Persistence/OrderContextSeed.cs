using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed 
    {
        public static async Task SeendAsync(OrderContext orderContext,ILogger<OrderContext> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContext}",typeof(OrderContext));
            }
        }
        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>
            {
                new Order(){UserName = "Milad",FirstName = "Milad",LastName="Nematpour",EmailAddress="miladnowshahr@gmail.com",TotalPrice=370,}
            };
        }
    }
}
