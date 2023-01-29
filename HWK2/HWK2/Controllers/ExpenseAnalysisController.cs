using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HWK2.Models;
using HWK2.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace HWK2.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        //Create an object 
        private readonly ILogger<ExpenseController> _logger;
        /// <summary>
        /// constructor to initialize the object
        /// </summary>
        /// <param name="logger"></param>
        public ExpenseController(ILogger<ExpenseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// /api/Expense - Gets all data from the file
        /// </summary>
        /// <returns>all fetched bill data</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Bill>))]
        public IActionResult GetExpenses()
        {
            return Ok(ExpenseAnalysisRepository.getInstance().GetExpenses());
        }

        /// <summary>
        /// /api/Expense/{id} - end point for getting bill data for the given id
        /// </summary>
        /// <param name="id">bill id</param>
        /// <returns>fetched bill data of the given id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Bill))]
        [ProducesResponseType(404)]
        public IActionResult GetExpense(int id)
        {
            Bill expense = ExpenseAnalysisRepository.getInstance().GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(expense);
            }
            
        }

        /// <summary>
        /// /api/Expense - end point for Create/Add new bill data
        /// </summary>
        /// <param name="expense">new bill data</param>
        /// <returns>success or failure response</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddExpense([FromBody] Bill expense)
        {
            if(expense == null)
            {
                return BadRequest("Expense is null");
            }
            bool result = ExpenseAnalysisRepository.getInstance().AddExpense(expense);
            if(result)
            {
                return Ok("Successfully added");
            } else
            {
                return BadRequest("Expense not added");
            }
            
        }

        /// <summary>
        /// /api/Expense/{id} - end point for updating the data for the given bill id
        /// </summary>
        /// <param name="id">bill id</param>
        /// <param name="expense">updated bill data</param>
        /// <returns>success or failure response</returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult EditExpense(int id, [FromBody] Bill expense)
        {
            if(expense == null)
            {
                return BadRequest("Todo is null");
            }
            bool isUpdated = ExpenseAnalysisRepository.getInstance().EditExpense(id, expense);
            if(!isUpdated)
            {
                return NotFound("No matching expense");
            }
            else
            {
                return Ok("Successfully updated");
            }
        }

        /// <summary>
        /// /api/Expense/{id} -  End point for deleting the data based on bill id
        /// </summary>
        /// <param name="id">bill id</param>
        /// <returns>success or failure response</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            bool deleted = ExpenseAnalysisRepository.getInstance().DeleteExpense(id);

            if (!deleted)
            {
                return NotFound("No matching id");
            }
            else
            {
                return Ok("Bill deleted");
            }
        }

        /// <summary>
        /// /api/expense/analysis -  End point for getting result of the data analysis.
        /// </summary>
        /// <returns>Result of data analyis</returns>
        [HttpGet]
        [Route("/expense/analysis")]
        public IActionResult GetExpenseAnalysis()
        {
            var expenses = ExpenseAnalysisRepository.getInstance().GetExpenses();
            var totalBills = expenses.Count;
            var totalAmount = expenses.Sum(e => e.Amount);
            var averageBillAmount = totalAmount / totalBills;
            var minimumBillAmount = expenses.Min(e => e.Amount);
            var maximumBillAmount = expenses.Max(e => e.Amount);
            var analysis = new
            {
                totalBills,
                totalAmount,
                averageBillAmount,
                minimumBillAmount,
                maximumBillAmount
            };
            return Ok(analysis);
        }

    }
}

