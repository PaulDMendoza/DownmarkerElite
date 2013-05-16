
namespace DownMarker.Core
{
    class EditorStateHistory : IHistory<EditorState>
    {
        private string currentText = "";
        private IHistory<StringChange> textEditHistory; // differences between states, not states!
        private IHistory<int> selectionStartHistory;

        public EditorStateHistory(int limit)
        {
            textEditHistory = new LinkedListHistory<StringChange>(limit); 
            selectionStartHistory = new LinkedListHistory<int>(limit);
        }

        public bool CanGoBack
        {
            get
            {
                return selectionStartHistory.CanGoBack;
            }
        }

        public bool CanGoForward
        {
            get
            {
                return selectionStartHistory.CanGoForward;
            }
        }

        public EditorState Current
        {
            get
            {
                return new EditorState(
                    currentText,
                    selectionStartHistory.Current);
            }
        }

        public void Add(EditorState state)
        {
            selectionStartHistory.Add(state.SelectionStart);
            textEditHistory.Add(StringChange.GetStringChange(this.currentText, state.Text));
            this.currentText = state.Text;
        }

        public void Clear()
        {
            currentText = "";
            textEditHistory.Clear();
            selectionStartHistory.Clear();
        }

        public EditorState Back()
        {
            var lastEdit = textEditHistory.Current;
            this.currentText = lastEdit.Undo(this.currentText);
            
            textEditHistory.Back();
            selectionStartHistory.Back();

            return Current;
        }

        public EditorState Forward()
        {
            var edit = textEditHistory.Forward();
            selectionStartHistory.Forward();

            this.currentText = edit.Do(this.currentText);

            return Current;
        }
    }
}
