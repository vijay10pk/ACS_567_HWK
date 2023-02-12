using System;
using HWK4.Models;

namespace HWK4.Interfaces
{
	/// <summary>
	/// Declaration of all the methods in ExpenseAnalysisRepository
	/// </summary>
	public interface IExpenseAnalysisRepository
	{
		ICollection<Bill> GetExpenses();
		Bill GetExpenseById(int id);
        bool BillExists(int id);
		bool AddExpense(Bill bill);
		bool EditExpense( Bill updateBill);
		bool DeleteExpense(int id);
		void Analysis();

    }
}

