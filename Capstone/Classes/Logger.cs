using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    static class Logger
    {
        public static string FilePath { get; set; } = "Log.txt";
        public static string LogTransactions(string transactionDescription, decimal transactionAmount, decimal endBalance)
        {

            string logEntry = ($"{DateTime.Now} {transactionDescription}: {transactionAmount:C2} {endBalance:C2}");
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine(logEntry);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return logEntry;
        }     
        public static string LogTransactions(string itemDispensed, string itemSlot, decimal transactionAmount, decimal endBalance)
        {
            string logEntry = ($"{DateTime.Now} {itemDispensed} {itemSlot} {transactionAmount:C2} {endBalance:C2}");
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine(logEntry);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return logEntry;
        }

    }
}
