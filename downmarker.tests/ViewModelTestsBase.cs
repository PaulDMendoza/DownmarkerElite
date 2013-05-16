using System;
using System.Linq.Expressions;
using DownMarker.Core;
using Rhino.Mocks;

namespace DownMarker.Tests
{
    public class ViewModelTestsBase
    {
        protected IPrompt promptStub;
        protected IFileSystem fileSystemStub;

        public virtual void SetUp()
        {
            promptStub = MockRepository.GenerateStub<IPrompt>();
            fileSystemStub = MockRepository.GenerateStub<IFileSystem>();
            fileSystemStub.Stub(x => x.GetAbsoluteFilePath(null))
                .IgnoreArguments()
                .Return(null)
                .WhenCalled(invokation =>
                {
                    invokation.ReturnValue = invokation.Arguments[0];
                });
        }

        protected static string GetPropertyName(Expression<Func<MainViewModel, object>> expression)
        {
            return expression.GetPropertyName();
        }

        protected void StubOpenFileReply(string openfileReply)
        {
            promptStub.Stub(x => x.QuestionOpenFile(null)).IgnoreArguments().Return(openfileReply);
        }

        protected void StubSaveAsReply(string saveAsReply)
        {
            promptStub.Stub(x => x.QuestionSaveAs(null)).IgnoreArguments().Return(saveAsReply);
        }

        protected void StubSaveHtmlReply(string saveHtmlReply)
        {
            promptStub.Stub(x => x.QuestionSaveHtml(null)).IgnoreArguments().Return(saveHtmlReply);
        }

        protected void StubFile(string filePath, string content)
        {
            fileSystemStub.Stub(x => x.ReadTextFile(filePath)).Return(content);
        }

    }
}
