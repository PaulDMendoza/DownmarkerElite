using System;
using System.Threading;
using MarkdownSharp;

namespace DownMarker.Core
{
    /// <summary>
    /// Adapts MarkdownSharp to the <see cref="IMarkdownTransformer"/> interface.
    /// </summary>
    public class MarkdownSharpAdapter : IMarkdownTransformer
    {
        private readonly string htmlSkeleton =
            "<html><head>"
            + "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />"
            + "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />"
            + "</head><body>{1}</body></html>";

        private readonly string htmlSkeletonPlain =
            "<html><head>"
            + "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />"
            + "</head><body>{0}</body></html>";

        private readonly Markdown markdown = new Markdown();
        private readonly SynchronizationContext synchronizationContext;
        private readonly string styleLink;

        private bool working;
        private string lastWorkItem;
        private string html;

        public MarkdownSharpAdapter(
            SynchronizationContext synchronizationContext,
            string styleLink)
        {
            if (synchronizationContext == null)
                throw new ArgumentNullException("synchronizationContext");
            if (styleLink == null)
                throw new ArgumentNullException("styleLink");

            this.synchronizationContext = synchronizationContext;
            this.styleLink = styleLink;
        }

        public string Html
        {
            get
            {
                return this.html;
            }
            set
            {
                if (this.html != value)
                {
                    this.html = value;
                    OnHtmlChanged();
                }
            }
        }

        public bool PlainStyle { get; set; }

        public void TransformToHtml(string markdown)
        {
            this.lastWorkItem = markdown;
            if (!working)
            {
                StartWorker(this.lastWorkItem);
            }
        }

        private void StartWorker(string workItem)
        {
            if (this.working)
                throw new InvalidOperationException();

            this.working = true;
            ThreadPool.QueueUserWorkItem(
                delegate
                {
                    // transform on worker thread and 
                    // post result back to the main thread
                    string htmlBodyContent = this.markdown.Transform(workItem);
                    synchronizationContext.Post(
                        delegate
                        {
                            string html;
                            if (this.PlainStyle)
                            {
                                html = string.Format(htmlSkeletonPlain, htmlBodyContent);
                            }
                            else
                            {
                                html = string.Format(htmlSkeleton, this.styleLink, htmlBodyContent);
                            }
                            WorkDone(workItem, html);
                        },
                        null);
                });
        }

        private void WorkDone(string workItem, string html)
        {
            this.working = false;
            this.Html = html;
            if (this.lastWorkItem != workItem)
            {
                StartWorker(this.lastWorkItem);
            }
        }

        private void OnHtmlChanged()
        {
            if (HtmlChanged != null)
                HtmlChanged(this, EventArgs.Empty);
        }

        public event EventHandler HtmlChanged;
    }
}
