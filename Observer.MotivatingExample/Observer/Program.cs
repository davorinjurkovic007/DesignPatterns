using System;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
			foreach (Stock s in SampleData.getNext())
			{
				// Reactive Filters
				if (s.Symbol == "GOOG")
					Console.WriteLine("Google's new price is: {0}", s.Price);

				if (s.Symbol == "MSFT" && s.Price > 10.00m)
					Console.WriteLine("Microsoft has reached the target price: {0}", s.Price);

				if (s.Symbol == "XOM")
					Console.WriteLine("Exxon mobile's price is {0}", s.Price);
			}
		}
    }
}
