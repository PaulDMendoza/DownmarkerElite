using System.Linq;
using DownMarker.Core;
using NUnit.Framework;

namespace DownMarker.Tests
{
   /// <summary>
   /// Test cases for <see cref="DownMarker.Core.RecentlyUsed"/>.
   /// </summary>
   [TestFixture]
   public class PersistentStateTests
   {
      private PersistentState persistentState;
          
      [SetUp]
      public void SetUp()
      {
         this.persistentState = new PersistentState(new FakeRegistry());
      }

      [Test]
      public void Touch_bumps_item_to_top()
      {
         persistentState.TouchRecentlyUsed("foo");
         persistentState.TouchRecentlyUsed("bar");
         persistentState.TouchRecentlyUsed("baz");
         persistentState.TouchRecentlyUsed("bar");

         var list = persistentState.RecentlyUsedDocuments.ToList();
         Assert.AreEqual("bar", list[0]);
         Assert.AreEqual("baz", list[1]);
         Assert.AreEqual("foo", list[2]);
      }
   }
}
