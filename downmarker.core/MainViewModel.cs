using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DownMarker.Core.Properties;

namespace DownMarker.Core
{
    /// <summary>
    /// State and actions for the <see cref="MainFormView"/>.
    /// </summary>
    public class MainViewModel : ViewModelBase<MainViewModel>
    {
        private const string URL_STARTPAGE = "downmarker:start";
        private const string URL_EXAMPLE = "downmarker:example";

        private readonly string temporaryHtmlFile = Path.GetTempFileName() + ".html";

        private readonly IMarkdownTransformer markdownTransformer;
        private readonly IPrompt prompt;
        private readonly IFileSystem fileSystem;
        private readonly IUriHandler uriHandler;
        private readonly PersistentState persistentState;

        // history
        private IHistory<string> navigationHistory = new LinkedListHistory<string>(1000);
        private IHistory<EditorState> editorHistory = new EditorStateHistory(1000);
        private bool restoringState;

        // current file information
        private string currentFullPath;
        private bool unsavedChanges;

        // text editor state
        private string markdownText;
        private int selectionStart;
        private int selectionLength;
        private bool editorVisible;

        // other
        private bool canGoBack;
        private bool canGoForward;
        private string title;
        private bool canUndo;
        private bool canRedo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/>
        /// class.
        /// </summary>
        /// <param name="markdownTransformer">
        /// The service to use to convert markdown text to HTML text.
        /// </param>
        /// <param name="prompt">
        /// The service to use to prompt the user.
        /// </param>
        /// <param name="prompt">
        /// The service to use to open and save files.
        /// </param>
        /// <param name="uriHandler">
        /// The service to use to open links to any content that is not a
        /// markdown file.
        /// </param>
        public MainViewModel(
                IMarkdownTransformer markdownTransformer,
                IPrompt prompt,
                IFileSystem fileSystem,
                IUriHandler uriHandler,
                PersistentState persistentState)
        {
            if (markdownTransformer == null)
                throw new ArgumentNullException("markdownTransformer");
            if (prompt == null)
                throw new ArgumentException("prompt");
            if (fileSystem == null)
                throw new ArgumentNullException("fileSystem");
            if (uriHandler == null)
                throw new ArgumentNullException("uriHandler");
            if (persistentState == null)
                throw new ArgumentNullException("persistentState");

            this.markdownTransformer = markdownTransformer;
            this.markdownTransformer.HtmlChanged += HandleHtmlChanged;
            this.prompt = prompt;
            this.fileSystem = fileSystem;
            this.uriHandler = uriHandler;
            this.persistentState = persistentState;

            this.markdownTransformer.PlainStyle = this.persistentState.PlainStyle;
            ShowStartPage();
            UpdateTitle();
        }

        public bool CanGoBack
        {
            get
            {
                return this.canGoBack;
            }
            private set
            {
                if (this.canGoBack != value)
                {
                    this.canGoBack = value;
                    OnPropertyChanged(x => x.CanGoBack);
                }
            }
        }

        public bool CanGoForward
        {
            get
            {
                return this.canGoForward;
            }
            private set
            {
                if (this.canGoForward != value)
                {
                    this.canGoForward = value;
                    OnPropertyChanged(x => x.CanGoForward);
                }
            }
        }

        public bool CanUndo
        {
            get
            {
                return this.canUndo;
            }
            private set
            {
                if (this.canUndo != value)
                {
                    this.canUndo = value;
                    OnPropertyChanged(x => x.CanUndo);
                }
            }
        }

