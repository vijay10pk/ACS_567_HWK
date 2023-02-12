using System;
using Microsoft.EntityFrameworkCore;
using HWK4.Models;
namespace HWK4.Data
{
	/// <summary>
	/// Initialize new instance with database
	/// </summary>
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<Bill> Bill { get; set; }
	}
}

