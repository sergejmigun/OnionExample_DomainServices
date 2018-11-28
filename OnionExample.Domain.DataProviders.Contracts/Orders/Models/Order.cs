using System;
using System.Collections.Generic;
using OnionExample.Domain.Models.Common.Orders;

namespace OnionExample.Domain.DataProviders.Contracts.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public short Status { get; set; }

        public OrderCustomer Customer { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
