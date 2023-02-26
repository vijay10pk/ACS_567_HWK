using System;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HWK4.Models;

namespace HWK6.Pages.ExpenseAnalysis
{
    /// <summary>
    /// Class that handles adding new bills
    /// </summary>
    public class AddBillModel : PageModel
    {
        public Bill bill = new();
        public string errorMessage = "";
        public string successMessage = "";
        /// <summary>
        /// Method for adding new bill by HTTP POST call
        /// </summary>
        public async void OnPost()
        {
            bill.Amount = double.Parse(Request.Form["amount"]);
            bill.Category = Request.Form["category"];
            bill.Date = DateTime.UtcNow;
            if (bill.Amount == null && bill.Category.Length == 0)
            {
                errorMessage = "Enter a valid bill amount";
            }
            else
            {
                var opt = new JsonSerializerOptions() { WriteIndented = true };
                string json = System.Text.Json.JsonSerializer.Serialize<Bill>(bill, opt);
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri("http://localhost:5026");
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("http://localhost:5082/api/Expense", content);

                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);

                    if (!result.IsSuccessStatusCode)
                    {
                        errorMessage = "Error adding";
                    }
                    else
                    {
                        successMessage = "Successfully added";
                    }
                }
            }
        }
    }
}

