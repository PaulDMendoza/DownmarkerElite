
using System;
namespace DownMarker.Core
{
    /// <summary>
    /// Immutable description of text editor state.
    /// </summary>
    public sealed class EditorState
    {
        public readonly string Text;
        public readonly int SelectionStart;

        public EditorState(string text, int selectionStart)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            this.Text = text;
            this.SelectionStart = selectionStart;
        }
    }
}
