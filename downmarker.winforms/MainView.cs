using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DownMarker.Core;
using DownMarker.WinForms.Properties;

namespace DownMarker.WinForms
{
    public partial class MainView : Form
    {
        private readonly MainViewModel viewModel;
        private readonly PropertyEvents<MainViewModel> events;
        private readonly List<ToolStripItem> selectionEditorToolStripItems;
        private readonly List<ToolStripMenuItem> selectionEditorMenuItems;

        private bool updatingBrowser;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        /// <param name="viewModel">
        /// The view model to bind to.
        /// </param>
        public MainView(MainViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");

            this.InitializeComponent();

            this.Icon = Resources.downmarker;

            selectionEditorToolStripItems = new List<ToolStripItem>()
            {
                this.linkButton,
                this.boldButton,
                this.italicButton,
                this.quoteButton,
                this.codeBlockButton,
                this.codeSpanButton,
                this.editToolsSeparator,
                this.imageButton
            };

            selectionEditorMenuItems = new List<ToolStripMenuItem>()
            {
                this.linkMenuItem,
                this.boldMenuItem,
                this.italicMenuItem,
                this.quoteMenuItem,
                this.codeBlockMenuItem,
                this.codeSpanMenuItem,
                this.imageMenuItem
            };
            
            // putting controls in the correct state here reduces start-up 
            // flicker a bit
            this.splitContainer.Panel1Collapsed = true;
            this.backButton.Enabled = false;
            this.forwardButton.Enabled = false;
            SetEditorToolStripItemsVisibility(false);

            this.viewModel = viewModel;
            this.viewModel.RequestClose += delegate { Application.Exit(); };
            this.viewModel.TemporaryHtmlFileContentChanged += HandleTemporaryHtmlFileContentChanged;

            this.events = this.viewModel.CreatePropertyEvents();
            this.events[x => x.MarkdownText] += HandleMarkdownTextChanged;
            this.events[x => x.Title] += HandleTitleChanged;
            this.events[x => x.EditorSize] += HandleEditorSizeChanged;
            this.events[x => x.EditorOrientation] += HandleSplitOrientationChanged;
            this.events[x => x.EditorVisible] += HandleEditorVisibleChanged;
            this.events[x => x.CanGoBack] += HandleCanGoBackChanged;
            this.events[x => x.CanGoForward] += HandleCanGoForwardChanged;
            this.events[x => x.SelectionStart] += HandleSelectionStartChanged;
            this.events[x => x.SelectionLength] += HandleSelectionLengthChanged;
            this.events[x => x.CanRedo] += HandleCanRedoChanged;
            this.events[x => x.CanUndo] += HandleCanUndoChanged;
            this.events[x => x.PlainStyle] += HandlePlainStyleChanged;

            if (Config.Read("Maximized") == "true")
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.Resize += (sender, args) =>
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        Config.SetSetting("Maximized", "true");
                    }
                    else
                    {
                        Config.SetSetting("Maximized", "false");
                    }
                };
        }

        private void SetEditorToolStripItemsVisibility(bool visibility)
        {
            foreach (var item in selectionEditorToolStripItems)
                item.Visible = visibility;
        }

        private void SetEditorMenuItemsEnabled(bool enabled)
        {
            foreach (var menuItem in selectionEditorMenuItems)
                menuItem.Enabled = enabled;
        }

        private void HandleCanRedoChanged(object sender, EventArgs args)
        {
            redoToolStripMenuItem.Enabled = viewModel.CanRedo;
        }

        private void HandleCanUndoChanged(object sender, EventArgs args)
        {
            undoToolStripMenuItem.Enabled = viewModel.CanUndo;
        }

        private void HandleSelectionStartChanged(object sender, EventArgs args)
        {
            this.markdownEditorTextBox.SelectionStart = viewModel.SelectionStart;
        }

        private void HandleSelectionLengthChanged(object sender, EventArgs args)
        {
            this.markdownEditorTextBox.SelectionLength = viewModel.SelectionLength;
        }

        private void HandleTitleChanged(object sender, EventArgs args)
        {
            this.Text = this.viewModel.Title; 
        }

        private void HandleCanGoBackChanged(object sender, EventArgs args)
        {
            this.backButton.Enabled = this.viewModel.CanGoBack;
            this.backMenuItem.Enabled = this.viewModel.CanGoBack;
        }
        
        private void HandleCanGoForwardChanged(object sender, EventArgs args)
        {
            this.forwardButton.Enabled = this.viewModel.CanGoForward;
            this.forwardMenuItem.Enabled = this.viewModel.CanGoForward;
        }
        
        private void HandleEditorVisibleChanged(object sender, EventArgs args)
        {
            bool visible = this.viewModel.EditorVisible;

            this.splitContainer.Panel1Collapsed = !visible;
            this.editButton.Checked = visible;
            this.showEditorToolStripMenuItem.Checked = visible;
            SetEditorToolStripItemsVisibility(visible);
            SetEditorMenuItemsEnabled(visible);
            if (visible)
               markdownEditorTextBox.Focus();
        }

        private void HandleSplitOrientationChanged(object sender, EventArgs args)
        {
            splitContainer.Orientation = GetSplitOrientation(this.viewModel.EditorOrientation);
            horizontalSplitterToolStripMenuItem.Checked = 
                (viewModel.EditorOrientation == DownMarker.Core.ViewOrientation.Horizontal);
            verticalSplitterToolStripMenuItem.Checked = 
                (viewModel.EditorOrientation == DownMarker.Core.ViewOrientation.Vertical);
        }

        private void HandlePlainStyleChanged(object sender, EventArgs args)
        {
            plainStyleToolStripMenuItem.Checked = this.viewModel.PlainStyle;
        }

        private static Orientation GetSplitOrientation(ViewOrientation viewOrientation)
        {
            if (viewOrientation == ViewOrientation.Horizontal)
                return Orientation.Horizontal;
            else
                return Orientation.Vertical;
        }

        private void HandleMarkdownTextChanged(object sender, EventArgs args)
        {
            markdownEditorTextBox.Text = this.viewModel.MarkdownText;
        }

        private void HandleTextBoxTextChanged(object sender, EventArgs args)
        {
            this.viewModel.MarkdownText = markdownEditorTextBox.Text;
        }

        private void HandleTemporaryHtmlFileContentChanged(object sender, EventArgs args)
        {
            this.updatingBrowser = true;
            if ((this.webBrowser.Document != null) && (this.webBrowser.Document.Body != null))
            {
                viewModel.HtmlScroll = this.webBrowser.Document.Body.ScrollTop;
            }
            this.webBrowser.Navigate(this.viewModel.TemporaryHtmlFile, newWindow: false);
        }

        private void HandleOpenClick(object sender, EventArgs e)
        {
            this.viewModel.Open();
        }

        private void HandleSaveAsClick(object sender, EventArgs e)
        {
            this.viewModel.SaveAs();
        }

        private void HandleSaveClick(object sender, EventArgs e)
        {
            this.viewModel.Save();
        }

        private void HandleExitClick(object sender, EventArgs e)
        {
            this.viewModel.Exit();
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // always cancel; it is up to the view model to raise
                // RequestClose instead.
                e.Cancel = true;
                this.viewModel.Exit();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            this.events.RaiseAll();
            this.MoveToForeground();
            base.OnShown(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.events.Dispose();
                if (this.components != null)
                    this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void HandleVerticalMenuItemClick(object sender, EventArgs e)
        {
            this.viewModel.EditorOrientation = ViewOrientation.Vertical;
        }

        private void HandleHorizontalMenuItemClick(object sender, EventArgs e)
        {
            this.viewModel.EditorOrientation = ViewOrientation.Horizontal;
        }

        private void HandleEditClick(object sender, EventArgs e)
        {
            this.viewModel.ToggleEdit();
        }

        private void HandleWebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (updatingBrowser)
                return;
            CatchErrors(
                delegate
                {
                    var blank = new Uri("about:blank");
                    var navigationTarget = e.Url;

                    if (navigationTarget != blank)
                    {
                        e.Cancel = true;
                        this.viewModel.Navigate(navigationTarget);
                    }
                });
        }

        private void HandleLinkClick(object sender, EventArgs e)
        {
            this.viewModel.Link();
        }

        /// <summary>
        /// Useful in callbacks where unexpected exceptions tend to get lost.
        /// </summary>
        private void CatchErrors(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleForwardClick(object sender, System.EventArgs e)
        {
            this.viewModel.GoForward();
        }

        private void HandleBackClick(object sender, System.EventArgs e)
        {
            this.viewModel.GoBack();     
        }

        private void HandleBoldClick(object sender, EventArgs e)
        {
            this.viewModel.Bold();
        }

        private void HandleItalicClick(object sender, EventArgs e)
        {
            this.viewModel.Italic();
        }

        private void HandleQuoteClick(object sender, EventArgs e)
        {
            this.viewModel.Quote();
        }

        private void HandleCodeBlockClick(object sender, EventArgs e)
        {
            this.viewModel.CodeBlock();
        }

        private void HandleMarkdownEditorTextBoxSelectionChanged(object sender, EventArgs e)
        {
            this.viewModel.SelectionStart = this.markdownEditorTextBox.SelectionStart;
            this.viewModel.SelectionLength = this.markdownEditorTextBox.SelectionLength;
        }

        private void HandleImageClick(object sender, EventArgs e)
        {
            this.viewModel.Image();
        }

        private void HandleUndoClick(object sender, EventArgs e)
        {
            this.viewModel.Undo();
        }

        private void HandleRedoClick(object sender, EventArgs e)
        {
            this.viewModel.Redo();
        }

        private void HandleCodeSpanClick(object sender, EventArgs e)
        {
            this.viewModel.CodeSpan();
        }

        private void HandleNewClick(object sender, EventArgs e)
        {
            this.viewModel.New();
        }

        private void HandleExportHtmlClick(object sender, EventArgs e)
        {
            this.viewModel.ExportHtml();
        }

        private void HandleWebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.updatingBrowser = false;
            if ((this.webBrowser.Document != null) && (this.webBrowser.Document.Body != null))
            {
                this.webBrowser.Document.Body.ScrollTop = 0; // otherwise next line doesn't work
                this.webBrowser.Document.Body.ScrollTop = viewModel.HtmlScroll;
            }
        }

        private void HandlePrintMenuItemClick(object sender, EventArgs e)
        {
           this.webBrowser.ShowPrintDialog();
        }

        private void HandleEditorSizeChanged(object sender, EventArgs e)
        {
            this.splitContainer.SplitterDistance = viewModel.EditorSize;
        }

        private void HandleSplitterMoved(object sender, SplitterEventArgs e)
        {
            if (viewModel != null) // null in InitializeComponent
            {
                viewModel.EditorSize = this.splitContainer.SplitterDistance;
            }
        }

        private void HandlePlainStyleClick(object sender, EventArgs e)
        {
           this.viewModel.PlainStyle = !this.viewModel.PlainStyle;
        }

        private void HandleReloadClick(object sender, EventArgs e)
        {
           this.viewModel.Reload();
        }
        
    }
}
