using Singleton.V2_Locking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Singleton.Test.V2_Locking
{
    public class SingletonInstanceLocking
    {
        private readonly ITestOutputHelper output;

        public SingletonInstanceLocking(ITestOutputHelper output)
        {
            this.output = output;
            SingletonTestHelpers.Reset(typeof(SingletonLocking));
            Logger.Clear();
        }

        [Fact]
        public void ReturnsNonNullSingletonInstance()
        {
            Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<SingletonLocking>());

            var result = SingletonLocking.Instance;

            Assert.NotNull(result);
            Assert.IsType<SingletonLocking>(result);

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }

        [Fact]
        public void CallsConstructorMultipleTimesGibenThreeParallelInstanceCall()
        {
            Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<SingletonLocking>());

            // configure logger to slow down the creation longer than the puses below
            Logger.DelayMiliseconds = 10;

            var result1 = SingletonLocking.Instance;
            Thread.Sleep(1);
            var result2 = SingletonLocking.Instance;
            Thread.Sleep(1);
            var result3 = SingletonLocking.Instance;

            var log = Logger.Output();
            Assert.Equal(1, log.Count(log => log.Contains("Constructor")));
            Assert.Equal(3, log.Count(log => log.Contains("Instance")));

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }

        [Fact]
        public void CallConstructorMultipleTimesGivenThreeParallelInstanceCalls()
        {
            Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<SingletonLocking>());

            // configre logger to slow down the creation long wnough to cause problems
            Logger.DelayMiliseconds = 50;

            var strings = new List<string>() { "one", "two", "three" };
            var instances = new List<SingletonLocking>();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 3 };
            Parallel.ForEach(strings, options, insance =>
            {
                instances.Add(SingletonLocking.Instance);
            });

            var log = Logger.Output();
            try
            {
                Assert.Equal(1, log.Count(log => log.Contains("Constructor")));
                Assert.Equal(3, log.Count(log => log.Contains("Instance")));
            }
            finally
            {
                Logger.Output().ToList().ForEach(h => output.WriteLine(h));
            }
        }
    }
}
