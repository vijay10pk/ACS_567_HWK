using System;
using HWK4.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HWK6.Pages.ExpenseAnalysis
{
    /// <summary>
    /// Class that handles analysis
    /// </summary>
	public class AnalysisModel : PageModel
	{
        public BillAnalysis billAnalysis = new();

        /// <summary>
        /// HTTP GET call for getting the simple data analysis of bill data
        /// </summary>
        public async void OnGet()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5026");
                //HTTP  GET
                var responseTask = client.GetAsync("http://localhost:5082/expense/analysis");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    billAnalysis = JsonConvert.DeserializeObject<BillAnalysis>(readTask);
                }
            }
        }
    }
}

