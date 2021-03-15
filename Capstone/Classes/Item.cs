using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public abstract class Item
    {
        public string Name { get; }

        public abstract string ItemMessage { get; }
       
        public decimal Price { get; }

        //set to 5 as default since always starts with 5 in inventory
        public int NumberInInventory { get; set; } = 5;

        public string DisplayInfoAsString
        {
            get
            {
                string numberInInventory = "";

                if(NumberInInventory == 0)
                {
                    numberInInventory = "SOLD OUT";
                }
                else
                {
                    numberInInventory = NumberInInventory.ToString();
                }

                return ($" {Price:C2} | {Name} | Available: {numberInInventory}");

            }
        }

        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public abstract string GetItemMsg();

    }
}
