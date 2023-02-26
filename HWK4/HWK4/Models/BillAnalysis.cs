using System;
namespace HWK4.Models
{
    /// <summary>
    /// Constructor for bill analysis data
    /// </summary>
	public class BillAnalysis
	{
            public int totalBills { get; set; } = 0;
            public double totalAmount { get; set; } = 0;
            public double averageAmount { get; set; } = 0;
            public int minimumAmount { get; set; } = 0;
            public int maximumAmount { get; set; } = 0;
    }

}

