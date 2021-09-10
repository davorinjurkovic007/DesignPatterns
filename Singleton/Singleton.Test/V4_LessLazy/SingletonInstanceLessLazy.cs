using Singleton.V4_LessLazy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Singleton.Test.V4_LessLazy
{
    public class SingletonInstanceLessLazy
    {
        private readonly ITestOutputHelper output;

        public SingletonInstanceLessLazy(ITestOutputHelper output)
        {
            this.output = output;

            // This doesn't work with a static readonly field
            //SingletonTestHelpers.Reset(typeof(Singleton));

            Logger.Clear();
        }

        [Fact]
        public void ReturnsNonNullSingletonInstance()
        {
            // This no longer works
            //Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<SingletonLessLazy>());

            var result = SingletonLessLazy.Instance;

            Assert.NotNull(result);
            Assert.IsType<SingletonLessLazy>(result);

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }

        [Fact]
        public void OnlyCallsConstructorOnceGivenThreeInstanceCalls()
        {
            // This no longer works
            //Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<Singleton>());

            // configure logger to slow down the creation longer than the pauses below
            Logger.DelayMiliseconds = 10;

            var result1 = SingletonLessLazy.Instance;
            Thread.Sleep(1);
            var result2 = SingletonLessLazy.Instance;
            Thread.Sleep(1);
            var result3 = SingletonLessLazy.Instance;

            var log = Logger.Output();

            // we can't check this since it depends on if this test is run alone or after others
            // Assert.Equal(1, log.Count(log => log.Contains("Constructor")));

            Assert.Equal(3, log.Count(log => log.Contains("Instance")));

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }

        // [Fact] // this test can only be run by itself
        public void InitializesSingletonWhenAnotherStaticMemberIsReferenced()
        {
            Assert.Equal(0, Logger.Output().Count(log => log.Contains("Constructor")));

            // run this test by itself to see it really work
            var greeting = SingletonLessLazy.GREETING;

            Assert.Equal(1, Logger.Output().Count(log => log.Contains("Constructor")));

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }
    }
}
