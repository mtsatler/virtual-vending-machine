using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{


    public class VendingMachineUI
    {

        private VendingMachine ourMachine = new VendingMachine();

        //Set to MainMenu because that is always where we start
        private string MenuSwitch { get; set; } = "MainMenu";

        public void RunMachine()
        {
            //First load inventory into vending machine and throw error if unsuccessful
            try
            {
                ourMachine.StockInventory();
            }
            catch (Exception e)
            {
                Console.WriteLine("Our Vending Machine failed to load its inventory");
                Console.WriteLine("Please place vendingmachine.csv file in same directory as program runtime");
            }


            Console.WriteLine("Welcome to our vending machine!");

            //The menu loop starts here.  Will continue to run program until MenuSwitch is set to "Exit"
            do
            {
                //MenuSwitch is used to call the correct menu interface, MainMenu() and PurchaseMenu() return MenuSwitch
                switch (MenuSwitch)
                {
                    case "MainMenu":
                        MenuSwitch = MainMenu();
                        break;

                    case "PurchaseMenu":
                        MenuSwitch = PurchaseMenu();
                        break;

                    default:
                        break;
                }

            } while (MenuSwitch != "Exit");

            Console.WriteLine();
            Console.WriteLine("Thank you for using our vending machine!");
            Console.WriteLine();

        }


        //return string with next menu
        private string MainMenu()
        {
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine();
            Console.Write("Please enter your menu option: ");

            try
            {
                switch (Console.ReadLine())
                {
                    //display vending machine items
                    case "1":
                        Console.WriteLine();
                        ourMachine.DisplayInventory();
                        Console.WriteLine();
                        return "MainMenu";
                    //display purchase menu
                    case "2":
                        Console.Clear();
                        return "PurchaseMenu";
                    //exit program
                    case "3":
                        return "Exit";
                    default:
                        Console.Clear();
                        throw new Exception("Please enter menu choice 1-3");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return MenuSwitch;
            }
           
        }
     
        //return string with next menu
        private string PurchaseMenu()
        {

            Console.WriteLine($"Current Money Provided: {ourMachine.Balance:C2}");
            Console.WriteLine();
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine();
            Console.Write("Please enter your menu option: ");
            

            try
            {
                switch (Console.ReadLine())
                {
                    //feed money
                    case "1":
                        Console.WriteLine("Please enter dollar amount: 1, 2, 5, 10");
                        try
                        {
                            ourMachine.FeedMoney(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.Message);                     
                        }
                        Console.WriteLine();
                        return "PurchaseMenu";
                    //purchase and dispense item
                    case "2":
                        ourMachine.DisplayInventory();
                        Console.WriteLine();
                        Console.Write("Which item would you like to purchase? ");
                        try
                        {
                            ourMachine.DispenseItem(Console.ReadLine().ToUpper());
                            Console.WriteLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                        }
                        return "PurchaseMenu";
                    //finish transaction
                    case "3":
                        Console.WriteLine(ourMachine.DispenseChange());
                        Console.WriteLine();
                        return "MainMenu";
                    default:
                        Console.Clear();
                        throw new Exception("Please enter menu choice 1-3");
         
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return MenuSwitch;
            }

        }

    }
}
