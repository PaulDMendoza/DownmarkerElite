using DownMarker.Core;
using NUnit.Framework;

namespace Downmarker.Tests
{
    [TestFixture]
    public class LinkedListHistoryTests
    {
        [Test]
        public void Limit_is_respected()
        {
            var history = new LinkedListHistory<string>(3);
            history.Add("1");
            history.Add("2");
            history.Add("3");
            history.Add("4");
            history.Back();
            history.Back();
            Assert.IsFalse(history.CanGoBack);
        }
    }
}
