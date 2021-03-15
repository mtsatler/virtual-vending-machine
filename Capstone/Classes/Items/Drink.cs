using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Drink : Item
    {
        public Drink(string name, decimal price) : base(name, price) { }

        public override string ItemMessage { get; } = "Glug Glug, Yum!";

        public override string GetItemMsg()
        {
            return ItemMessage;
        }


    }
}
