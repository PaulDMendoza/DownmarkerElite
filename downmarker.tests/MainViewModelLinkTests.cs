using DownMarker.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace DownMarker.Tests
{
    [TestFixture]
    public class MainViewModelLinkTests : MainViewModelTestsBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void Link_does_nothing_if_prompt_cancelled()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(null);

            viewModel.MarkdownText = "the quick brown fox";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 11;
            viewModel.Link();

            Assert.AreEqual("the quick brown fox", viewModel.MarkdownText);
        }

        [Test]
        public void Link_replaces_selected_text()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(
                    new Link()
                    {
                        Text = "slow white",
                        Target = "http://www.example.com"
                    });

            viewModel.MarkdownText = "the quick brown fox";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 11;
            viewModel.Link();

            Assert.AreEqual(
                "the [slow white](http://www.example.com) fox",
                viewModel.MarkdownText);
        }

        [Test]
        [Ignore]
        public void Link_detects_existing_link()
        {
            viewModel.MarkdownText = "hello world [linktext](linktarget)";
            viewModel.SelectionStart = 14;
            viewModel.Link();

            promptStub.AssertWasCalled(x => x.EditLink(
                Arg<Link>.Matches(
                   y => y.Text == "linktext" && y.Target == "linktarget"),
                Arg<string>.Is.Anything,
                Arg.Is(false)));
        }

        [Test]
        [Ignore]
        public void Link_at_start_of_line_suggests_first_word()
        {
            viewModel.MarkdownText = "foo\r\nbar";
            viewModel.SelectionStart = 5;
            viewModel.Link();

            promptStub.AssertWasCalled(x => x.EditLink(
                Arg<Link>.Matches(y => y.Text == "bar"),
                Arg<string>.Is.Anything,
                Arg.Is(false)));
        }

        [Test]
        [Ignore]
        public void Link_suggests_selected_text()
        {
            viewModel.MarkdownText = "the quick brown fox";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 5;
            viewModel.Link();

            promptStub.AssertWasCalled(
                x => x.EditLink(
                    Arg<Link>.Matches(y => y.Text == "quick"),
                    Arg<string>.Is.Anything,
                    Arg.Is(false)));
        }

        [Test]
        public void Link_replaces_word_right_from_cursor()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(
                    new Link()
                    {
                        Text = "slow",
                        Target = "http://www.example.com"
                    });

            viewModel.MarkdownText = "the quick brown fox";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 0;
            viewModel.Link();

            Assert.AreEqual(
                "the [slow](http://www.example.com) brown fox",
                viewModel.MarkdownText);
        }

        [Test]
        public void Link_replaces_word_left_from_cursor()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(
                    new Link()
                    {
                        Text = "slow",
                        Target = "http://www.example.com"
                    });

            viewModel.MarkdownText = "the quick brown fox";
            viewModel.SelectionStart = 9;
            viewModel.SelectionLength = 0;
            viewModel.Link();

            Assert.AreEqual(
                "the [slow](http://www.example.com) brown fox",
                viewModel.MarkdownText);
        }

        [Test]
        public void Link_inserts_into_empty_document()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(
                    new Link()
                    {
                        Text = "hello",
                        Target = "http://www.example.com"
                    });

            viewModel.MarkdownText = "";
            viewModel.Link();

            Assert.AreEqual(
                "[hello](http://www.example.com)",
                viewModel.MarkdownText);
        }

        [Test]
        public void Link_on_first_word()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(
                    new Link()
                    {
                        Text = "hello",
                        Target = "http://www.example.com"
                    });

            viewModel.MarkdownText = "foo";
            viewModel.Link();

            Assert.AreEqual(
                "[hello](http://www.example.com)",
                viewModel.MarkdownText);
        }

        [Test]
        public void Link_inserts_at_end_of_document()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(
                    new Link()
                    {
                        Text = "hello",
                        Target = "http://www.example.com"
                    });

            viewModel.MarkdownText = "a ";
            viewModel.SelectionStart = 2;
            viewModel.Link();

            Assert.AreEqual(
                "a [hello](http://www.example.com)",
                viewModel.MarkdownText);
        }

        [Test]
        public void Link_selects_resulting_markdown()
        {
            promptStub.Stub(x => x.EditLink(null, null, false))
                .IgnoreArguments()
                .Return(new Link() { Target = "bar", Text = "foo" });

            viewModel.MarkdownText = "hello world";
            viewModel.SelectionStart = 6;
            viewModel.Link();

            string selected = viewModel.MarkdownText.Substring(
                viewModel.SelectionStart,
                viewModel.SelectionLength);
            Assert.AreEqual("[foo](bar)", selected);
        }

    }
}
