/*
 * A C# console application that reads, add, analyze and filters monthly bill data from a CSV file with menu driven interface and object oriented approach.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Bill class with getter and setter.
/// </summary>
class Bill
{
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }
}

class Program
{
    //List to store bill data
    static List<Bill> bills = new List<Bill>();
    //Path of the file that has the bill data
    static string filePath = "../../../Data/bills.csv";

    static void Main(string[] args)
    {
        ReadDataFromFile();

        // Menu Loop that display the option
        while(true)
        {
            Console.WriteLine("---Monthly Bill Analysis---");
            Console.WriteLine("1. View Bills");
            Console.WriteLine("2. Add Bill");
            Console.WriteLine("3. Analyze Data");
            Console.WriteLine("4. Filter Bills");
            Console.WriteLine("5. Exit");

            Console.WriteLine("Enter your Choice: ");
            int choice;
            //Validate menu choice
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid choice. Enter a number between 1 to 5");
                Console.WriteLine("Enter your choice: ");
            }
            switch(choice)
            {
                case 1:
                    ViewBills();
                    break;
                case 2:
                    AddNewBill();
                    break;
                case 3:
                    AnalyzeData();
                    break;
                case 4:
                    FilterBills();
                    break;
                case 5:
                    SaveDataToFile();
                    return;
            }
        }
    }

    /// <summary>
    ///This Method will read data from the CSV file
    /// </summary>
    static void ReadDataFromFile()
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    bills.Add(new Bill()
                    {
                        Date = DateTime.Parse(line[0]),
                        Amount = double.Parse(line[1]),
                        Category = line[2]
                    });
                }
            }
        }

        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }

    /// <summary>
    /// This method will save new bill data to the file and exit logic.
    /// </summary>
    static void SaveDataToFile()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach(Bill bill in bills)
                {
                    sw.WriteLine(bill.Date.ToString() + "," + bill.Amount + "," + bill.Category);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }


    /// <summary>
    /// This method will display the bill data from the CSV file.
    /// </summary>
    static void ViewBills()
    {
        Console.WriteLine("----------------------Bills----------------------");
        foreach(Bill bill in bills)
        {
            Console.WriteLine(bill.Date.ToString("MM/dd/yyyy") + " | $" + bill.Amount + " | " + bill.Category);
        }
        Console.WriteLine("----------------------xxxxx----------------------");
    }

    /// <summary>
    /// This method will get new bill data from the user and add it to the bills.csv file with input validation.
    /// </summary>
    static void AddNewBill()
    {
        Console.WriteLine("--- Enter the New Bill Details ---")
        Console.Write("Enter the date of the bill(MM/DD/YYYY): ");
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("Invalid date format. Enter a date in the format MM/DD/YYYY format.");
            Console.Write("Enter the date of the bill(MM/DD/YYYY): ");
        }

        Console.Write("Enter the bill amount: $");
        double amount;
        while(!double.TryParse(Console.ReadLine(), out amount))
        {
            Console.WriteLine("Invalid bill amount. Enter a valid number.");
            Console.Write("Enter the bill amount: $");

        }

        Console.Write("Enter the category of your expense: ");
        string category = Console.ReadLine();

        bills.Add(new Bill() { Date = date, Amount = amount, Category = category });
        Console.WriteLine("Data added successfully!!");
    }

    /// <summary>
    /// This method performs simple data analysis like average, total, maximum and minimum value from the bill data.
    /// </summary>
    static void AnalyzeData()
    {
        Console.WriteLine("--- Bill Analysis ---");
        Console.WriteLine("Total number of bills: " + bills.Count); //Calculate the total number of bills in bills.csv file.
        Console.WriteLine("Total amount: $" + bills.Sum(bill => bill.Amount)); //Calculate the total amount of all the bills.
        Console.WriteLine("Average bill amount: $" + bills.Average(bill => bill.Amount)); //Calculate the average bill amount.
        Console.WriteLine("Minimum bill amount: $" + bills.Min(bill => bill.Amount)); //Find the minimum bill amount.
        Console.WriteLine("Maximum bill amount: $" + bills.Max(bill => bill.Amount)); //Find the maximum bill amount.

    }

    /// <summary>
    /// This method provide option for the user to filter bills based on date range and category of their expenditure.
    /// </summary>
    static void FilterBills()
    {
        Console.WriteLine("--- Filter Bills by ---");
        Console.WriteLine("1. Filter by date range");
        Console.WriteLine("2. Filter by category");
        Console.Write("Enter your choice for filtering: ");
        int choice;
        while(!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("Invalid choice.Enter a number between 1 and 2");
            Console.Write("Enter your choice for filtering: ");

        }

        switch(choice)
        {
            //Logic for filtering bill data based on date range with input validation.
            case 1:
                Console.Write("Enter start date(MM/DD/YYYY): ");
                DateTime startDate;
                while (!DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    Console.WriteLine("Invalid start date format. Enter a date in the format MM/DD/YYYY format.");
                    Console.Write("Enter a valid start date (MM/DD/YYYY): ");
                }

                DateTime endDate;
                while (!DateTime.TryParse(Console.ReadLine(), out endDate))
                {
                    Console.WriteLine("Invalid end date format. Enter a date in the format MM/DD/YYYY format.");
                    Console.Write("Enter a valid end date (MM/DD/YYYY): ");
                }

                List<Bill> filterbills = bills.Where(bill => bill.Date >= startDate && bill.Date <= endDate).ToList();
                if(filterbills.Count == 0)
                {
                    Console.WriteLine("No bills found in the specified date range");
                }
                else
                {
                    Console.WriteLine("--- Filtered Bills by Date Range ---");
                    foreach(Bill bill in filterbills)
                    {
                        Console.WriteLine(bill.Date.ToString("MM/dd/YYYY") + "| $" + bill.Amount + "|" + bill.Category);
                    }
                    Console.WriteLine("--- xxxxxxxxxxxxxxxxxxxxxxxxxxx ---");
                }
                break;

            //Logic for filtering bill data based on category with input validation.
            case 2:
                Console.Write("Enter Category:");
                string category = Console.ReadLine();

                List<Bill> filteredBillsCategory = bills.Where(bill => bill.Category.ToUpper() == category.ToUpper()).ToList();
                if(filteredBillsCategory.Count == 0)
                {
                    Console.WriteLine("No bills found with the specified category");
                }
                else
                {
                    Console.WriteLine("--- Filtered Bills by Category ---");
                    foreach (Bill bill in filteredBillsCategory)
                    {
                        Console.WriteLine(bill.Date.ToString("MM/dd/YYYY") + "| $" + bill.Amount + "|" + bill.Category);
                    }
                    Console.WriteLine("--- xxxxxxxxxxxxxxxxxxxxxxxxxxx ---");
                }
                break;
        }

    }
}