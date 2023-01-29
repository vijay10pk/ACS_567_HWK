using System;
namespace HWK2.Models
{
	public class Bill
	{
            //For auto-generating ID
            private static int nextId = 1;
            public Bill(DateTime date, double amount, string category )
            {
                Date = date;
                Amount = amount;
                Category = category;
                Id = nextId++;
            }
            //Getting and setting the value with with the variables.
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public double Amount { get; set; }
            public string Category { get; set; }
    }
}

