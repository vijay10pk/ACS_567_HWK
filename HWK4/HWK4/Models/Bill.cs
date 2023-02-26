using System;
namespace HWK4.Models
{
    public class Bill
    {
        public Bill()
        {
        }

        /// <summary>
        /// Constructor for bill object
        /// </summary>
        /// <param name="date">date of the bill</param>
        /// <param name="amount">Bill Amount</param>
        /// <param name="category">Category of expense</param>
        public Bill(DateTime date, double amount, string category)
        {
            Date = date;
            Amount = amount;
            Category = category;
        }
        //Getting and setting the value with with the variables.
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
    }
}

