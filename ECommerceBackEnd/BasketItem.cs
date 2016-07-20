using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceBackEnd
{
    /// <summary>A shopping item as it exists in the shop or the basket.</summary>
    public class BasketItem
    {
        /// <summary>Constructor for a new basket item.</summary>
        public BasketItem(int itemID, string itemName, double itemPrice, int basketID)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.itemPrice = itemPrice;
            this.basketID = basketID;
        }

        /// <summary>The ID number for the item.</summary>
        public int itemID;

        /// <summary>The name of the item.</summary>
        public string itemName;

        /// <summary>The price of the item.</summary>
        public double itemPrice;

        /// <summary>The ID number for the basket the item has been added to.</summary>
        public int basketID;
    }
}
