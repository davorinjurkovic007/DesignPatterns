using System;
using System.Collections;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
			//foreach (Stock s in SampleData.getNext())
			//{
			//	// Reactive Filters
			//	if (s.Symbol == "GOOG")
			//		Console.WriteLine("Google's new price is: {0}", s.Price);

			//	if (s.Symbol == "MSFT" && s.Price > 10.00m)
			//		Console.WriteLine("Microsoft has reached the target price: {0}", s.Price);

			//	if (s.Symbol == "XOM")
			//		Console.WriteLine("Exxon mobile's price is {0}", s.Price);
			//}

			// Create observable weather station
			WeatherStation station = new WeatherStation();

			// Create two observers
			WeatherDataPrinter printer = new WeatherDataPrinter();
			WeatherDataAggregator aggregator = new WeatherDataAggregator();

			// Add the observers
			IDisposable unsubber1 = station.Subscribe(printer);
			IDisposable unusbber2 = station.Subscribe(aggregator);

            station.PrintObservable();


			// Pretend that new data arrives to the station
			station.AddData(new WeatherData("Temperature", 10));
			station.AddData(new WeatherData("Temperature", 11));
			station.AddData(new WeatherData("Temperature", 12));

			// Print result of aggregator observer
			Console.WriteLine("Final average is: " + aggregator.GetAverage());
		}
    }

	class WeatherDataPrinter : IObserver<WeatherData>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Received all data from WeatherStation.");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Exception occurred!");
        }

        public void OnNext(WeatherData value)
        {
            Console.WriteLine(value.Name + ":" + value.Value);
        }

        public void Update(WeatherData wd)
        {
			Console.WriteLine(wd.Name + ":" + wd.Value);
        }
    }

	class WeatherDataAggregator : IObserver<WeatherData>
    {
		int sum = 0;
		int n = 0;
		double avg = 0;
        bool errorHasOccured = false;

		public void OnNext(WeatherData wd)
        {
			this.n += 1;
			this.sum += wd.Value;
			this.avg = (double)this.sum / this.n;
			Console.WriteLine("Running average: " + this.avg);
        }

		public double GetAverage()
        {
			return this.avg;
        }

        public void OnCompleted()
        {
            Console.WriteLine("Received all data from WeatherStation.");
        }

        public void OnError(Exception error)
        {
            this.errorHasOccured = true;
            Console.WriteLine("Exception occurred!");
        }
    }

    class WeatherStation : IObservable<WeatherData>
    {
        ICollection<WeatherData> data = new List<WeatherData>();
        ICollection<IObserver<WeatherData>> observers = new List<IObserver<WeatherData>>();

        public void AddData(WeatherData wd)
        {
            this.data.Add(wd);
            this.Notify(wd);
        }

        // Observer pattern stuff below:
        public void AddObserver(IObserver<WeatherData> o)
        {
            this.observers.Add(o);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(WeatherData value)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<WeatherData> observer)
        {
            this.observers.Add(observer);
            return new Unsubscriber<IObserver<WeatherData>>(this.observers, observer);
        }

        public void PrintObservable()
        {

            foreach(var o in this.observers)
            {
                Console.WriteLine(o);
            }
        }

        private void Notify(WeatherData wd)
        {
            foreach (var o in this.observers)
            {
                o.OnNext(wd);
            }
        }
    }

    public class Unsubscriber<T> : IDisposable
    {
        private ICollection<T> items;
        private T item;

        public Unsubscriber(ICollection<T> items, T item)
        {
            this.items = items;
            this.item = item;
        }

        public void Dispose()
        {
            if (this.items.Contains(this.item))
                this.items.Remove(this.item);
        }
    }
    

	class WeatherData
    {
		public string Name { get; private set; }
		public int Value { get; private set; }
        public WeatherData(string name, int val)
        {
			this.Name = name;
			this.Value = val;
        }

        public override string ToString()
        {
			return this.Name + ": " + this.Value;
        }
    }

}
