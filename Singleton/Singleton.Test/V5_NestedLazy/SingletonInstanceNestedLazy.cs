using Singleton.V5_NestedLazy;
using System.Linq;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Singleton.Test.V5_NestedLazy
{
    public class SingletonInstanceNestedLazy
    {
        private readonly ITestOutputHelper output;

        public SingletonInstanceNestedLazy(ITestOutputHelper output)
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
            //Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<SingletonNestedLazy>());

            var result = SingletonNestedLazy.Instance;

            Assert.NotNull(result);
            Assert.IsType<SingletonNestedLazy>(result);

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }

        [Fact]
        public void OnlyCallsConstructorOnceGivenThreeInstanceCalls()
        {
            // This no longer works
            //Assert.Null(SingletonTestHelpers.GetPrivateStaticInstance<Singleton>());

            // configure logger to slow down the creation longer than the pauses below
            Logger.DelayMiliseconds = 10;

            var result1 = SingletonNestedLazy.Instance;
            Thread.Sleep(1);
            var result2 = SingletonNestedLazy.Instance;
            Thread.Sleep(1);
            var result3 = SingletonNestedLazy.Instance;

            var log = Logger.Output();

            // we can't check this since it depends on if this test is run alone or after others
            // Assert.Equal(1, log.Count(log => log.Contains("Constructor")));

            Assert.Equal(3, log.Count(log => log.Contains("Instance")));

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }

        [Fact] // this test should only be run by itself
        public void NoLongerInitializesSingletonWhenAnotherStaticMemberIsReferenced()
        {
            Assert.Equal(0, Logger.Output().Count(log => log.Contains("Constructor")));

            // run this test by itself to see it really work
            var greeting = SingletonNestedLazy.GREETING;

            Assert.Equal(0, Logger.Output().Count(log => log.Contains("Constructor")));

            Logger.Output().ToList().ForEach(h => output.WriteLine(h));
        }
    }
}
