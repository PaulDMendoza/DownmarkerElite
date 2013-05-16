using System.Linq;
using DownMarker.Core;
using Rhino.Mocks;

namespace DownMarker.Tests
{
    public class MainViewModelTestsBase : ViewModelTestsBase
    {
        protected IUriHandler uriHandlerStub;
        protected IMarkdownTransformer markdownTransformerStub;
        protected MainViewModel viewModel;
        protected PersistentState persistentState;

        public override void SetUp()
        {
            base.SetUp();
            uriHandlerStub = MockRepository.GenerateStub<IUriHandler>();
            markdownTransformerStub = MockRepository.GenerateStub<IMarkdownTransformer>();
            persistentState = new PersistentState(new FakeRegistry());

            viewModel = new MainViewModel(
               markdownTransformerStub, promptStub, fileSystemStub, uriHandlerStub, persistentState);
        }

    }
}
