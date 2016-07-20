using System;
using ECommerceBackEnd;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_BasketItem_ConfirmBasketItemCanBeCreated()
        {
            var itemToAdd = new BasketItem(1, "TestItem", 2.00, 2);

            Assert.IsNotNull(itemToAdd);
        }

        [TestMethod]
        public void Test_AddItem_AddItemToBasketConfirmReturnsTrue()
        {
            var itemToAdd = new BasketItem(1, "TestItem", 2.00, 2);

            var result = DatabaseAccess.AddItem(itemToAdd);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Test_GetItem_GetItemFromBasketConfirmReturnsFullItem()
        {
            var itemFound = DatabaseAccess.GetItem("Item1");

            Assert.AreEqual("Item1", itemFound.itemName);
        }

        [TestMethod]
        public void Test_GetBasket_ConfirmItemIsNotNull()
        {
            var itemFound = DatabaseAccess.GetBasket("1");

            Assert.IsNotNull(itemFound);
        }

        [TestMethod]
        public void Test_RemoveItems_ConfirmBasketIsEmptied()
        {
            DatabaseAccess.RemoveItems("1");

            DatabaseAccess.RemoveItems("2");

            var itemFound = DatabaseAccess.GetBasket("1");

            Assert.AreEqual(0, itemFound.Count);
        }
    }
}
