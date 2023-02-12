using System;
using HWK4.Data;
using HWK4.Models;

namespace HWK4
{
	public class Seed
	{
		private readonly DataContext dataContext;
        List<Bill> bills;

        public Seed(DataContext dataContext)
		{
			this.dataContext = dataContext;
		}

        /// <summary>
        /// This method get the data from the ReadDataFromCsv method and store it in the database
        /// </summary>
		public void SeedDataContext()
		{
			if (!dataContext.Bill.Any())
			{
                bills= new();
                ReadDataFromCsv();

                dataContext.Bill.AddRange(bills);
                dataContext.SaveChanges();
            }
		}

        /// <summary>
        /// Method that read data from the csv file.
        /// </summary>
        private void ReadDataFromCsv()
        {
            try
            {
                using (StreamReader sr = new StreamReader("./Data/bills.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        bills.Add(new Bill(DateTime.Parse(line[0]),
                            double.Parse(line[1]),
                            line[2]));
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            //return bills;

        }

    }
}