        public bool CanRedo
        {
            get
            {
                return this.canRedo;
            }
            private set
            {
                if (this.canRedo != value)
                {
                    this.canRedo = value;
                    OnPropertyChanged(x => x.CanRedo);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the editor panel is
        /// currently visible.
        /// </summary>
        public bool EditorVisible
        {
            get
            {
                return this.editorVisible;
            }
            set
            {
                if (this.editorVisible != value)
                {
                    this.editorVisible = value;
                    OnPropertyChanged(x => x.EditorVisible);
                }

            }
        }

        public int HtmlScroll { get; set; }

        public bool PlainStyle
        {
           get
           {
              return this.persistentState.PlainStyle;
           }
           set
           {
              if (this.PlainStyle != value)
              {
                 this.persistentState.PlainStyle = value;
                 this.markdownTransformer.PlainStyle = value;
                 UpdateHtml();
                 this.OnPropertyChanged(x => x.PlainStyle);
              }
           }
        }


        /// <summary>
        /// Gets the title of the main form.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                if (this.title != value)
                {
                    this.title = value;
                    OnPropertyChanged(x => x.Title);
                }
            }
        }

        public bool UnsavedChanges
        {
            get
            {
                return this.unsavedChanges;
            }
            set
            {
                if (this.unsavedChanges != value)
                {
                    this.unsavedChanges = value;
                    OnPropertyChanged(x => x.UnsavedChanges);
                }
            }
        }

        public int EditorSize
        {
            get
            {
                return persistentState.EditorSize;
            }
            set
            {
                if (persistentState.EditorSize != value)
                {
                    persistentState.EditorSize = value;
                     OnPropertyChanged(x => x.EditorSize);
                }
            }
        }

        /// <summary>
        /// Gets or sets the orientation of editor / html view splitter.
        /// </summary>
        public ViewOrientation EditorOrientation
        {
            get
            {
                return persistentState.EditorOrientation;
            }
            set
            {
                if (persistentState.EditorOrientation != value)
                {
                    persistentState.EditorOrientation = value;
                    OnPropertyChanged(x => x.EditorOrientation);
                }
                this.EditorVisible = true;
            }
        }

        /// <summary>
        /// Gets or sets the markdown text that is being edited.
        /// </summary>
        /// <remarks>
        /// Never <c>null</c>. Line endings are always UNIX style,
        /// i.e. a single line feed character.
        /// </remarks>
        public string MarkdownText
        {
            get
            {
                return this.markdownText;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (this.markdownText != value)
                {
                    this.markdownText = value;
                    AddEditorHistory();
                    UnsavedChanges = true;
                    OnPropertyChanged(x => x.MarkdownText);
                    UpdateTitle();
                    UpdateHtml();
                }
            }
        }

        /// <summary>
        /// Gets the path to the file that contains the result of the transformation
        /// of <see cref="MarkdownText"/> into HTML.
        /// </summary>
        /// <remarks>
        /// This file doesn't exist until after the first time
        /// <see cref="TemporaryHtmlFileContentChanged"/> is raised.
        /// </remarks>
        public string TemporaryHtmlFile
        {
            get
            {
               return temporaryHtmlFile;
            }
        }

        public int SelectionStart
        {
            get
            {
                return this.selectionStart;
            }
            set
            {
                if (this.selectionStart != value)
                {
                    this.selectionStart = value;
                    OnPropertyChanged(x => x.SelectionStart);
                }
            }
        }

        public int SelectionLength
        {
            get
            {
                return this.selectionLength;
            }
            set
            {
                this.selectionLength = value;
                OnPropertyChanged(x => x.SelectionLength);
            }
        }

        private void AddEditorHistory()
        {
            if (!restoringState)
            {
                this.editorHistory.Add(
                    new EditorState(
                        this.MarkdownText,
                        this.SelectionStart));
                UpdateDoCommands();
            }
        }

        private void ClearTextHistory()
        {
            this.editorHistory.Clear();
            UpdateDoCommands();
        }

        public void GoBack()
        {
            if (!CanGoBack)
                throw new InvalidOperationException("CanGoBack is false");

            if (!this.PromptSave())
                return; // prompted but then user cancelled or the saving failed

            bool success = OpenInternal(this.navigationHistory.Back());
            if (success)
                UpdateNavigationCommands();
            else
                this.navigationHistory.Forward(); // restore nav pointer
        }

        public void ToggleEdit()
        {
            this.EditorVisible = !this.EditorVisible;
        }

        public void GoForward()
        {
            if (!CanGoForward)
                throw new InvalidOperationException("CanGoForward is false");

            if (!this.PromptSave())
                return; // prompted but then user cancelled or the saving failed

            bool success = OpenInternal(this.navigationHistory.Forward());
            if (success)
                UpdateNavigationCommands();
            else
                this.navigationHistory.Back(); //restore nav pointer
        }

        private Substring GetSelectionOrWordAtCursor()
        {
            return GetSelection(x => !char.IsLetterOrDigit(this.MarkdownText[x]));
        }

        private Substring GetSelectionOrParagraphAtCursor()
        {
            return GetSelection(x => IsLeftBlockBoundary(x), x=> IsRightBlockBoundary(x));
        }

        private bool IsLeftBlockBoundary(int position)
        {
            return IsLineEndingOrOutside(position) && IsLineEndingOrOutside(position-1);
        }

        private bool IsRightBlockBoundary(int position)
        {
            return IsLineEndingOrOutside(position) && IsLineEndingOrOutside(position + 1);
        }

        private bool IsLineEndingOrOutside(int position)
        {
            return (position < 0) || (position >= this.MarkdownText.Length)
                || (this.MarkdownText[position] == '\n');
        }

        /// <summary>
        /// Returns the substring for the current selection, or an expansion.
        /// </summary>
        /// <param name="leftBoundary">
        /// Predicate used to find a left boundary when expanding an empty selection.
        /// </param>
        /// <param name="rightBoundary">
        /// Predicate used to find a right boundary when expanding an empty selection.
        /// If omitted or <c>null</c>, the <see cref="leftBoundary"/> predicate is used.
        /// </param>
        /// <remarks>
        /// If the current selection is empty, it is expanded to
        /// the left and right until a position in <see cref="MarkdownText"/>
        /// is encountered which satisfies the given predicate. Those boundary
        /// positions will not be included in the expanded selection.
        /// </remarks>
        private Substring GetSelection(Predicate<int> leftBoundary, Predicate<int> rightBoundary=null)
        {
            if (leftBoundary == null)
                throw new ArgumentException();
            if (rightBoundary == null)
                rightBoundary = leftBoundary;

            // range to replace
            int firstChar = this.selectionStart;
            int lastChar = this.SelectionStart + this.SelectionLength - 1;

            // zero-width selection is expanded to the left and right
            if (this.SelectionLength == 0)
            {
                while ((firstChar > 0) && !leftBoundary(firstChar - 1))
                {
                    firstChar--;
                }
                while ((lastChar < this.MarkdownText.Length - 1)
                    && !rightBoundary(lastChar + 1))
                {
                    lastChar++;
                }
            }

            int length = lastChar - firstChar + 1;
            return new Substring()
            {
                StartIndex = firstChar,
                Value = this.MarkdownText.Substring(firstChar, length)
            };
        }

        private static string StripPrefix(string line, string prefix)
        {
            if (line.StartsWith(prefix))
                return line.Substring(prefix.Length);
            else
                return line;
        }

        private void ToggleBlockPrefix(string prefix)
        {
            var selection = GetSelectionOrParagraphAtCursor();

            // shrink selection to ignore trailing linefeed
            if (selection.Value.EndsWith("\n"))
                selection.Value = selection.Value.Substring(0, selection.Value.Length - 1);
            
            IEnumerable<string> lines = selection.Value.Split('\n');
            string replacement;
            int newSelectionStart = selection.StartIndex;
            int newSelectionLength;
            if (lines.All(x => x.StartsWith(prefix)))
            {
                // already quoted, so unquote
                replacement = string.Join("\n", lines.Select(x => StripPrefix(x, prefix)).ToArray());
                newSelectionLength = replacement.Length;
            }
            else
            {
                replacement = string.Join("\n", lines.Select(x => prefix + x).ToArray());
                newSelectionLength = replacement.Length;
                // make sure the result is a block surrounded by empty lines
                if (!IsLeftBlockBoundary(selection.StartIndex - 1))
                {
                    if (IsPrecededBy(selection, "\n"))
                    {
                        replacement = "\n" + replacement;
                        newSelectionStart++;
                    }
                    else
                    {
                        replacement = "\n\n" + replacement;
                        newSelectionStart += 2;
                    }
                }
                if (!IsRightBlockBoundary(selection.StartIndex+selection.Value.Length))
                {
                    if (IsFollowedBy(selection, "\n"))
                        replacement = replacement + "\n";
                    else
                        replacement = replacement + "\n\n";
                }
            }
            this.MarkdownText = this.MarkdownText.ReplaceSubstring(
                    selection.StartIndex,
                    selection.Value.Length,
                    replacement);
            this.SelectionStart = newSelectionStart;
            this.SelectionLength = newSelectionLength;
        }

        public void Quote()
        {
            ToggleBlockPrefix("> ");
        }

        private string GetVersionInfo()
        {
           var version = Assembly.GetExecutingAssembly().GetCustomAttributes
              (typeof(AssemblyFileVersionAttribute), false)[0] as AssemblyFileVersionAttribute;
           var informationalVersion = Assembly.GetExecutingAssembly().GetCustomAttributes
              (typeof(AssemblyInformationalVersionAttribute), false)[0] as AssemblyInformationalVersionAttribute;
           return "DownMarker " + version.Version + " built from revision " + informationalVersion.InformationalVersion;
        }

        private void ShowStartPage()
        {
            string recentlyUsed = null;
            if (this.persistentState.RecentlyUsedDocuments.Any())
            {
                recentlyUsed = string.Join(
                    "\n",
                    this.persistentState
                        .RecentlyUsedDocuments
                        .Select(file => "[" + Path.GetFileName(file) + "](" + (new Uri(file)).AbsoluteUri + ")  ")
                        .ToArray());
            }
            else
            {
               recentlyUsed = "None so far...";
            }
            MarkdownText = Resources.startpageTemplate
               .Replace("%recentlyused%", recentlyUsed)
               .Replace("%versioninfo%", GetVersionInfo());

            this.currentFullPath = URL_STARTPAGE;
            this.AddNavigationHistory();
            this.ResetEditor();
        }

        private void ShowExample()
        {
            MarkdownText = Resources.markdownhelp;
            this.currentFullPath = null;
            this.AddNavigationHistory();
            this.ResetEditor();
        }

        public void CodeBlock()
        {
            ToggleBlockPrefix("    ");
        }

        public void CodeSpan()
        {
            ToggleSurroundingToken("`", "code");
        }

        public void Link()
        {
            Link(image: false);
        }

        public void Image()
        {
            Link(image: true);
        }

        private void Link(bool image)
        {
            Link suggestedLink = null;
            Substring toReplace = null;

            // look for existing link first
            if (this.SelectionLength == 0)
            {
                suggestedLink = GetLinkAt(this.selectionStart, out toReplace);
            }

            // use selected text or word at cursor otherwise
            if (suggestedLink == null)
            {
                toReplace = GetSelectionOrWordAtCursor();
                string linkTarget = "";
                if (!image)
                {
                   linkTarget = toReplace.Value + ".md";
                }
                suggestedLink = new Link() { Text = toReplace.Value, Target = linkTarget };
            }

            Link link = this.prompt.EditLink(
                suggestedLink, this.currentFullPath, image);
            if (link != null)
            {
                // replace selection by link
                int start = toReplace.StartIndex;
                int length = toReplace.Value.Length;
                string markdownLink = (image ? "!" : "") + link.ToMarkDown();
                this.MarkdownText = this.MarkdownText.ReplaceSubstring(
                    start, length, markdownLink);
                this.SelectionStart = start;
                this.SelectionLength = markdownLink.Length;
            }
        }

        private Link GetLinkAt(int position, out Substring linkText)
        {
            var linkPattern = new Regex(@"\!?\[([^\]]*)\]\(([^)]*)\)");
            foreach (Match match in linkPattern.Matches(this.MarkdownText))
            {
                if ((match.Index <= position) && (position <= match.Index + match.Length))
                {
                    linkText = new Substring()
                    {
                        StartIndex = match.Index,
                        Value = match.Value
                    };
                    return new Link()
                    {
                        Text = match.Groups[1].Value,
                        Target = match.Groups[2].Value
                    };
                }
            }
            linkText = null;
            return null;
        }

        private bool IsPrecededBy(Substring sub, string token)
        {
            int tokenStart = sub.StartIndex - token.Length;
            if (tokenStart < 0)
            {
                return false; // not enough characters in front
            }
            string prefix = this.MarkdownText.Substring(
                tokenStart,
                token.Length);
            return (prefix == token);
        }

        private bool IsFollowedBy(Substring sub, string token)
        {
            int tokenStart = sub.StartIndex + sub.Value.Length;
            if (tokenStart + token.Length > this.markdownText.Length)
            {
                return false; // not enough characters after
            }
            string suffix = this.MarkdownText.Substring(
                tokenStart,
                token.Length);
            return (suffix == token);
        }

        private bool IsSurroundedWith(Substring sub, string token)
        {
            return IsPrecededBy(sub, token) && IsFollowedBy(sub, token);
        }

        public void Bold()
        {
            ToggleSurroundingToken(token: "**", placeholder: "bold");
        }

        public void Italic()
        {
            ToggleSurroundingToken(token: "*", placeholder: "italic");
        }

        private void ToggleSurroundingToken(string token, string placeholder)
        {
            Substring selection = GetSelectionOrWordAtCursor();
            // remove surrounding "**", else add them
            if (IsSurroundedWith(selection, token))
            {
                this.MarkdownText = this.MarkdownText.ReplaceSubstring(
                    selection.StartIndex - token.Length,
                    selection.Value.Length + 2 * token.Length,
                    selection.Value);
                // select the unbolded text
                this.SelectionStart = selection.StartIndex - token.Length;
                this.SelectionLength = selection.Value.Length;
            }
            else
            {
                string selected = selection.Value;
                if (selected == "")
                {
                    selected = placeholder;
                }
                this.MarkdownText = this.MarkdownText.ReplaceSubstring(
                    selection.StartIndex,
                    selection.Value.Length,
                    token + selected + token);
                // select the bolded text
                this.SelectionStart = selection.StartIndex + token.Length;
                this.SelectionLength = selected.Length;
            }
        }


        private void UpdateNavigationCommands()
        {
            CanGoBack = this.navigationHistory.CanGoBack;
            CanGoForward = this.navigationHistory.CanGoForward;
        }

        private void UpdateDoCommands()
        {
            CanUndo = this.editorHistory.CanGoBack;
            CanRedo = this.editorHistory.CanGoForward;
        }

        private void UpdateHtml()
        {
            // result is processed by markdownTransformed.HtmlChanged handler
            this.markdownTransformer.TransformToHtml(MarkdownText);
        }

        /// <summary>
        /// Prompts to save if necessary.
        /// </summary>
        /// <returns>
        /// <c>true</c> if all is well: no prompt was necessary, 
        /// or the user confirmed that no save was necessary,
        /// or any unsaved changes were successfully saved.
        /// <c>false</c> if something went wrong: the user cancelled,
        /// or the save failed.
        /// </returns>
        private bool PromptSave()
        {
            if (UnsavedChanges)
            {
                var result = this.prompt.QuestionSaveUnsavedChanges();
                switch (result)
                {
                    case PromptResult.Yes:
                        return Save();
                    case PromptResult.No:
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool SaveAs()
        {
            string chosenFileName = this.prompt.QuestionSaveAs(GetCurrentLocation());
            if (chosenFileName == null)
            {
                return false;
            }
            else
            {
                return Save(chosenFileName);
            }
        }

        private bool Save(string fileName)
        {
            try
            {
                this.fileSystem.WriteTextFile(fileName, MarkdownText);
                this.currentFullPath = fileName;
                UnsavedChanges = false;
                AddNavigationHistory();
                UpdateTitle();
                return true;
            }
            catch (IOException e)
            {
                this.prompt.ShowError(e.Message, "Failed to save");
                return false;
            }
        }

        /// <summary>   
        /// Opens the specified markdown file.
        /// </summary>
        public bool Open(string fileName)
        {
            bool success = OpenInternal(fileName);
            if (success)
            {
                AddNavigationHistory();
            }
            return success;
        }

        private void ResetEditor()
        {
           this.UnsavedChanges = false;
           this.SelectionStart = 0;
           this.SelectionLength = 0;
           this.EditorVisible = false;
        }

        /// <summary>
        /// Opens the specified markdown file without touching the navigation
        /// history.
        /// </summary>
        private bool OpenInternal(string fileName)
        {
            if (fileName == URL_STARTPAGE)
            {
               ShowStartPage();
               return true;
            }
            try
            {
                var absoluteFilePath = this.fileSystem.GetAbsoluteFilePath(fileName);
                this.MarkdownText = this.fileSystem
                    .ReadTextFile(absoluteFilePath)
                    .NormalizeToUnixLineEndings();
                this.currentFullPath = absoluteFilePath;
                ClearTextHistory();
                HtmlScroll = 0;
                ResetEditor();
                UpdateTitle();
                return true;
            }
            catch (IOException e)
            {
                this.prompt.ShowError(e.Message, "Failed to open file");
                return false;
            }
        }

        private void AddNavigationHistory()
        {
            if (navigationHistory.Current != this.currentFullPath)
            {
                navigationHistory.Add(this.currentFullPath);
                UpdateNavigationCommands();
                if (this.currentFullPath != null && !this.currentFullPath.StartsWith("downmarker:"))
                {
                    persistentState.TouchRecentlyUsed(this.currentFullPath);
                }
            }
        }

        public bool Save()
        {
            if (!IsFileUrl(this.currentFullPath))
            {
                return SaveAs();
            }
            else
            {
                return Save(this.currentFullPath);
            }
        }

        public void New()
        {
            if (PromptSave())
            {
                this.EditorVisible = true;
                this.MarkdownText = "# Title";
                this.SelectionStart = 2;
                this.SelectionLength = 5;
                this.editorHistory.Clear();
                UpdateDoCommands();
                this.currentFullPath = null;
                this.UnsavedChanges = false;
                AddNavigationHistory();
                UpdateTitle();
            }
        }

        private string GetCurrentLocation()
        {
            if (currentFullPath == null)
            {
                return null;
            }
            else
            {
                return Path.GetDirectoryName(currentFullPath);
            }
        }

        public void Open()
        {
            if (!this.PromptSave())
                return; // prompted but then user cancelled or the saving failed

            string chosenFileName = this.prompt.QuestionOpenFile(GetCurrentLocation());
            if (chosenFileName != null)
            {
                Open(chosenFileName);
            }
        }

        public void Navigate(Uri uri)
        {
            // ignore empty "file:///" urls from mono webbrowser
            if (uri.OriginalString == "file:///")
            {
                return;
            }

            if (uri.OriginalString == "downmarker:new")
            {
               New();
               return;
            }

            if (uri.OriginalString == "downmarker:open")
            {
                Open();
                return;
            }

            if (uri.OriginalString == URL_EXAMPLE)
            {
                ShowExample();
                return;
            }

            // transform about:foo urls back to relative links,
            // to work around questionable behavior of the WebBrowser control
            if (uri.IsAbsoluteUri && (uri.Scheme == "about"))
            {
                var blank = new Uri("about:blank");
                uri = blank.MakeRelativeUri(uri);
            }

            // make relative URIs absolute or give up with error message
            if (!uri.IsAbsoluteUri)
            {
                if (IsFileUrl(this.currentFullPath))
                {
                    var baseUri = new Uri(this.currentFullPath);
                    uri = new Uri(baseUri, uri);
                }
                else
                {
                    this.prompt.ShowError(
                            String.Format(
                                "Cannot navigate to URI \"{0}\". "
                                + "A relative URI cannot be resolved until the document is saved.",
                                uri.ToString()),
                            "Navigation Error");
                    return;
                }
            }

            // Open absolute URI. Use external application if it is not a markdown file.
            if ((uri.Scheme == Uri.UriSchemeFile) && (uri.LocalPath.ToLower().EndsWith(".md")))
            {
                PromptSave();
                Open(uri.LocalPath);
            }
            else
            {
                this.uriHandler.OpenAbsoluteUri(uri);
            }
        }

        private void DeleteIfExists(string absoluteFilePath)
        {
            if (fileSystem.Exists(absoluteFilePath))
            {
                fileSystem.Delete(absoluteFilePath);
            }
        }

        public void Exit()
        {
            if (PromptSave())
            {
                DeleteIfExists(TemporaryHtmlFile);
                OnRequestClose();
            }
        }

        private void OnRequestClose()
        {
            if (RequestClose != null)
            {
                RequestClose(this, EventArgs.Empty);
            }
        }

        private bool IsFileUrl(string url)
        {
            return (url != null) && (!url.StartsWith("downmarker:"));
        }

        private void UpdateTitle()
        {
            string documentTitle;
            if (!IsFileUrl(this.currentFullPath))
            {
                documentTitle = "[No Name]";
            }
            else
            {
                string changeIndicator = UnsavedChanges ? "*" : "";
                string shortName =
                    Path.GetFileNameWithoutExtension(this.currentFullPath);
                documentTitle = string.Format("{0}{1} ({2})",
                    shortName,
                    changeIndicator,
                    this.currentFullPath);
            }
            this.Title = string.Format("{0} - DownMarker", documentTitle);
        }

        public void Undo()
        {
            var state = editorHistory.Back();
            RestoreEditorState(state);
            UpdateDoCommands();
        }

        public void Redo()
        {
            var state = editorHistory.Forward();
            RestoreEditorState(state);
            UpdateDoCommands();
        }

        public void Reload()
        {
            if (this.currentFullPath != null && !this.currentFullPath.StartsWith("downmarker:"))
            {
                OpenInternal(this.currentFullPath);
            }
        }

        public void ExportHtml()
        {
            string target = this.prompt.QuestionSaveHtml(GetCurrentLocation());
            if (target != null)
                fileSystem.WriteTextFile(target, this.markdownTransformer.Html);
        }

        private void RestoreEditorState(EditorState state)
        {
            restoringState = true;
            try
            {
                this.MarkdownText = state.Text;
                this.SelectionStart = state.SelectionStart;
                this.selectionLength = 0;
            }
            finally
            {
                restoringState = false;
            }
        }

        private string RewriteRelativeLinks(string html)
        {
            if (this.currentFullPath == null)
                return html;
            if (html == null)
                return html;

            MatchEvaluator linkRewriter = 
                match => 
                {
                    Uri uri;
                    if (Uri.TryCreate(match.Value, UriKind.RelativeOrAbsolute, out uri))
                    {
                        if (!uri.IsAbsoluteUri)
                        {
                            var absoluteUri = new Uri(new Uri(this.currentFullPath), uri);
                            return absoluteUri.ToString();
                        }
                    }
                    return match.Value;
                };

            return Regex.Replace(
                html,
                @"(?<=(href|src)=['""])[^'""<>]+(?=['""])", 
                linkRewriter,
                RegexOptions.IgnoreCase);
        }

        private void HandleHtmlChanged(object sender, EventArgs args)
        {
            this.fileSystem.WriteTextFile(
                absoluteFilePath: this.TemporaryHtmlFile, 
                content: RewriteRelativeLinks(this.markdownTransformer.Html));
            if (TemporaryHtmlFileContentChanged != null)
            {
                TemporaryHtmlFileContentChanged(this, EventArgs.Empty);
            }
        }


        public event EventHandler RequestClose;

        public event EventHandler TemporaryHtmlFileContentChanged;
    }
}
