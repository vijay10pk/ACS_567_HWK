using System;
using System.Globalization;
using HWK4.Models;
using HWK4.Interfaces;
using HWK4.Data;

namespace HWK4.Repository
{
    public class ExpenseAnalysisRepository : IExpenseAnalysisRepository
    {
        private DataContext _context;

        public ExpenseAnalysisRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that handle get all data
        /// </summary>
        /// <returns>bills</returns>
        public ICollection<Bill> GetExpenses()
        {
            return _context.Bill.ToList();
        }

        public Bill GetExpenseById(int id)
        {
            return _context.Bill.Where(bill => bill.Id == id).FirstOrDefault();
        }

        public bool BillExists(int id)
        {
            return _context.Bill.Any(bill => bill.Id == id);
        }

        /// <summary>
        /// Method to handle create new data.
        /// </summary>
        /// <param name="bill">new bill data</param>
        /// <returns>true if new data has been created or false if new data is not created</returns>
        public bool AddExpense(Bill bill)
        {
            _context.Add(bill);
            return Save();
        }

        /// <summary>
        /// Method that handle updating a data
        /// </summary>
        /// <param name="id">bill id</param>
        /// <param name="updateBill">updated bill data</param>
        /// <returns>true if new data has been updated or false if new data is not updated</returns>
        public bool EditExpense(Bill updateBill)
        {
            _context.Update(updateBill);
            return Save();
        }

        /// <summary>
        /// Method that handle deleting a data
        /// </summary>
        /// <param name="id">bill id</param>
        /// <returns>true if new data has been deleted or false if new data is not deleted</returns>
        public bool DeleteExpense(int id)
        {
            _context.Remove(GetExpenses().FirstOrDefault(a => a.Id == id)); 
            return Save();
        }

        /// <summary>
        /// Method that handle simple data analysis.
        /// </summary>
        public void Analysis()
        {
            var totalBills = _context.Bill.Count();
            var totalAmount = _context.Bill.Sum(b => b.Amount);
            var averageAmount = _context.Bill.Average(b => b.Amount);
            var minAmount = _context.Bill.Min(b => b.Amount);
            var maxAmount = _context.Bill.Max(b => b.Amount);

            Console.WriteLine("Total Number of Bills: " + totalBills);
            Console.WriteLine("Total Amount: " + totalAmount);
            Console.WriteLine("Average Bill Amount: " + averageAmount);
            Console.WriteLine("Minimum Bill Amount: " + minAmount);
            Console.WriteLine("Maximum Bill Amount: " + maxAmount);
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved == 1;
        }
    }
}

