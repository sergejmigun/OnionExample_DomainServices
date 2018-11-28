using System.Collections.Generic;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.Services.Contracts.Orders.Models
{
    public class OrderCreationData
    {
        public OrderCustomer Customer { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }
    }
}