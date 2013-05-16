namespace DownMarker.WinForms
{
   partial class MainView
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.splitContainer = new System.Windows.Forms.SplitContainer();
         this.webBrowser = new System.Windows.Forms.WebBrowser();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
         this.backMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.forwardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
         this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.linkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.imageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.boldMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.italicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.quoteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.codeBlockMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.codeSpanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.horizontalSplitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.verticalSplitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.plainStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.showEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.backButton = new System.Windows.Forms.ToolStripButton();
         this.forwardButton = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.openButton = new System.Windows.Forms.ToolStripButton();
         this.saveButton = new System.Windows.Forms.ToolStripButton();
         this.editButton = new System.Windows.Forms.ToolStripButton();
         this.editToolsSeparator = new System.Windows.Forms.ToolStripSeparator();
         this.linkButton = new System.Windows.Forms.ToolStripButton();
         this.imageButton = new System.Windows.Forms.ToolStripButton();
         this.boldButton = new System.Windows.Forms.ToolStripButton();
         this.italicButton = new System.Windows.Forms.ToolStripButton();
         this.quoteButton = new System.Windows.Forms.ToolStripButton();
         this.codeBlockButton = new System.Windows.Forms.ToolStripButton();
         this.codeSpanButton = new System.Windows.Forms.ToolStripButton();
         this.markdownEditorTextBox = new DownMarker.WinForms.RichTextBoxEx();
         this.splitContainer.Panel1.SuspendLayout();
         this.splitContainer.Panel2.SuspendLayout();
         this.splitContainer.SuspendLayout();
         this.menuStrip1.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // splitContainer
         // 
         this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer.Location = new System.Drawing.Point(0, 66);
         this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
         this.splitContainer.Name = "splitContainer";
         this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer.Panel1
         // 
         this.splitContainer.Panel1.Controls.Add(this.markdownEditorTextBox);
         // 
         // splitContainer.Panel2
         // 
         this.splitContainer.Panel2.Controls.Add(this.webBrowser);
         this.splitContainer.Size = new System.Drawing.Size(745, 480);
         this.splitContainer.SplitterDistance = 198;
         this.splitContainer.SplitterWidth = 5;
         this.splitContainer.TabIndex = 0;
         this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.HandleSplitterMoved);
         // 
         // webBrowser
         // 
         this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.webBrowser.IsWebBrowserContextMenuEnabled = false;
         this.webBrowser.Location = new System.Drawing.Point(0, 0);
         this.webBrowser.Margin = new System.Windows.Forms.Padding(4);
         this.webBrowser.MinimumSize = new System.Drawing.Size(27, 25);
         this.webBrowser.Name = "webBrowser";
         this.webBrowser.Size = new System.Drawing.Size(745, 277);
         this.webBrowser.TabIndex = 0;
         this.webBrowser.WebBrowserShortcutsEnabled = false;
         this.webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.HandleWebBrowserNavigated);
         this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.HandleWebBrowserNavigating);
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
         this.menuStrip1.Size = new System.Drawing.Size(745, 27);
         this.menuStrip1.TabIndex = 1;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.toolStripSeparator5,
            this.backMenuItem,
            this.forwardMenuItem,
            this.toolStripSeparator6,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.printToolStripMenuItem,
            this.toolStripSeparator7,
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // newToolStripMenuItem
         // 
         this.newToolStripMenuItem.Name = "newToolStripMenuItem";
         this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
         this.newToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.newToolStripMenuItem.Text = "&New";
         this.newToolStripMenuItem.Click += new System.EventHandler(this.HandleNewClick);
         // 
         // openToolStripMenuItem
         // 
         this.openToolStripMenuItem.Name = "openToolStripMenuItem";
         this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.openToolStripMenuItem.Text = "&Open...";
         this.openToolStripMenuItem.Click += new System.EventHandler(this.HandleOpenClick);
         // 
         // reloadToolStripMenuItem
         // 
         this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
         this.reloadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
         this.reloadToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.reloadToolStripMenuItem.Text = "&Reload From Disk";
         this.reloadToolStripMenuItem.Click += new System.EventHandler(this.HandleReloadClick);
         // 
         // toolStripSeparator5
         // 
         this.toolStripSeparator5.Name = "toolStripSeparator5";
         this.toolStripSeparator5.Size = new System.Drawing.Size(216, 6);
         // 
         // backMenuItem
         // 
         this.backMenuItem.Name = "backMenuItem";
         this.backMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
         this.backMenuItem.Size = new System.Drawing.Size(219, 24);
         this.backMenuItem.Text = "&Back";
         this.backMenuItem.Click += new System.EventHandler(this.HandleBackClick);
         // 
         // forwardMenuItem
         // 
         this.forwardMenuItem.Name = "forwardMenuItem";
         this.forwardMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
         this.forwardMenuItem.Size = new System.Drawing.Size(219, 24);
         this.forwardMenuItem.Text = "&Forward";
         this.forwardMenuItem.Click += new System.EventHandler(this.HandleForwardClick);
         // 
         // toolStripSeparator6
         // 
         this.toolStripSeparator6.Name = "toolStripSeparator6";
         this.toolStripSeparator6.Size = new System.Drawing.Size(216, 6);
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.saveToolStripMenuItem.Text = "&Save";
         this.saveToolStripMenuItem.Click += new System.EventHandler(this.HandleSaveClick);
         // 
         // saveAsToolStripMenuItem
         // 
         this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
         this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
         this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.saveAsToolStripMenuItem.Text = "Save &As...";
         this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.HandleSaveAsClick);
         // 
         // toolStripSeparator4
         // 
         this.toolStripSeparator4.Name = "toolStripSeparator4";
         this.toolStripSeparator4.Size = new System.Drawing.Size(216, 6);
         // 
         // printToolStripMenuItem
         // 
         this.printToolStripMenuItem.Name = "printToolStripMenuItem";
         this.printToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.printToolStripMenuItem.Text = "&Print...";
         this.printToolStripMenuItem.Click += new System.EventHandler(this.HandlePrintMenuItemClick);
         // 
         // toolStripSeparator7
         // 
         this.toolStripSeparator7.Name = "toolStripSeparator7";
         this.toolStripSeparator7.Size = new System.Drawing.Size(216, 6);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
         this.exitToolStripMenuItem.Text = "&Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.HandleExitClick);
         // 
         // editToolStripMenuItem
         // 
         this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.linkMenuItem,
            this.imageMenuItem,
            this.boldMenuItem,
            this.italicMenuItem,
            this.quoteMenuItem,
            this.codeBlockMenuItem,
            this.codeSpanMenuItem});
         this.editToolStripMenuItem.Name = "editToolStripMenuItem";
         this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
         this.editToolStripMenuItem.Text = "&Edit";
         // 
         // undoToolStripMenuItem
         // 
         this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
         this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
         this.undoToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
         this.undoToolStripMenuItem.Text = "Undo";
         this.undoToolStripMenuItem.Click += new System.EventHandler(this.HandleUndoClick);
         // 
         // redoToolStripMenuItem
         // 
         this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
         this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
         this.redoToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
         this.redoToolStripMenuItem.Text = "Redo";
         this.redoToolStripMenuItem.Click += new System.EventHandler(this.HandleRedoClick);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
         // 
         // linkMenuItem
         // 
         this.linkMenuItem.Name = "linkMenuItem";
         this.linkMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
         this.linkMenuItem.Size = new System.Drawing.Size(195, 24);
         this.linkMenuItem.Text = "&Link";
         this.linkMenuItem.Click += new System.EventHandler(this.HandleLinkClick);
         // 
         // imageMenuItem
         // 
         this.imageMenuItem.Name = "imageMenuItem";
         this.imageMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
         this.imageMenuItem.Size = new System.Drawing.Size(195, 24);
         this.imageMenuItem.Text = "Image";
         this.imageMenuItem.Click += new System.EventHandler(this.HandleImageClick);
         // 
         // boldMenuItem
         // 
         this.boldMenuItem.Name = "boldMenuItem";
         this.boldMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
         this.boldMenuItem.Size = new System.Drawing.Size(195, 24);
         this.boldMenuItem.Text = "Bold";
         this.boldMenuItem.Click += new System.EventHandler(this.HandleBoldClick);
         // 
         // italicMenuItem
         // 
         this.italicMenuItem.Name = "italicMenuItem";
         this.italicMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
         this.italicMenuItem.Size = new System.Drawing.Size(195, 24);
         this.italicMenuItem.Text = "Italic";
         this.italicMenuItem.Click += new System.EventHandler(this.HandleItalicClick);
         // 
         // quoteMenuItem
         // 
         this.quoteMenuItem.Name = "quoteMenuItem";
         this.quoteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
         this.quoteMenuItem.Size = new System.Drawing.Size(195, 24);
         this.quoteMenuItem.Text = "Quote";
         this.quoteMenuItem.Click += new System.EventHandler(this.HandleQuoteClick);
         // 
         // codeBlockMenuItem
         // 
         this.codeBlockMenuItem.Name = "codeBlockMenuItem";
         this.codeBlockMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
         this.codeBlockMenuItem.Size = new System.Drawing.Size(195, 24);
         this.codeBlockMenuItem.Text = "Code Block";
         this.codeBlockMenuItem.Click += new System.EventHandler(this.HandleCodeBlockClick);
         // 
         // codeSpanMenuItem
         // 
         this.codeSpanMenuItem.Name = "codeSpanMenuItem";
         this.codeSpanMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
         this.codeSpanMenuItem.Size = new System.Drawing.Size(195, 24);
         this.codeSpanMenuItem.Text = "Code Span";
         this.codeSpanMenuItem.Click += new System.EventHandler(this.HandleCodeSpanClick);
         // 
         // viewToolStripMenuItem
         // 
         this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalSplitterToolStripMenuItem,
            this.verticalSplitterToolStripMenuItem,
            this.plainStyleToolStripMenuItem,
            this.toolStripSeparator1,
            this.showEditorToolStripMenuItem});
         this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
         this.viewToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
         this.viewToolStripMenuItem.Text = "&View";
         // 
         // horizontalSplitterToolStripMenuItem
         // 
         this.horizontalSplitterToolStripMenuItem.Name = "horizontalSplitterToolStripMenuItem";
         this.horizontalSplitterToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
         this.horizontalSplitterToolStripMenuItem.Text = "&Horizontal Splitter";
         this.horizontalSplitterToolStripMenuItem.Click += new System.EventHandler(this.HandleHorizontalMenuItemClick);
         // 
         // verticalSplitterToolStripMenuItem
         // 
         this.verticalSplitterToolStripMenuItem.Name = "verticalSplitterToolStripMenuItem";
         this.verticalSplitterToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
         this.verticalSplitterToolStripMenuItem.Text = "&Vertical Splitter";
         this.verticalSplitterToolStripMenuItem.Click += new System.EventHandler(this.HandleVerticalMenuItemClick);
         // 
         // plainStyleToolStripMenuItem
         // 
         this.plainStyleToolStripMenuItem.Name = "plainStyleToolStripMenuItem";
         this.plainStyleToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
         this.plainStyleToolStripMenuItem.Text = "Plain Style";
         this.plainStyleToolStripMenuItem.Click += new System.EventHandler(this.HandlePlainStyleClick);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
         // 
         // showEditorToolStripMenuItem
         // 
         this.showEditorToolStripMenuItem.Name = "showEditorToolStripMenuItem";
         this.showEditorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
         this.showEditorToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
         this.showEditorToolStripMenuItem.Text = "&Editor";
         this.showEditorToolStripMenuItem.Click += new System.EventHandler(this.HandleEditClick);
         // 
         // toolStrip1
         // 
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.toolStripSeparator2,
            this.openButton,
            this.saveButton,
            this.editButton,
            this.editToolsSeparator,
            this.linkButton,
            this.imageButton,
            this.boldButton,
            this.italicButton,
            this.quoteButton,
            this.codeBlockButton,
            this.codeSpanButton});
         this.toolStrip1.Location = new System.Drawing.Point(0, 27);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(745, 39);
         this.toolStrip1.TabIndex = 2;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // backButton
         // 
         this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.backButton.Image = global::DownMarker.WinForms.Properties.Resources.back32;
         this.backButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.backButton.Name = "backButton";
         this.backButton.Size = new System.Drawing.Size(36, 36);
         this.backButton.Text = "Previous Document";
         this.backButton.ToolTipText = "Previous Document (Ctrl+Left)";
         this.backButton.Click += new System.EventHandler(this.HandleBackClick);
         // 
         // forwardButton
         // 
         this.forwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.forwardButton.Image = global::DownMarker.WinForms.Properties.Resources.forward32;
         this.forwardButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.forwardButton.Name = "forwardButton";
         this.forwardButton.Size = new System.Drawing.Size(36, 36);
         this.forwardButton.Text = "Next Document";
         this.forwardButton.ToolTipText = "Next Document (Ctrl+Right)";
         this.forwardButton.Click += new System.EventHandler(this.HandleForwardClick);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
         // 
         // openButton
         // 
         this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.openButton.Image = global::DownMarker.WinForms.Properties.Resources.folder;
         this.openButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.openButton.Name = "openButton";
         this.openButton.Size = new System.Drawing.Size(36, 36);
         this.openButton.Text = "Open";
         this.openButton.ToolTipText = "Open (Ctrl+O)";
         this.openButton.Click += new System.EventHandler(this.HandleOpenClick);
         // 
         // saveButton
         // 
         this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.saveButton.Image = global::DownMarker.WinForms.Properties.Resources.save_32;
         this.saveButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.saveButton.Name = "saveButton";
         this.saveButton.Size = new System.Drawing.Size(36, 36);
         this.saveButton.Text = "Save";
         this.saveButton.ToolTipText = "Save (Ctrl+S)";
         this.saveButton.Click += new System.EventHandler(this.HandleSaveClick);
         // 
         // editButton
         // 
         this.editButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.editButton.Image = global::DownMarker.WinForms.Properties.Resources.edit32;
         this.editButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.editButton.Name = "editButton";
         this.editButton.Size = new System.Drawing.Size(36, 36);
         this.editButton.Text = "Edit";
         this.editButton.ToolTipText = "Edit (Ctrl+E)";
         this.editButton.Click += new System.EventHandler(this.HandleEditClick);
         // 
         // editToolsSeparator
         // 
         this.editToolsSeparator.Name = "editToolsSeparator";
         this.editToolsSeparator.Size = new System.Drawing.Size(6, 39);
         // 
         // linkButton
         // 
         this.linkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.linkButton.Image = global::DownMarker.WinForms.Properties.Resources.globe_link;
         this.linkButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.linkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.linkButton.Name = "linkButton";
         this.linkButton.Size = new System.Drawing.Size(36, 36);
         this.linkButton.Text = "Link";
         this.linkButton.ToolTipText = "Link (Ctrl+L)";
         this.linkButton.Click += new System.EventHandler(this.HandleLinkClick);
         // 
         // imageButton
         // 
         this.imageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.imageButton.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.imageButton.Image = global::DownMarker.WinForms.Properties.Resources.shapes32;
         this.imageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.imageButton.Name = "imageButton";
         this.imageButton.Size = new System.Drawing.Size(23, 36);
         this.imageButton.Text = "Image";
         this.imageButton.ToolTipText = "Image (Ctrl+M)";
         this.imageButton.Click += new System.EventHandler(this.HandleImageClick);
         // 
         // boldButton
         // 
         this.boldButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.boldButton.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
         this.boldButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.boldButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.boldButton.Name = "boldButton";
         this.boldButton.Size = new System.Drawing.Size(33, 36);
         this.boldButton.Text = "B";
         this.boldButton.ToolTipText = "Bold (Ctrl+B)";
         this.boldButton.Click += new System.EventHandler(this.HandleBoldClick);
         // 
         // italicButton
         // 
         this.italicButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.italicButton.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Italic);
         this.italicButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.italicButton.Name = "italicButton";
         this.italicButton.Size = new System.Drawing.Size(26, 36);
         this.italicButton.Text = "I";
         this.italicButton.ToolTipText = "Italic (Ctrl+I)";
         this.italicButton.Click += new System.EventHandler(this.HandleItalicClick);
         // 
         // quoteButton
         // 
         this.quoteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.quoteButton.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.quoteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.quoteButton.Name = "quoteButton";
         this.quoteButton.Size = new System.Drawing.Size(33, 36);
         this.quoteButton.Text = "“";
         this.quoteButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
         this.quoteButton.ToolTipText = "Quote (Ctrl+Q)";
         this.quoteButton.Click += new System.EventHandler(this.HandleQuoteClick);
         // 
         // codeBlockButton
         // 
         this.codeBlockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.codeBlockButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.codeBlockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.codeBlockButton.Name = "codeBlockButton";
         this.codeBlockButton.Size = new System.Drawing.Size(40, 36);
         this.codeBlockButton.Text = "{ }";
         this.codeBlockButton.ToolTipText = "Code Block (Ctrl+K)";
         this.codeBlockButton.Click += new System.EventHandler(this.HandleCodeBlockClick);
         // 
         // codeSpanButton
         // 
         this.codeSpanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.codeSpanButton.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.codeSpanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.codeSpanButton.Name = "codeSpanButton";
         this.codeSpanButton.Size = new System.Drawing.Size(33, 36);
         this.codeSpanButton.Text = "TT";
         this.codeSpanButton.ToolTipText = "Code Span (Ctrl+T)";
         this.codeSpanButton.Click += new System.EventHandler(this.HandleCodeSpanClick);
         // 
         // markdownEditorTextBox
         // 
         this.markdownEditorTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.markdownEditorTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.markdownEditorTextBox.Location = new System.Drawing.Point(0, 0);
         this.markdownEditorTextBox.Name = "markdownEditorTextBox";
         this.markdownEditorTextBox.Size = new System.Drawing.Size(745, 198);
         this.markdownEditorTextBox.TabIndex = 0;
         this.markdownEditorTextBox.Text = "";
         this.markdownEditorTextBox.VerticalScrollPosition = 0;
         this.markdownEditorTextBox.SelectionChanged += new System.EventHandler(this.HandleMarkdownEditorTextBoxSelectionChanged);
         this.markdownEditorTextBox.TextChanged += new System.EventHandler(this.HandleTextBoxTextChanged);
         // 
         // MainView
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(745, 546);
         this.Controls.Add(this.splitContainer);
         this.Controls.Add(this.toolStrip1);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "MainView";
         this.Text = "DownMarker";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
         this.splitContainer.Panel1.ResumeLayout(false);
         this.splitContainer.Panel2.ResumeLayout(false);
         this.splitContainer.ResumeLayout(false);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer;
      private DownMarker.WinForms.RichTextBoxEx markdownEditorTextBox;
      private System.Windows.Forms.WebBrowser webBrowser;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem horizontalSplitterToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem verticalSplitterToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripMenuItem showEditorToolStripMenuItem;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripButton backButton;
      private System.Windows.Forms.ToolStripButton forwardButton;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripButton editButton;
      private System.Windows.Forms.ToolStripButton linkButton;
      private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem linkMenuItem;
      private System.Windows.Forms.ToolStripButton boldButton;
      private System.Windows.Forms.ToolStripMenuItem boldMenuItem;
      private System.Windows.Forms.ToolStripButton saveButton;
      private System.Windows.Forms.ToolStripSeparator editToolsSeparator;
      private System.Windows.Forms.ToolStripMenuItem italicMenuItem;
      private System.Windows.Forms.ToolStripButton italicButton;
      private System.Windows.Forms.ToolStripMenuItem quoteMenuItem;
      private System.Windows.Forms.ToolStripButton quoteButton;
      private System.Windows.Forms.ToolStripMenuItem codeBlockMenuItem;
      private System.Windows.Forms.ToolStripButton codeBlockButton;
      private System.Windows.Forms.ToolStripButton imageButton;
      private System.Windows.Forms.ToolStripMenuItem imageMenuItem;
      private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.ToolStripMenuItem codeSpanMenuItem;
      private System.Windows.Forms.ToolStripButton codeSpanButton;
      private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
      private System.Windows.Forms.ToolStripMenuItem backMenuItem;
      private System.Windows.Forms.ToolStripMenuItem forwardMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
      private System.Windows.Forms.ToolStripButton openButton;
      private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
      private System.Windows.Forms.ToolStripMenuItem plainStyleToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
   }
}

