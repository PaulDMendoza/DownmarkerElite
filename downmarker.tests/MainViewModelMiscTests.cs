using System;
using System.IO;
using System.Linq;
using DownMarker.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace DownMarker.Tests
{
    [TestFixture]
    public class MainViewModelMiscTests : MainViewModelTestsBase
    {

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        [Test]
        public void Changing_MarkdownText_raises_PropertyChanged()
        {
            var raised = viewModel.WatchPropertyEvent(x => x.MarkdownText);
            viewModel.MarkdownText = "foo";
            Assert.IsTrue(raised(), "PropertyChanged was not raised, or not raised with the expected property name");
        }

        [Test]
        public void HtmlChanged_on_transformer_triggers_TemporaryHtmlFileContentChanged()
        {
            bool raised = false;
            viewModel.TemporaryHtmlFileContentChanged += delegate { raised = true; };
            markdownTransformerStub.Stub(x => x.Html).Return("some html");

            markdownTransformerStub.Raise(
                x => x.HtmlChanged+=null,
                null,
                EventArgs.Empty);

            Assert.IsTrue(raised);
        }

        [Test]
        public void HtmlChanged_on_transformer_writes_file()
        {
            markdownTransformerStub.Stub(x => x.Html).Return("some html");

            markdownTransformerStub.Raise(
                x => x.HtmlChanged += null,
                null,
                EventArgs.Empty);

            fileSystemStub.AssertWasCalled(x => x.WriteTextFile(viewModel.TemporaryHtmlFile, "some html"));
        }

        [Test]
        public void TemporaryHtmlFile_is_in_TempPath()
        {
            StubFile("c:\\foo.md", "foo content");
            viewModel.Open("c:\\foo.md");
            string tempPath = Path.GetTempPath();
            Assert.IsTrue(viewModel.TemporaryHtmlFile.StartsWith(tempPath));
        }

        [Test]
        public void Relative_links_are_rewritten_to_absolute_links_in_temporary_html()
        {
            StubFile(@"c:\folder\foo.md", "");
            viewModel.Open(@"c:\folder\foo.md");

            markdownTransformerStub.Stub(x => x.Html).Return("href=\"bar\"");
            markdownTransformerStub.Raise(
                x => x.HtmlChanged += null,
                null,
                EventArgs.Empty);

            fileSystemStub.AssertWasCalled(x=>x.WriteTextFile(
                viewModel.TemporaryHtmlFile,
                "href=\"file:///c:/folder/bar\""));
        }

        [Test]
        public void Setting_MarkdownText_to_same_value_does_not_raises_PropertyChanged()
        {
            bool raised = false;
            viewModel.MarkdownText = "foo";
            viewModel.PropertyChanged += (sender, args) => raised = true;

            viewModel.MarkdownText = "foo";

            Assert.IsFalse(raised, "PropertyChanged was raised unexpectedly");
        }

        [Test]
        public void Open_sets_MarkdownText()
        {
            StubOpenFileReply("chosenFile");
            StubFile("chosenFile", "content");
            
            string expectedProperty = GetPropertyName(x => x.MarkdownText);
            bool raised = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == expectedProperty)
                {
                    raised = true;
                }
            };

            viewModel.Open();

            Assert.AreEqual("content", viewModel.MarkdownText);
            Assert.IsTrue(raised, "PropertyChanged was not raised, or not raised with correct name");
        }

        [Test]
        public void IOException_for_open_is_shown()
        {
            StubOpenFileReply("chosenFile");
            fileSystemStub.Stub(x=>x.ReadTextFile("chosenFile"))
                .Throw(new IOException("errorMessage"));

            viewModel.Open();
            viewModel.Exit();

            promptStub.AssertWasCalled(
                x=>x.ShowError(
                    Arg.Text.Contains("errorMessage"), Arg<string>.Is.Anything));
        }

        [Test]
        public void Cancel_open_dialog_stops_open()
        {
            StubOpenFileReply(null);            

            viewModel.Open();

            fileSystemStub.AssertWasNotCalled(
                x => x.ReadTextFile(Arg<string>.Is.Anything)); 
        }

        [Test]
        public void Setting_Orientation_forces_editor_visibility()
        {
            if (viewModel.EditorVisible)
               viewModel.ToggleEdit();
            viewModel.EditorOrientation = ViewOrientation.Horizontal;
            
            Assert.IsTrue(viewModel.EditorVisible);
        }


        [Test]
        public void ToggleEdit_changes_EditorVisible()
        {
           bool original = viewModel.EditorVisible;
           viewModel.ToggleEdit();
           Assert.AreNotEqual(original, viewModel.EditorVisible);
           viewModel.ToggleEdit();
           Assert.AreEqual(original, viewModel.EditorVisible);
        }

        [Test]
        public void Title_on_start_up()
        {
            Assert.AreEqual("[No Name] - DownMarker", this.viewModel.Title);
        }

        [Test]
        public void Title_after_open()
        {
            StubOpenFileReply(@"c:\full\chosenFile.md");
            StubFile(@"c:\full\chosenFile.md",  "content");
            this.viewModel.Open();
            Assert.AreEqual(
                @"chosenFile (c:\full\chosenFile.md) - DownMarker",
                this.viewModel.Title);
        }

        [Test]
        public void Title_after_modification()
        {
            StubOpenFileReply(@"c:\full\chosenFile.md");
            StubFile(@"c:\full\chosenFile.md", "content");

            this.viewModel.Open();
            this.viewModel.MarkdownText = "modified content";

            Assert.AreEqual(
                @"chosenFile* (c:\full\chosenFile.md) - DownMarker",
                this.viewModel.Title);
        }

        [Test]
        public void Title_after_modification_and_save()
        {
            StubOpenFileReply(@"c:\full\chosenFile.md");
            StubFile(@"c:\full\chosenFile.md", "content");

            this.viewModel.Open();
            this.viewModel.MarkdownText = "modified content";
            this.viewModel.Save();

            Assert.AreEqual(
                @"chosenFile (c:\full\chosenFile.md) - DownMarker",
                this.viewModel.Title);
        }

        [Test]
        public void Title_after_save_shows_new_name()
        {
            StubSaveAsReply(@"c:\full\saveName.md");
            this.viewModel.Save();
            Assert.AreEqual(
                @"saveName (c:\full\saveName.md) - DownMarker",
                this.viewModel.Title);
        }

        [Test]
        public void Bold_makes_selected_text_bold()
        {
            viewModel.MarkdownText = "foo bar";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 3;
            viewModel.Bold();
            Assert.AreEqual("foo **bar**", viewModel.MarkdownText);
        }

        [Test]
        public void Bold_keeps_selection()
        {
            viewModel.MarkdownText = "foo bar";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 3;
            viewModel.Bold();
            Assert.AreEqual(6, viewModel.SelectionStart);
            Assert.AreEqual(3, viewModel.SelectionLength);
        }

        [Test]
        public void Bold_unbolds_selected_text()
        {
            viewModel.MarkdownText = "foo **bar**";
            viewModel.SelectionStart = 6;
            viewModel.SelectionLength = 3;
            viewModel.Bold();
            Assert.AreEqual("foo bar", viewModel.MarkdownText);
        }

        [Test]
        public void Unbold_keeps_selection()
        {
            viewModel.MarkdownText = "foo **bar**";
            viewModel.SelectionStart = 6;
            viewModel.SelectionLength = 3;
            viewModel.Bold();
            Assert.AreEqual(4, viewModel.SelectionStart);
            Assert.AreEqual(3, viewModel.SelectionLength);
        }

        [Test]
        public void Bold_without_selection_expands_to_word()
        {
            viewModel.MarkdownText = "foo bar";
            viewModel.SelectionStart = 3;
            viewModel.SelectionLength = 0;
            viewModel.Bold();
            Assert.AreEqual("**foo** bar", viewModel.MarkdownText);
            Assert.AreEqual(2, viewModel.SelectionStart);
            Assert.AreEqual(3, viewModel.SelectionLength);
        }

        [Test]
        public void Bold_at_word_without_selection_selects_and_unbolds_word()
        {
            viewModel.MarkdownText = "**foo** bar";
            viewModel.SelectionStart = 3;
            viewModel.SelectionLength = 0;
            viewModel.Bold();
            Assert.AreEqual("foo bar", viewModel.MarkdownText);
            Assert.AreEqual(0, viewModel.SelectionStart);
            Assert.AreEqual(3, viewModel.SelectionLength);
        }

        [Test]
        public void Bold_in_whitespace_inserts_selected_placeholder()
        {
            viewModel.MarkdownText = "foo  bar";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 0;
            viewModel.Bold();
            Assert.AreEqual("foo **bold** bar", viewModel.MarkdownText);
            Assert.AreEqual(6, viewModel.SelectionStart);
            Assert.AreEqual(4, viewModel.SelectionLength);
        }

        [Test]
        public void Bold_toggles()
        {
            viewModel.MarkdownText = "foo bar";
            viewModel.SelectionStart = 6;
            viewModel.Bold();
            viewModel.Bold();
            Assert.AreEqual("foo bar", viewModel.MarkdownText);
        }

        [Test]
        public void Quote_turns_selection_into_quote()
        {
            viewModel.MarkdownText = "line1\nline2\nline3\nline4";
            viewModel.SelectionStart = 6;
            viewModel.SelectionLength = 11;
            viewModel.Quote();
            Assert.AreEqual("line1\n\n> line2\n> line3\n\nline4", viewModel.MarkdownText);
        }

        [Test]
        public void Quote_unquotes_selection()
        {
            viewModel.MarkdownText = "line1\n> line2\n> line3";
            viewModel.SelectionStart = 6;
            viewModel.SelectionLength = 15;
            viewModel.Quote();
            Assert.AreEqual("line1\nline2\nline3", viewModel.MarkdownText);
        }


        [Test]
        public void Quote_turns_current_paragraph_into_quote()
        {
            viewModel.MarkdownText = "before\n\nline1\nline2\n\nafter";
            viewModel.SelectionStart = 10;
            viewModel.SelectionLength = 0;
            viewModel.Quote();
            Assert.AreEqual("before\n\n> line1\n> line2\n\nafter", viewModel.MarkdownText);
        }

        [Test]
        public void Quote_selects_replacement_text()
        {
            viewModel.MarkdownText = "foo";
            viewModel.Quote();
            Assert.AreEqual(0, viewModel.SelectionStart);
            Assert.AreEqual("> foo".Length, viewModel.SelectionLength);
        }

        [Test]
        public void Quote_ignores_trailing_linefeed_in_selection()
        {
            viewModel.MarkdownText = "foo\nbar\n";
            viewModel.SelectionLength = 7;
            viewModel.Quote();
            string resultWithoutTrailing = viewModel.MarkdownText;

            viewModel.MarkdownText = "foo\nbar\n";
            viewModel.SelectionLength = 8;
            viewModel.Quote();
            string resultWithTrailing = viewModel.MarkdownText;

            Assert.AreEqual(resultWithoutTrailing, resultWithTrailing);
        }

        [Test]
        public void CodeBlock_turns_text_into_code_even_if_already_indented()
        {
            viewModel.MarkdownText =
                "define SayHello:\n"
              + "    print(hello)\n"; // already indented
            viewModel.CodeBlock();
            Assert.AreEqual(
                "    define SayHello:\n"
              + "        print(hello)\n",
                viewModel.MarkdownText);
        }

        [Test]
        public void CodeBlock_adds_newlines_to_creat_block_but_doesnt_select_them()
        {
            viewModel.MarkdownText = "text code text";
            viewModel.SelectionStart = 5;
            viewModel.SelectionLength = 4;
            viewModel.CodeBlock();
            Assert.AreEqual("text \n\n    code\n\n text", viewModel.MarkdownText);
            Assert.AreEqual(7, viewModel.SelectionStart, "wrong SelectionStart");
            Assert.AreEqual(8, viewModel.SelectionLength, "wrong SelectionLength");
        }

        [Test]
        public void CodeSpan_inserts_backticks()
        {
            viewModel.MarkdownText = "the foo variable";
            viewModel.SelectionStart = 4;
            viewModel.SelectionLength = 3;
            viewModel.CodeSpan();
            Assert.AreEqual("the `foo` variable", viewModel.MarkdownText);
        }

        [Test]
        public void Undo_restores_previous_MarkdownText()
        {
            viewModel.MarkdownText = "";
            viewModel.SelectionStart = 1;
            viewModel.MarkdownText = "f";
            viewModel.SelectionStart = 2;
            viewModel.MarkdownText = "fo";
            viewModel.SelectionStart = 3;
            viewModel.MarkdownText = "foo";
            viewModel.Undo();
            Assert.AreEqual("fo", viewModel.MarkdownText);
            Assert.AreEqual(2, viewModel.SelectionStart);
            viewModel.Undo();
            Assert.AreEqual("f", viewModel.MarkdownText);
            Assert.AreEqual(1, viewModel.SelectionStart);
            viewModel.Undo();
            Assert.AreEqual("", viewModel.MarkdownText);
            Assert.AreEqual(0, viewModel.SelectionStart);

        }

        [Test]
        public void Redo_restores_undone_states()
        {
            viewModel.MarkdownText = "";
            viewModel.SelectionStart = 1;
            viewModel.MarkdownText = "f";
            viewModel.SelectionStart = 2;
            viewModel.MarkdownText = "fo";
            viewModel.SelectionStart = 3;
            viewModel.MarkdownText = "foo";
            viewModel.Undo();
            viewModel.Undo();
            viewModel.Undo();
            Assert.AreEqual("", viewModel.MarkdownText);
            Assert.AreEqual(0, viewModel.SelectionStart);
            viewModel.Redo();
            Assert.AreEqual("f", viewModel.MarkdownText);
            Assert.AreEqual(1, viewModel.SelectionStart);
            viewModel.Redo();
            Assert.AreEqual("fo", viewModel.MarkdownText);
            Assert.AreEqual(2, viewModel.SelectionStart);
            viewModel.Redo();
            Assert.AreEqual("foo", viewModel.MarkdownText);
            Assert.AreEqual(3, viewModel.SelectionStart);
        }

        [Test]
        public void CanUndo_true_after_first_change()
        {
            Assert.IsFalse(viewModel.CanUndo, "CanUndo should be false initially");
            viewModel.MarkdownText = "foo";
            Assert.IsTrue(viewModel.CanUndo, "CanUndo should be true after first change");
        }

        [Test]
        public void CanUndo_false_after_undo_change()
        {
            viewModel.MarkdownText = "foo";
            viewModel.Undo();
            Assert.IsFalse(viewModel.CanUndo, "CanUndo should be false after undoing only change");
        }

        [Test]
        public void Redo_undoes_undo()
        {
            viewModel.MarkdownText = "foo";
            viewModel.MarkdownText = "bar";
            viewModel.Undo();
            viewModel.Redo();
            Assert.AreEqual("bar", viewModel.MarkdownText);
        }

        [Test]
        public void CanRedo_true_after_undo()
        {
            viewModel.MarkdownText = "foo";
            viewModel.MarkdownText = "bar";
            Assert.IsFalse(viewModel.CanRedo, "CanRedo should be false initially");
            viewModel.Undo();
            Assert.IsTrue(viewModel.CanRedo, "CanRedo should be true after undo");
        }

        [Test]
        public void CanUndo_false_after_open()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.No);
            StubOpenFileReply(@"c:\full\chosenFile.md");
            StubFile(@"c:\full\chosenFile.md", "content");

            viewModel.MarkdownText = "blabla";
            viewModel.Open();
            Assert.IsFalse(viewModel.CanUndo);
        }

        [Test]
        public void Setting_MarkdownText_truncates_redo_history()
        {
            viewModel.MarkdownText = "foo";
            viewModel.MarkdownText = "bar";
            viewModel.MarkdownText = "baz";
            viewModel.Undo();
            viewModel.Undo();
            viewModel.MarkdownText = "hello";
            Assert.IsFalse(viewModel.CanRedo);
        }

        [Test]
        public void Delete_line_does_not_crash()
        {
            viewModel.MarkdownText = "foo\nbar\baz";
            viewModel.MarkdownText = "foo\nbaz";
        }

        [Test]
        public void New_clears_Undo_history()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.No);
            viewModel.MarkdownText = "foo";
            viewModel.MarkdownText = "bar";
            viewModel.New();
            Assert.IsFalse(viewModel.CanUndo);
        }

        [Test]
        public void New_after_changes_prompts_to_save()
        {
            viewModel.MarkdownText = "foo";
            viewModel.New();
            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges());
        }

        [Test]
        public void New_new_after_changes_prompts_to_save_only_once()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges()).Return(PromptResult.No);
            viewModel.MarkdownText = "foo";
            viewModel.New();
            viewModel.New();
            promptStub.AssertWasCalled(x => x.QuestionSaveUnsavedChanges(), y => y.Repeat.Once());
        }

        [Test]
        public void New_does_nothing_if_prompt_to_save_cancelled()
        {
            promptStub.Stub(x => x.QuestionSaveUnsavedChanges())
                .Return(PromptResult.Cancel);
            viewModel.MarkdownText = "foo";
            viewModel.New();
            Assert.AreEqual("foo", viewModel.MarkdownText);
        }

        [Test]
        public void New_changes_title()
        {
            StubSaveAsReply("foofile");
            viewModel.Save();

            viewModel.New();
            Assert.AreEqual("[No Name] - DownMarker", viewModel.Title);
        }

        [Test]
        public void ExportHtml_does_nothing_if_user_cancels_save_dialog()
        {
            StubSaveHtmlReply(null);
            viewModel.ExportHtml();
            fileSystemStub.AssertWasNotCalled(
                x => x.WriteTextFile(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
        }

        [Test]
        public void ExportHtml_writes_htlm_to_filesystem()
        {
            StubSaveHtmlReply("chosenFile");
            markdownTransformerStub.Stub(x => x.Html).Return("html content");

            viewModel.ExportHtml();
            
            fileSystemStub.AssertWasCalled(
                x => x.WriteTextFile("chosenFile","html content"));
        }

        [Test]
        public void OpenDialog_uses_current_document_location_as_initial_directory()
        {
            StubSaveAsReply(@"c:\foo\bar");
            viewModel.Save();
            viewModel.Open();
            promptStub.AssertWasCalled(x => x.QuestionOpenFile(@"c:\foo"));
        }

        [Test]
        public void New_selects_placeholder_title()
        {
           viewModel.New();
           Assert.IsTrue(viewModel.EditorVisible);
           Assert.AreEqual("# Title", viewModel.MarkdownText);
           Assert.AreEqual(2, viewModel.SelectionStart);
           Assert.AreEqual(5, viewModel.SelectionLength);
        }

        [Test]
        public void Start_page_not_in_recently_used()
        {
            Assert.IsFalse(persistentState.RecentlyUsedDocuments.Any(
                x => x.StartsWith("downmarker:start")));
        }
    }

}
