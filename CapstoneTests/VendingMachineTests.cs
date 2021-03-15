using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes.Items;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {

        [TestMethod]
        public void UpdateBalanceHappyPathTests()
        {
            VendingMachine testMachine = new VendingMachine();

            Assert.AreEqual(5.00M, testMachine.UpdateBalance(5.00M));
            Assert.AreEqual(0.00M, testMachine.UpdateBalance(-5.00M));
        }
        [TestMethod]
        public void DispenseChangeHappyPathTests()
        {
            VendingMachine testDispenseChange = new VendingMachine();
            
            testDispenseChange.Balance = 2.00M;
            Assert.AreEqual($"Change provided: 8 quarters, 0 dimes, and 0 nickels.", testDispenseChange.DispenseChange());

            testDispenseChange.Balance = 3.45M;
            Assert.AreEqual($"Change provided: 13 quarters, 2 dimes, and 0 nickels.", testDispenseChange.DispenseChange());

            testDispenseChange.Balance = 1.40M;
            Assert.AreEqual($"Change provided: 5 quarters, 1 dimes, and 1 nickels.", testDispenseChange.DispenseChange());

            testDispenseChange.Balance = 0.00M;
            Assert.AreEqual($"Change provided: 0 quarters, 0 dimes, and 0 nickels.", testDispenseChange.DispenseChange());
        }
        [TestMethod]
        public void DispenceChangeNullTest()
        {
            VendingMachine testDispenseChange = new VendingMachine();

            testDispenseChange.Balance = 0;
            Assert.AreEqual($"Change provided: 0 quarters, 0 dimes, and 0 nickels.", testDispenseChange.DispenseChange());
        }
        [TestMethod]
        public void FeedMoneyHappyPathTests()
        {
            VendingMachine testFeedMoney = new VendingMachine();

            //tests as a continued transaction
            Assert.AreEqual(5.00M, testFeedMoney.FeedMoney("5"));
            Assert.AreEqual(6.00M, testFeedMoney.FeedMoney("1"));
            Assert.AreEqual(8.00M, testFeedMoney.FeedMoney("2"));
            Assert.AreEqual(18.00M, testFeedMoney.FeedMoney("10"));
        }
        [TestMethod]
        public void FeedMoneyEmptyStringTest()
        {
            VendingMachine testFeedMoney = new VendingMachine();

            Assert.AreEqual(0.00M, testFeedMoney.FeedMoney("0"));
        }
        [TestMethod]
        public void FeedMoneyInvalidEntryTest()
        {
            VendingMachine testFeedMoney = new VendingMachine();

            Assert.AreEqual(0.00M, testFeedMoney.FeedMoney("3"));
            Assert.AreEqual(0.00M, testFeedMoney.FeedMoney("11"));
        }


        [TestMethod]
        public void DispenseItemHappyPathTests()
        {
            VendingMachine testMachine = new VendingMachine();
            testMachine.Balance = 5.00M;

            Item testItem = new Gum("TestItem", 1.00M);
            testMachine.Inventory["A1"] = testItem;

            Assert.AreEqual(true, testMachine.DispenseItem("A1"));
            Assert.AreEqual(4.00M, testMachine.Balance);
            Assert.AreEqual(4, testMachine.Inventory["A1"].NumberInInventory);
        }

        [TestMethod]
        public void DispenseItemNullTest()
        {
            VendingMachine testMachine = new VendingMachine();
            //Because DispenseItem returns a bool, it will always return false when an exception is thrown, null will throw an exception
            Assert.AreEqual(false, testMachine.DispenseItem(null)); 
        }

        [TestMethod]
        public void DispenseItemInputValidationTest()
        {
            //DispenseItem returns a bool, it will always return false when an exception is thrown, any
            //invalid input will throw an exception and return false
            VendingMachine testMachine = new VendingMachine();

            testMachine.Balance = 5.00M;

            Item testItem = new Gum("TestItem", 1.00M);
            testMachine.Inventory["A50"] = testItem;

            Assert.AreEqual(false, testMachine.DispenseItem(""));
            Assert.AreEqual(false, testMachine.DispenseItem("D7"));
            Assert.AreEqual(false, testMachine.DispenseItem("12653"));
            Assert.AreEqual(true, testMachine.DispenseItem("A50"));
            Assert.AreEqual(false, testMachine.DispenseItem("a50"));
        }
        [TestMethod]
        public void DispenseItemNotEnoughBalanceTest()
        {
            //DispenseItem will throw an exception and return false when not enough money is available for purchase
            VendingMachine testMachine = new VendingMachine();

            testMachine.Balance = 0.50M;

            Item testItem = new Gum("TestItem", 1.00M);
            testMachine.Inventory["A50"] = testItem;

            Item testItemTwo = new Gum("TestItem2",  0.50M);
            testMachine.Inventory["A31"] = testItemTwo;

            Assert.AreEqual(false, testMachine.DispenseItem("A50"));
            Assert.AreEqual(true, testMachine.DispenseItem("A31"));
        }

        [TestMethod]
        public void StockInventoryTest()
        {
            //our stock inventory method returns a bool, true if successful, false if unsuccessful

            VendingMachine testMachine = new VendingMachine();

            Assert.AreEqual(true, testMachine.StockInventory());
           
        }
    }
}
