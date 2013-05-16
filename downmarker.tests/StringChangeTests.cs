using DownMarker.Core;
using NUnit.Framework;

namespace Downmarker.Tests
{
    [TestFixture]
    public class StringChangeTests
    {
        [Test]
        public void Prefix()
        {
            var edit = StringChange.GetStringChange("foo", "barfoo");
            Assert.AreEqual("barfoo", edit.Do("foo"));
            Assert.AreEqual("foo", edit.Undo("barfoo"));
        }

        [Test]
        public void Postfix()
        {
            var edit = StringChange.GetStringChange("foo", "foobar");
            Assert.AreEqual("foobar", edit.Do("foo"));
            Assert.AreEqual("foo", edit.Undo("foobar"));
        }

        [Test]
        public void Insert()
        {
            var edit = StringChange.GetStringChange("aaabbb", "aaaxxxbbb");
            Assert.AreEqual("aaaxxxbbb", edit.Do("aaabbb"));
            Assert.AreEqual("aaabbb", edit.Undo("aaaxxxbbb"));
        }

        [Test]
        public void Remove()
        {
            var edit = StringChange.GetStringChange("aaaxxxbbb", "aaabbb");
            Assert.AreEqual("aaabbb", edit.Do("aaaxxxbbb"));
            Assert.AreEqual("aaaxxxbbb", edit.Undo("aaabbb"));
        }

        [Test]
        public void Replace()
        {
            var edit = StringChange.GetStringChange("aaabbbccc", "aaaxxxccc");
            Assert.AreEqual("aaaxxxccc", edit.Do("aaabbbccc"));
            Assert.AreEqual("aaabbbccc", edit.Undo("aaaxxxccc"));
        }

        [Test]
        public void Remove_bar_from_foobarbaz()
        {
            var edit = StringChange.GetStringChange("foobarbaz", "foobaz");
            Assert.AreEqual("foobaz", edit.Do("foobarbaz"));
            Assert.AreEqual("foobarbaz", edit.Undo("foobaz"));
        }
    }
}
