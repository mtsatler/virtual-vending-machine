using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Gum : Item
    {

        public Gum(string name, decimal price) : base(name, price) { }

        public override string ItemMessage { get; } = "Chew Chew, Yum!";

        public override string GetItemMsg()
        {
            return ItemMessage;
        }


    }
}
