using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DownMarker.WinForms
{
    /// <summary>
    /// <see cref="RichTextBox"/> variant which fixes some issues with MVVM-style property binding.
    /// </summary>
    /// <remarks>
    /// This class
    /// <list type="bullet">
    /// <item>ignores unnecessary sets for the Text property, preventing flicker</item>
    /// <item>prevents selection being lost when Text is set</item>
    /// <item>prevents vertical scroll changes when Text is set</item>
    /// <item>prevents SelectionLength reset to 0 when SelectionStart is set</item>
    /// </list>
    /// </remarks>
    public class RichTextBoxEx : RichTextBox
    {
        private bool suppressSelectionChanged;

        /// <summary>
        /// Gets or sets the shown text.
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (value != base.Text)
                {
                    this.suppressSelectionChanged = true;
                    int scrollpos = VerticalScrollPosition;
                    int start = this.SelectionStart;
                    int length = this.SelectionLength;

                    base.Text = value;
                    
                    this.VerticalScrollPosition = scrollpos;
                    this.SelectionStart = start;
                    this.SelectionLength = length;
                    this.suppressSelectionChanged = false;
                }
            }
        }

        public new int SelectionStart
        {
            get
            {
                return base.SelectionStart;
            }
            set
            {
                if (value != base.SelectionStart)
                {
                    suppressSelectionChanged = true;
                    int oldLength = base.SelectionLength;

                    base.SelectionStart = value;
                    
                    base.SelectionLength = oldLength;
                    suppressSelectionChanged = false;
                    OnSelectionChanged(EventArgs.Empty);
                }
            }
        }

        public override int SelectionLength
        {
            get
            {
                return base.SelectionLength;
            }
            set
            {
                if (base.SelectionLength != value)
                {
                    base.SelectionLength = value;
                }
            }
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            if (!suppressSelectionChanged)
                base.OnSelectionChanged(e);
        }

        /// <summary>
        /// Gets or sets the first visible line.
        /// </summary>
        public int VerticalScrollPosition
        {
            get
            {
                return SendMessage(this.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);
            }
            set
            {
                SendMessage(this.Handle, EM_LINESCROLL, 0, value);
            }
        }

        private const string USER32DLL = "user32.dll";
        private const int EM_LINESCROLL = 0xB6;
        private const int EM_GETFIRSTVISIBLELINE = 0xCE;

        [DllImport(USER32DLL)]
        static extern int SendMessage(
            IntPtr hWnd, int wMsg, int wParam, int lParam);

    }
}
