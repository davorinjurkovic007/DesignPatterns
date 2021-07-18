using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer.TemperatueMonitor
{
    class TemperatureReporter : IObserver<Temperature>
    {
        private IDisposable unsubscriber;
        private bool first = true;
        private Temperature last;

        public void OnCompleted()
        {
            Console.WriteLine("Additional temperature data will not be transmitted.");
        }

        public void OnError(Exception error)
        {
            // Do nothing.
        }

        public void OnNext(Temperature value)
        {
            Console.WriteLine("The temperature is {0}°C at {1:g}", value.Degrees, value.Date);
            if (first)
            {
                last = value;
                first = false;
            }
            else
            {
                Console.WriteLine("   Change: {0}° in {1:g}", value.Degrees - last.Degrees,
                                                              value.Date.ToUniversalTime() - last.Date.ToUniversalTime());
            }
        }

        public virtual void Subscribe(IObservable<Temperature> provider)
        {
            unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}
