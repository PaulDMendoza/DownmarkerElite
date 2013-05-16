using System;
using System.IO;
using DownMarker.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace DownMarker.Tests
{
    [TestFixture]
    public class MainViewModelSavePromptTests : MainViewModelTestsBase
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void Exit_shows_no_save_prompt_for_unmodified_new_document()
        {
            viewModel.Exit();

            promptStub.AssertWasNotCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Exit_shows_save_prompt_for_modified_new_document()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.No);

            viewModel.MarkdownText = "foo";
            viewModel.Exit();

            promptStub.AssertWasCalled(
               x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void No_to_save_prompt_continues_exit()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.No);
            bool closed = false;
            viewModel.RequestClose += delegate { closed = true; };

            viewModel.MarkdownText = "foo";
            viewModel.Exit();

            Assert.IsTrue(closed);
        }

        [Test]
        public void Cancel_save_prompt_prevents_exit()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Cancel);
            bool closed = false;
            viewModel.RequestClose += delegate { closed = true; };

            viewModel.MarkdownText = "foo";
            viewModel.Exit();

            Assert.IsFalse(closed);
        }

        [Test]
        public void Cancel_save_as_dialog_prevents_exit()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Yes);
            StubSaveAsReply(null);
            bool closed = false;
            viewModel.RequestClose += delegate { closed = true; };

            viewModel.MarkdownText = "foo";
            viewModel.Exit();

            Assert.IsFalse(closed);
        }


        [Test]
        public void IOException_for_save_prevents_exit()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Yes);
            StubSaveAsReply("chosenFileName");
            fileSystemStub.Stub(x => x.WriteTextFile(null, null))
                .IgnoreArguments()
                .Throw(new IOException());
            bool closed = false;
            viewModel.RequestClose += delegate { closed = true; };

            viewModel.MarkdownText = "foo";
            viewModel.Exit();

            Assert.IsFalse(closed);
        }

        [Test]
        public void IOException_for_save_is_shown()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Yes);
            StubSaveAsReply("chosenFileName");
            fileSystemStub.Stub(x => x.WriteTextFile(null, null))
                .IgnoreArguments()
                .Throw(new IOException("errorMessage"));

            viewModel.Save();

            promptStub.AssertWasCalled(
                x => x.ShowError(Arg.Text.Contains("errorMessage"), Arg<string>.Is.Anything));
        }

        [Test]
        public void Save_does_not_prompt_for_filename_after_SaveAs()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Yes);
            StubSaveAsReply("chosenFileName");

            viewModel.MarkdownText = "foo";
            viewModel.SaveAs();
            viewModel.Save();

            promptStub.AssertWasCalled(
                x => x.QuestionSaveAs(Arg<string>.Is.Anything),
                options => options.Repeat.Once());
            fileSystemStub.AssertWasCalled(
                x => x.WriteTextFile("chosenFileName", "foo"),
                options => options.Repeat.Twice());
        }

        [Test]
        public void Exit_does_not_prompt_to_save_after_Save()
        {
            StubSaveAsReply("chosenFileName");

            viewModel.MarkdownText = "foo";
            viewModel.Save();
            viewModel.Exit();

            promptStub.AssertWasNotCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Exit_does_prompt_to_save_after_cancelled_Save()
        {
            StubSaveAsReply(null);

            viewModel.MarkdownText = "foo";
            viewModel.Save();
            viewModel.Exit();

            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Exit_does_prompt_to_save_after_failed_Save()
        {
            StubSaveAsReply("chosenFileName");
            fileSystemStub.Stub(x => x.WriteTextFile(null, null))
                .IgnoreArguments().Throw(new IOException());

            viewModel.MarkdownText = "foo";
            viewModel.Save();
            viewModel.Exit();

            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Save_does_not_prompt_for_filename_after_Open()
        {
            StubOpenFileReply("chosenFile");
            StubFile("chosenFile", "content");

            viewModel.Open();
            viewModel.Save();

            promptStub.AssertWasNotCalled(x => x.QuestionSaveAs(Arg<string>.Is.Anything));
            fileSystemStub.AssertWasCalled(x => x.WriteTextFile("chosenFile", "content"));
        }

        [Test]
        public void Exit_after_open_does_not_prompt_to_save()
        {
            StubOpenFileReply("chosenFile");
            StubFile("chosenFile", "content");

            viewModel.Open();
            viewModel.Exit();

            promptStub.AssertWasNotCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Navigate_prompts_to_save_unsaved_changes()
        {
            StubFile("c:\\foo.md", "content");

            viewModel.MarkdownText = "unsaved";
            viewModel.Navigate(new Uri("file:///c:/foo.md"));

            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Back_command_prompts_to_save()
        {
            StubFile("c:\\foo.md", "foo content");
            StubFile("c:\\bar.md", "bar content");

            viewModel.Open("c:\\foo.md");
            viewModel.Navigate(new Uri("c:\\bar.md", UriKind.Absolute));
            viewModel.MarkdownText = "changed bar content";
            viewModel.GoBack();

            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void Forward_command_prompts_to_save()
        {
            StubFile("c:\\foo.md", "foo content");
            StubFile("c:\\bar.md", "bar content");

            viewModel.Open("c:\\foo.md");
            viewModel.Navigate(new Uri("c:\\bar.md", UriKind.Absolute));
            viewModel.GoBack();
            viewModel.MarkdownText = "changed foo content";
            viewModel.GoForward();

            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges(),
               options => options.Repeat.Once());
        }

        [Test]
        public void Cancel_save_prompt_prevents_open()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Cancel);

            viewModel.MarkdownText = "foo";
            viewModel.Open();

            promptStub.AssertWasNotCalled(x => x.QuestionOpenFile(Arg<string>.Is.Anything));
        }

        [Test]
        public void SaveAs_dialog_uses_current_document_location_as_initial_directory()
        {
            StubSaveAsReply(@"c:\foo\bar");
            viewModel.Save();
            viewModel.SaveAs();
            
            promptStub.AssertWasCalled(x => x.QuestionSaveAs(@"c:\foo"));
        }

    }
}
