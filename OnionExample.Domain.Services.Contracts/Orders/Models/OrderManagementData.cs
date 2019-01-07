using System.Collections.Generic;
using OnionExample.Core.Domain.Orders;

namespace OnionExample.Core.Services.Contracts.Orders.Models
{
    public class OrderManagementData
    {
        public OrderCustomer Customer { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }
    }
}
