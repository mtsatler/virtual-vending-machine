using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Capstone.Classes.Items;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        //Key is the Items Slot, Value is each Item
        public Dictionary<string, Item> Inventory = new Dictionary<string, Item>();

        //private Logger Log = new Logger();

        private String InventoryFileName = "vendingmachine.csv";

        //set to look for file in same folder as program 
        private String InventoryFilePath = "";

        private String InventoryFullPath
        {
            get
            {
               return Path.Combine(InventoryFilePath, InventoryFileName);
            }
        }

        public decimal Balance { get; private set; } = 0.00M;

        public decimal UpdateBalance(decimal changeInBalance)
        {
            Balance += changeInBalance;
            return Balance;
        }
        public decimal FeedMoney(string moneyInserted)
        {
            //check for valid dollar amount entry
            //if valid, update Balance
            //since Balance is decimal, parse string into decimal
            //if invalid, throw exception

            try
            {
                if (moneyInserted == "1" || moneyInserted == "2" || moneyInserted == "5" || moneyInserted == "10")
                {
                    Balance += int.Parse(moneyInserted);
                    Logger.LogTransactions("FEED MONEY", int.Parse(moneyInserted), Balance);
                    return Balance;
                    
                }
                else
                {
                    throw new Exception("Not a valid dollar amount");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }

        }

        public string DispenseChange()
        {
            Logger.LogTransactions("GIVE CHANGE", Balance, 0.00M);
            int quarters = 0;
            int dimes = 0;
            int nickel = 0;
            //check to see if Balance is divisible by quarters, then dimes
            //calculate how many quarters and deduct from balance
            //calculate how many dimes and deduct from balance
            //should only have 1 or 0 nickels


            if (Balance % 0.25M == 0)
            {
                quarters = (int)(Balance / 0.25M);
                Balance = 0.0M;
            }
            else
            {
                //deduct quarters then count dimes
                for (decimal i = 0.25M; i < Balance; i += 0.25M) //try <= balance
                {
                    quarters++; 
                }
                Balance -= quarters * 0.25M;
                //if change left over, divide by dimes
                if (Balance % 0.10M == 0)
                {
                    dimes = (int)(Balance / 0.10M);
                    Balance -= dimes * 0.10M;
                }
                else 
                {
                    for(decimal i = 0.10M; i < Balance; i += 0.10M)
                    {
                        dimes++;
                    }
                    Balance -= dimes * 0.10M; 
                    if (Balance == .05M)
                    {
                        nickel = 1;
                        Balance -= 0.05M;
                    }
                    
                }

            }
            return ($"Change provided: {quarters} quarters, {dimes} dimes, and {nickel} nickels.");

        }

        public bool StockInventory()
        {
            try
            {
                using (StreamReader sr = new StreamReader(InventoryFullPath))

                do
                {
                        string inputLine = sr.ReadLine();
                        string[] splitLine = inputLine.Split('|');
                        //splitLine format from .csv [0] = ItemSlot, [1] = ItemName, [2] = Price, [3] = Item Category
                        Item itemToAdd;
                        switch (splitLine[3])
                        {
                            case "Chip": 
                                itemToAdd = new Chips(splitLine[1], decimal.Parse(splitLine[2]));
                                break;
                            case "Gum":
                                 itemToAdd = new Gum(splitLine[1], decimal.Parse(splitLine[2]));
                                break;
                            case "Drink":
                                itemToAdd = new Drink(splitLine[1], decimal.Parse(splitLine[2]));
                                break;
                            case "Candy":
                                itemToAdd = new Candy(splitLine[1], decimal.Parse(splitLine[2]));
                                break;
                            default: return false;
                        }

                        Inventory.Add(splitLine[0], itemToAdd);

                } while (!sr.EndOfStream);

                return true;
            } 
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool DisplayInventory()
        {
            try
            {
                foreach (KeyValuePair<string, Item> items in Inventory)
                {
                    Console.WriteLine($"{items.Key.ToString()}|{items.Value.DisplayInfoAsString}");
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
      
        }

        public bool DispenseItem(string itemSelected)
        {
            try
            {
                //check if valid itemslot given, throw exception if not
                if (!Inventory.ContainsKey(itemSelected))
                {
                    throw new Exception("Not a valid item selection");
                }
                if (Inventory[itemSelected].NumberInInventory == 0)
                {
                    throw new Exception("Item selected is sold out");
                }
                if (Inventory[itemSelected].Price > Balance)
                {
                    throw new Exception("Your balance is not enough to purchase");
                }

                Inventory[itemSelected].NumberInInventory -= 1;

                UpdateBalance(Inventory[itemSelected].Price * -1.00M);

                Logger.LogTransactions(Inventory[itemSelected].Name, itemSelected, Inventory[itemSelected].Price, Balance);

                Console.WriteLine($"You purchased {Inventory[itemSelected].Name} for {Inventory[itemSelected].Price:C2}| Balance Remaining: {Balance:C2}");
                Console.WriteLine($"{Inventory[itemSelected].ItemMessage}");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;    
            }

        }

      
    }
}
