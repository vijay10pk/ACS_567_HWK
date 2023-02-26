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

        /// <summary>
        /// Method to Get the Bill for the given bill ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bill data of the given id</returns>
        public Bill GetExpenseById(int id)
        {
            return _context.Bill.Where(bill => bill.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Method for checking whether the bill data exist for the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            bill.Date = DateTime.UtcNow;
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
            updateBill.Date = DateTime.UtcNow;
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
        public Dictionary<string, dynamic> Analysis()
        {
            var billAnalysis = new Dictionary<string, dynamic>();
            List<double> allBills = _context.Bill.ToList()
                    .Select(a => a.Amount)
                    .ToList();
            billAnalysis.Add("totalBills", allBills.Count());
            billAnalysis.Add("totalAmount", allBills.Sum());
            billAnalysis.Add("averageAmount", allBills.Average());
            billAnalysis.Add("minimumAmount", allBills.Min());
            billAnalysis.Add("maximumAmount", allBills.Max());

            return billAnalysis;
        }

        /// <summary>
        /// Save the changes to the database
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved == 1;
        }
    }
}

