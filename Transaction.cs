using System;
public class Transaction
{
    public DateTime Date {get; set;}
    public string Description { get; set; }
    public string OriginalDescription { get; set; }
    public double Amount { get; set; }
    public string TransactionType { get; set; }
    public string Category { get; set; }
    public string AccountName { get; set; }
    public string Labels { get; set; }
    public string Notes { get; set; }
}

    
// Date,Description,Original Description,Amount,Transaction Type,Category,Account Name,Labels,Notes