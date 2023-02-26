using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using HWK4.Models;

namespace HWK6.Pages.ExpenseAnalysis
{
    /// <summary>
    /// Class that handles the getting and displaying all the bill data
    /// </summary>
    public class IndexModel : PageModel
    {
        public List<Bill> Expenses = new();
        /// <summary>
        /// HTTP GET call that get all the bill data
        /// </summary>
        public async void OnGet() {
            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri("http://localhost:5026");
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:5082/api/Expense");
                Console.WriteLine(responseTask);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    Expenses = JsonConvert.DeserializeObject<List<Bill>>(readTask);
                }
            }
        }
    }
}

