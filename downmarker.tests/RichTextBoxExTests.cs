using DownMarker.WinForms;
using NUnit.Framework;

namespace DownMarker.Tests
{
    [TestFixture]
    public class RichTextBoxExTests
    {

        [Test]
        public void Setting_text_keeps_selection()
        {
            var box = new RichTextBoxEx();
            box.Text = "foo bar baz";
            box.SelectionStart = 4;
            box.SelectionLength = 3;

            box.Text = box.Text + " beh";

            Assert.AreEqual(4, box.SelectionStart);
            Assert.AreEqual(3, box.SelectionLength);
        }

        /* This test is disabled because the tested functionality appears
         * to require a message pump.
        [Test]
        public void Setting_text_keeps_vertical_scroll_pos()
        {
            var box = new RichTextBoxEx();
            box.Text = string.Join(
                "\n",
                Enumerable.Range(0, 100).Select(x=>x.ToString()).ToArray());
            
            box.VerticalScrollPosition = 50;
            box.Text = box.Text + "foo";
            Assert.AreEqual(50, box.VerticalScrollPosition);
        }*/ 
    }
}
