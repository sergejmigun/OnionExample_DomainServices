using System.Collections.Generic;

namespace OnionExample.Areas.Cart.ApiModels
{
    public class CartModel
    {
        public CartModel()
        {
            this.Items = new List<CartItemModel>();
        }

        public ICollection<CartItemModel> Items { get; private set; }
    }
}