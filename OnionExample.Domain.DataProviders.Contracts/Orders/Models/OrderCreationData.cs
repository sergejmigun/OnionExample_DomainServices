using System;
using System.Collections.Generic;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Contracts.Orders.Models
{
    public class OrderCreationData
    {
        public DateTime CreationDate { get; set; }

        public OrderCustomer Customer { get; set; }

        public IEnumerable<OrderItemManagementData> Items { get; set; }

        public short Status { get; set; }
    }
}
