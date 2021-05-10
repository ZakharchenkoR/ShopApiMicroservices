﻿using System.Collections.Generic;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
            Items = new List<ShoppingCartItem>();
        }

        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
	            {
                    totalPrice += item.Quantity * item.Price;
                }

                return totalPrice;
            }
        }
    }
}
