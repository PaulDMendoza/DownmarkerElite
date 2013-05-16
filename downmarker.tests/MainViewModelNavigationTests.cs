using System;
using NUnit.Framework;
using Rhino.Mocks;
using DownMarker.Core;

namespace DownMarker.Tests
{
    [TestFixture]
    public class MainViewModelNavigationTests : MainViewModelTestsBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void Navigate_to_relative_uri_shows_error_if_current_document_has_no_location()
        {
            viewModel.Navigate(new Uri("someRelativeUri", UriKind.Relative));

            promptStub.AssertWasCalled(
                x => x.ShowError(
                    Arg.Text.Contains("someRelativeUri"), Arg<string>.Is.Anything));
            uriHandlerStub.AssertWasNotCalled(
                x => x.OpenAbsoluteUri(Arg<Uri>.Is.Anything));
        }

        [Test]
        public void Navigate_resolves_relative_uri_with_document_location()
        {
            StubFile(@"c:\basepath\somedocument.md", "content");

            viewModel.Open(@"c:\basepath\somedocument.md");
            viewModel.Navigate(new Uri("someRelativeUri", UriKind.Relative));

            uriHandlerStub.AssertWasCalled(
                x => x.OpenAbsoluteUri(new Uri("file:///c:/basepath/someRelativeUri")));
        }

        [Test]
        public void Navigate_resolves_relative_uri_with_document_location_even_if_opened_with_relative_path()
        {
            fileSystemStub.BackToRecord();
            fileSystemStub.Replay();
            fileSystemStub.Stub(x => x.GetAbsoluteFilePath(null))
                .IgnoreArguments()
                .Return(@"c:\basepath\somedocument.md");
            StubFile(@"c:\basepath\somedocument.md", "content");

            viewModel.Open(@"somedocument.md");
            viewModel.Navigate(new Uri("someRelativeUri", UriKind.Relative));

            uriHandlerStub.AssertWasCalled(
                x => x.OpenAbsoluteUri(new Uri("file:///c:/basepath/someRelativeUri")));
        }

        // Test work-around for questionable WebBrowser behavior: all relative links
        // are turned into "about:" links if the document URI is unknown.
        // MainFormViewModel.Navigate reverses this by interpreting "about:" links as
        // relative links again.
        [Test]
        public void Navigate_interpretes_about_uri_as_relative_uri()
        {
            StubFile("c:\\basepath\\somedocument.md", "content");

            viewModel.Open("c:\\basepath\\somedocument.md");
            viewModel.Navigate(new Uri("about:someRelativeUri"));

            uriHandlerStub.AssertWasCalled(
                x => x.OpenAbsoluteUri(new Uri("file:///c:/basepath/someRelativeUri")));
        }

        [Test]
        public void Navigate_to_file_URI_for_markdown_document_opens_it()
        {
            StubFile("c:\\foo.md", "content");

            viewModel.Navigate(new Uri("file:///c:/foo.md"));

            Assert.AreEqual("content", viewModel.MarkdownText);
        }

        [Test]
        public void Cannot_execute_back_initially()
        {
            Assert.IsFalse(viewModel.CanGoBack);
        }

        [Test]
        public void Can_execute_back_after_navigate()
        {
            StubFile("c:\\foo.md", "foo content");

            viewModel.Navigate(new Uri("file:///c:/foo.md"));
            Assert.IsTrue(viewModel.CanGoBack);
        }

        [Test]
        public void Start_page_does_not_appear_in_recently_opened()
        {
            StubFile("c:\\foo.md", "foo content");
            viewModel.Navigate(new Uri("file:///c:/foo.md"));
            viewModel.GoBack(); // to start page
            Assert.IsFalse(viewModel.MarkdownText.Contains("downmarker:start"));
        }

