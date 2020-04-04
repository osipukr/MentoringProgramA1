using System;
using System.Linq;
using SampleQueries.Data;

namespace SampleQueries
{
	[Title("06 LINQ")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{
		private readonly DataSource _dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
			foreach (var x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return return all presented in market products")]

		public void Linq2()
		{
			var products =
				from p in _dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}
	}
}