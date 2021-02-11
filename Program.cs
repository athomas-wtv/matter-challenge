using System;
using System.Collections.Generic;
using System.Linq;

namespace MatterChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use reflection to get the number of properties for the class.
            Console.WriteLine("Use reflection to get the number of properties for the class");
            Type type = typeof(Transaction);
            var numOfProperties = type.GetProperties().Length;
            var properties = type.GetProperties();
            
            // Add property names to a list of strings
            Console.WriteLine("Add property names to a list of strings");
            var propertyNames = new List<string>();
            foreach(var prop in properties)
            {
                propertyNames.Add(prop.Name);
            }

            // Converting each record to be a transaction object and adding it to a list of transactions.
            Console.WriteLine("Converting each record to be a transaction object and adding it to a list of transactions.");
            var transactions = new List<Transaction>();
            string[] rawTransactions = System.IO.File.ReadAllLines("transactions.txt");
            string[] rtArr;
            foreach(var rt in rawTransactions.Skip(1))
            {
                rtArr = rt.Split(",");
                var transaction = new Transaction();
                for (int i = 0; i < rtArr.Length; i++)
                {
                    // propertyPlacement = i - numOfProperties;
                    var currentPropety = transaction.GetType().GetProperty(propertyNames[i]);

                    // Checking the property type then updating the value
                    if(currentPropety.PropertyType.Name == "String")
                    {
                        currentPropety.SetValue(transaction, rtArr[i]);
                    }
                    else if(currentPropety.PropertyType.Name == "Double")
                    {
                        currentPropety.SetValue(transaction, Convert.ToDouble(rtArr[i]));
                    }
                    else if(currentPropety.PropertyType.Name == "DateTime")
                    {
                        currentPropety.SetValue(transaction, DateTime.Parse(rtArr[i]).Date);
                    }

                }
                transactions.Add(transaction);
            }

            // Adding transactions to a dictionary
            Console.WriteLine("Adding transactions to a dictionary");
            var master = new Dictionary<int, Transaction>();
            var index = 1;
            foreach(var t in transactions)
            {
                master.Add(index,t);
                index++;
            }
            

            // Find duplicates and remove them
            Console.WriteLine("Find duplicates and remove them");
            bool firstOccurance = true;
            foreach(var t in transactions)
            {
                foreach(var tr in master)
                {
                    var isEqual = t.Equals(tr.Value);
                    if(isEqual && firstOccurance)
                    {
                        firstOccurance = false;
                    }
                    else if(isEqual)
                    {
                        master.Remove(tr.Key);
                    }
                }
                firstOccurance = true;
            }
            
            Console.WriteLine("Largest Transaction:");
            DisplayLargestTransaction(master);
            Console.WriteLine("Smallest Transaction:");
            DisplaySmallestTransaction(master);
            Console.ReadLine();
            
        }

        private static void DisplaySmallestTransaction(Dictionary<int, Transaction> master)
        {
            var hasSmallest = 1;
            var currentAmount = 0.00;
            var smallestAmount = master[hasSmallest].Amount;
            for(var i = 1; i <= master.Count; i++)
            {
                currentAmount = master[i].Amount;
                if(currentAmount < smallestAmount)
                {
                    smallestAmount = currentAmount;
                    hasSmallest = i;
                }
            }
            Display(master[hasSmallest]);
        }

        private static void DisplayLargestTransaction(Dictionary<int, Transaction> master)
        {
            var hasLargest = 1;
            var currentAmount = 0.00;
            var largestAmount = master[hasLargest].Amount;
            for(var i = 1; i <= master.Count; i++)
            {
                currentAmount = master[i].Amount;
                if(currentAmount > largestAmount)
                {
                    largestAmount = currentAmount;
                    hasLargest = i;
                }
            }
            Display(master[hasLargest]);
        }

        private static void Display(Transaction transaction)
        {
            Console.WriteLine("Date: {0}, Description: {1}, Amount: {2}, Account Name: {3}", transaction.Date, transaction.Description, transaction.Amount, transaction.AccountName);
        }
    }
}
