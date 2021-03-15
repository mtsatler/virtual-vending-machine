using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Candy : Item
    {

        public Candy(string name, decimal price) : base(name, price) { }

        public override string ItemMessage { get; } = "Munch Munch, Yum!";

        public override string GetItemMsg()
        {
            return ItemMessage;
        }

    }
}
