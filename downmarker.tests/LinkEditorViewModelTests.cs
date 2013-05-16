using DownMarker.Core;
using NUnit.Framework;

namespace DownMarker.Tests
{
    [TestFixture]
    public class LinkEditorViewModelTests : ViewModelTestsBase
    {
        private LinkEditorViewModel viewModel;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            viewModel = new LinkEditorViewModel(fileSystemStub, promptStub);
        }

        [Test]
        public void LinkTargetError_for_empty_link()
        {
            viewModel.LinkTarget = "";
            Assert.IsNotNullOrEmpty(viewModel.LinkTargetError);
        }

        [Test]
        public void PropertyChanged_raised_when_LinkTargetError_set()
        {
            viewModel.LinkTarget = "foo.md";
            var raised = viewModel.WatchPropertyEvent(x=>x.LinkTargetError);

            viewModel.LinkTarget = "";

            Assert.IsTrue(raised());
        }

        [Test]
        public void PropertyChanged_raised_when_LinkDescriptionError_set()
        {
            viewModel.LinkDescription = "description";
            var raised = viewModel.WatchPropertyEvent(x=>x.LinkDescriptionError);

            viewModel.LinkDescription = "";

            Assert.IsTrue(raised());
        }

        [Test]
        public void LinkDescriptionError_for_empty_description()
        {
            viewModel.LinkDescription = "";
            Assert.IsNotNullOrEmpty(viewModel.LinkDescriptionError);
        }

        [Test]
        public void Browse_produces_relative_URI()
        {
            StubOpenFileReply(@"c:\base\bar.md");

            viewModel.CurrentFile = @"c:\base\foo.md";
            viewModel.Browse();

            Assert.AreEqual("bar.md", viewModel.LinkTarget);
        }

        [Test]
        public void Browse_automatically_sets_CreateTarget_to_false()
        {
            StubOpenFileReply(@"c:\base\bar.md");

            viewModel.CurrentFile = @"c:\base\foo.md";
            viewModel.CreateTarget = true;
            viewModel.Browse();

            Assert.IsFalse(viewModel.CreateTarget);
        }

        [Test]
        public void CreateTargetError_for_non_file_uri()
        {
            viewModel.CreateTarget = true;
            viewModel.LinkTarget = "foo:bar";

            Assert.IsNotNullOrEmpty(viewModel.CreateTargetError);
        }

        [Test]
        public void CreateTargetError_for_non_md_uri()
        {
            viewModel.CreateTarget = true;
            viewModel.LinkTarget = "file:///c:/foo.txt";

            Assert.IsNotNullOrEmpty(viewModel.CreateTargetError);
        }

    }
}
