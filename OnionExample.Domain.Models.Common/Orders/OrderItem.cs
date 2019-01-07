﻿namespace OnionExample.Core.Domain.Orders
{
    public class OrderItem
    {
        public int? ProductId { get; set; }

        public string ProductTitle { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
