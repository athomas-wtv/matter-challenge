using System;
using System.Collections.Generic;

namespace Sites
{
    class Program
    {
        static void Main(string[] args)
        {
            String strTransactions = "";
            var transactions = new List<Transaction>();
            var transaction = new Transaction();

            Type type = typeof(Transaction);
            var numOfProperties = type.GetProperties().Length;
            var properties = type.GetProperties();
            
            var propertyNames = new List<string>();
            foreach(var prop in properties)
            {
                propertyNames.Add(prop.Name);
            }

            var fileItems = strTransactions.Split(',');
            int num = 0;
            for (int i = numOfProperties; i <= fileItems.Length; i++)
            {
                num = i - numOfProperties;
                transaction.GetType().GetProperty(propertyNames[num]).SetValue(transaction, fileItems[i]);
                transactions.Add(transaction);
            }

            var master = new Dictionary<int, Transaction>();
            var index = 1;
            foreach(var t in transactions)
            {
                master.Add(index,t);
            }
            
            bool first = true;
            foreach(var t in transactions)
            {
                foreach(var tr in master)
                {
                    var isEqual = t.Equals(tr.Value);
                    if(isEqual && first)
                    {
                        first = false;
                    }
                    else
                    {
                        master.Remove(tr.Key);
                    }
                }
            }

            
        }
    }
}
