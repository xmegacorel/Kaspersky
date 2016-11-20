using First;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Should;
using Xunit.Abstractions;

namespace UnitTestProject
{
    public class KasperskyTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public KasperskyTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData(10000, (int)(10000 * 0.1))]
        [InlineData(10000, (int)(10000 * 0.8))]
        public void SumPair_GenerateElementsAndProcess_Succed(int last, int x)
        {
            var seq = Enumerable.Range(0, last);

            var timeFast = GetExecutedTime(() => new SumPair(seq, x).ToString());
            var timeSlow = GetExecutedTime(() => new SumPairSlow(seq, x).ToString());

            Assert.Equal(timeSlow.ResultValue, timeFast.ResultValue);
            
            _outputHelper.WriteLine("fast: {0}, slow: {1}", timeFast.ExecutedTime, timeSlow.ExecutedTime);
            
            timeSlow.ExecutedTime.ShouldBeGreaterThan(timeFast.ExecutedTime);
        }

        private static Result<T> GetExecutedTime<T>(Func<T> action)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = action();
            sw.Stop();

            return new Result<T>() { ExecutedTime = sw.ElapsedMilliseconds, ResultValue = result};
        }

        class Result<T>
        {
            public long ExecutedTime { get; set; }
            public T ResultValue { get; set; }
        }
    }
}
