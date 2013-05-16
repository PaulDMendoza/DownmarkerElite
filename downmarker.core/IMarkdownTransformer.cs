using System;

namespace DownMarker.Core
{
    public interface IMarkdownTransformer
    {
        /// <summary>
        /// Asynchronously transforms the given markdown text
        /// to html. 
        /// </summary>
        /// <remarks>
        /// When ready, the result of the transformation is published in
        /// the <see cref="Html"/> property. If the transformation requests
        /// come in rapid succession, then some may be skipped.
        /// </remarks>
        void TransformToHtml(string markdown);

        string Html { get; }
        bool PlainStyle { get; set; }

        event EventHandler HtmlChanged;
    }
}
