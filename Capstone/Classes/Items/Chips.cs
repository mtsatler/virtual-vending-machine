using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Chips : Item
    {
        public Chips(string name, decimal price) : base(name, price) { }

        public override string ItemMessage { get; } = "Crunch Crunch, Yum!";

        public override string GetItemMsg()
        {
            return ItemMessage;
        }

    }
}
