using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.ComponentModel;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using HWK4.Models;
using System.Text.Json;

namespace HWK6.Pages.ExpenseAnalysis
{
    /// <summary>
    /// Class that handles the edit functionality
    /// </summary>
    public class EditBillModel : PageModel
    {
        public Bill bill = new();
        public string errorMessage = "";
        public string successMessage = "";

        /// <summary>
        /// Get the bill data by ID for the selected bill
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
        /// Update the bill of the selected ID
        /// </summary>
        public async void OnPost()
        {
            bill.Id = int.Parse(Request.Form["id"]);
            bill.Amount = double.Parse(Request.Form["amount"]);
            bill.Category = Request.Form["category"];
            if (bill.Amount == null && bill.Category.Length == 0)
            {
                errorMessage = "Description is required";
            }
            else
            {
                var opt = new JsonSerializerOptions() { WriteIndented = true };
                string json = System.Text.Json.JsonSerializer.Serialize<Bill>(bill, opt);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5026");
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var result = await client.PutAsync("http://localhost:5082/api/Expense", content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);
                    if (!result.IsSuccessStatusCode)
                    {
                        errorMessage = "Error editing";
                    }
                    else
                    {
                        successMessage = "Successfully edited";
                    }
                }
            }
        }
    }
}