        [Test]
        public void Can_execute_back_after_open_and_navigate()
        {
            StubFile("c:\\foo.md", "foo content");
            StubFile("c:\\bar.md", "bar content");

            viewModel.Open("c:\\foo.md");
            viewModel.Navigate(new Uri("bar.md", UriKind.Relative));
            viewModel.GoBack();

            Assert.AreEqual("foo content", viewModel.MarkdownText);
        }

        [Test]
        public void Can_execute_forward_after_open_and_navigate_and_back()
        {
            StubFile("c:\\foo.md", "foo content");
            StubFile("c:\\bar.md", "bar content");

            viewModel.Open("c:\\foo.md");
            viewModel.Navigate(new Uri("bar.md", UriKind.Relative));
            viewModel.GoBack();
            viewModel.GoForward();

            Assert.AreEqual("bar content", viewModel.MarkdownText);
        }

        [Test]
        public void SaveAs_adds_navigation_point()
        {
            StubSaveAsReply("c:\\foo.md");
            StubFile("c:\\bar.md", "bar content");
            StubFile("c:\\foo.md", "foo content");

            viewModel.MarkdownText = "foo content";
            viewModel.SaveAs();
            viewModel.Open("c:\\bar.md");
            viewModel.GoBack();

            fileSystemStub.AssertWasCalled(x => x.ReadTextFile("c:\\foo.md"));
        }

        [Test]
        public void SaveAs_with_same_name_does_not_add_navigation_point()
        {
            StubFile("c:\\foo.md", "foo content");
            StubSaveAsReply("c:\\foo.md");

            viewModel.Open("c:\\foo.md");
            viewModel.SaveAs();

            viewModel.GoBack(); // should go to start page
            Assert.IsFalse(viewModel.CanGoBack);
        }

        [Test]
        public void Navigate_ignores_empty_file_URL()
        {
            // navigation events for these urls happen on mono
            // each time WebBrowser.DocumentStream is assigned,
            // and must be ignored
            this.viewModel.Navigate(new Uri("file:///"));

            this.uriHandlerStub.AssertWasNotCalled(
            x => x.OpenAbsoluteUri(Arg<Uri>.Is.Anything));
        }

        [Test]
        public void CanGoBack_after_creating_new_document_at_startpage()
        {
           viewModel.New();
           Assert.IsTrue(viewModel.CanGoBack);
        }

        [Test]
        public void Open_after_new_replaces_empty_navigation_point()
        {
           StubFile("c:\\foo.md", "foo content");
           StubOpenFileReply("c:\\foo.md");

           viewModel.New();
           viewModel.Open();

           viewModel.GoBack(); // should go back to start page
           Assert.IsFalse(viewModel.CanGoBack);
        }

        [Test]
        public void Cannot_go_forward_to_unsaved_new_document()
        {
           promptStub.Stub(x => x.QuestionSaveUnsavedChanges()).Return(PromptResult.No);

           viewModel.New();
           viewModel.GoBack();
           Assert.IsFalse(viewModel.CanGoForward);
        }

        [Test]
        public void Open_hides_editor_and_resets_selection()
        {
           StubFile("c:\\foo", "foo content");

           viewModel.EditorVisible = true;
           viewModel.SelectionStart = 3;
           viewModel.SelectionLength = 3;
           viewModel.Open("c:\\foo");

           Assert.IsFalse(viewModel.EditorVisible);
           Assert.AreEqual(0, viewModel.SelectionStart);
           Assert.AreEqual(0, viewModel.SelectionLength);
        }
       
        [Test]
        public void GoBack_to_startpage_hides_editor_and_resets_selection()
        {
           StubFile("c:\\foo", "foo content");
           viewModel.Open("c:\\foo");

           viewModel.EditorVisible = true;
           viewModel.SelectionStart = 3;
           viewModel.SelectionLength = 3;
           viewModel.GoBack();

           Assert.IsFalse(viewModel.EditorVisible);
           Assert.AreEqual(0, viewModel.SelectionStart);
           Assert.AreEqual(0, viewModel.SelectionLength);
        }
 
 
    }
}
