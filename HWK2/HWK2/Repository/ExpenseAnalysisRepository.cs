using System;
using System.Globalization;
using HWK2.Models;

namespace HWK2.Repository
{
	public class ExpenseAnalysisRepository
	{
        private static ExpenseAnalysisRepository instance;
        private List<Bill> bills;
        /// <summary>
        /// Constructor to initialize the object
        /// </summary>
        public ExpenseAnalysisRepository()
        {
            bills = new();
            ReadDataFromCsv();
        }

        public static ExpenseAnalysisRepository getInstance()
        {
            if(instance == null)
            {
                instance = new();
            }
            return instance;
        }

        /// <summary>
        /// Method that read data from the csv file.
        /// </summary>
        private void ReadDataFromCsv()
        {
            try
            {
                using (StreamReader sr = new StreamReader("./Data/bills.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        bills.Add(new Bill(DateTime.Parse(line[0]),
                            double.Parse(line[1]),
                            line[2]));
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            //return bills;

        }

        /// <summary>
        /// Method that handle get all data
        /// </summary>
        /// <returns>bills</returns>
        public List<Bill> GetExpenses()
        {
            return bills;
        }

        public Bill GetExpenseById(int id)
        {
            Bill expense = null;
            foreach(Bill b in bills)
            {
                if(id == b.Id)
                {
                    expense = b;
                    break;
                }
            }
            return expense;
        }
        /// <summary>
        /// Method to handle create new data.
        /// </summary>
        /// <param name="bill">new bill data</param>
        /// <returns>true if new data has been created or false if new data is not created</returns>
        public bool AddExpense(Bill bill)
        {
            bool isAdded = true;

            foreach(Bill b in bills)
            {
                if(b.Id == bill.Id)
                {
                    isAdded = false;
                    break;
                }
            }
            if (isAdded)
            {
                bills.Add(bill);
            }
            return isAdded;
        }

        /// <summary>
        /// Method that handle updating a data
        /// </summary>
        /// <param name="id">bill id</param>
        /// <param name="updateBill">updated bill data</param>
        /// <returns>true if new data has been updated or false if new data is not updated</returns>
        public bool EditExpense(int id, Bill updateBill)
        {
            bool isEdited = false;

            foreach(Bill b in bills)
            {
                if(b.Id == id)
                {
                    b.Date = updateBill.Date;
                    b.Amount = updateBill.Amount;
                    b.Category = updateBill.Category;
                    isEdited = true;
                }
            }
            return isEdited;
        }

        /// <summary>
        /// Method that handle deleting a data
        /// </summary>
        /// <param name="id">bill id</param>
        /// <returns>true if new data has been deleted or false if new data is not deleted</returns>
        public bool DeleteExpense(int id)
        {
            Bill delete = null;
            foreach(Bill b in bills)
            {
                if(id == b.Id)
                {
                    delete = b;
                    break;
                }
            }
            if(delete != null) {
                bills.Remove(delete);
            }
            return delete == null;
        }

        /// <summary>
        /// Method that handle simple data analysis.
        /// </summary>
        public void Analysis()
        {
            var totalBills = bills.Count();
            var totalAmount = bills.Sum(b => b.Amount);
            var averageAmount = bills.Average(b => b.Amount);
            var minAmount = bills.Min(b => b.Amount);
            var maxAmount = bills.Max(b => b.Amount);

            Console.WriteLine("Total Number of Bills: " + totalBills);
            Console.WriteLine("Total Amount: " + totalAmount);
            Console.WriteLine("Average Bill Amount: " + averageAmount);
            Console.WriteLine("Minimum Bill Amount: " + minAmount);
            Console.WriteLine("Maximum Bill Amount: " + maxAmount);
        }
    }
}

