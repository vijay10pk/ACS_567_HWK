using System;
using System.Text.Json;
using HWK4.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HWK6.Pages.ExpenseAnalysis
{
    /// <summary>
    /// Class that handles delete functionality
    /// </summary>
    public class DeleteModel : PageModel
    {
        public Bill bill = new();
        public string errorMessage = "";
        public string successMessage = "";
        /// <summary>
        /// Get the the bill data of the chosen id
        /// </summary>
        public async void OnGet()
        {
            string id = Request.Query["id"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5026");
                //HTTP  GET
                var responseTask = client.GetAsync("http://localhost:5082/api/Expense/" + id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    bill = JsonConvert.DeserializeObject<Bill>(readTask);
                }
            }
        }


        /// <summary>
        /// Delete the selected bill
        /// </summary>
        public async void OnPost()
        {
            bool isDeleted = false;
            int id = int.Parse(Request.Form["id"]);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5026");
                    var result = await client.DeleteAsync("http://localhost:5082/api/Expense/"+id);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    if (!isDeleted)
                    {
                        successMessage = "Successfully deleted";
                    }
                    else
                    {
                        errorMessage = "Error deleted";
                    }
                }
        }
    }
}

