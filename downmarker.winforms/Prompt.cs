using System;
using System.IO;
using System.Windows.Forms;
using DownMarker.Core;

namespace DownMarker.WinForms
{
    public class Prompt : IPrompt
    {
        private readonly IFileSystem fileSystem;

        public Prompt(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public PromptResult QuestionSaveUnsavedChanges()
        {
            return GetPromptResult(MessageBox.Show(
               "Save changes?",
               "There are unsaved changes",
               MessageBoxButtons.YesNoCancel,
               MessageBoxIcon.Question));
        }

        private static PromptResult GetPromptResult(DialogResult dialogResult)
        {
            switch (dialogResult)
            {
                case DialogResult.OK:
                    return PromptResult.OK;
                case DialogResult.Cancel:
                    return PromptResult.Cancel;
                case DialogResult.Yes:
                    return PromptResult.Yes;
                case DialogResult.No:
                    return PromptResult.No;
                default:
                    throw new ArgumentException("Unsupported DialogResult");
            }

        }

        public void ShowError(string errorMessage, string title)
        {
            MessageBox.Show(errorMessage, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public string QuestionSaveAs(string initialDirectory)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".md";
                dialog.DereferenceLinks = true;
                dialog.Filter = "Markdown files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.InitialDirectory = initialDirectory;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string QuestionSaveHtml(string initialDirectory)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".html";
                dialog.DereferenceLinks = true;
                dialog.Filter = "All files (*.*)|*.*|HTML files (*.html)|*.html|HTM files (*.htm)|*.htm";
                dialog.InitialDirectory = initialDirectory;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string QuestionOpenFile(string initialDirectory)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.AddExtension = true;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".md";
                dialog.DereferenceLinks = true;
                dialog.Filter = "Markdown files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.Multiselect = false;
                dialog.InitialDirectory = initialDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        public string QuestionOpenImageFile(string initialDirectory)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.AddExtension = true;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".jpg";
                dialog.DereferenceLinks = true;
                dialog.Filter = "Image Files|*.jpg;*.gif;*.bmp;*.png;*.jpeg|All Files (*.*)|*.*";
                dialog.Multiselect = false;
                dialog.InitialDirectory = initialDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
                else
                {
                    return null;
                }
            }
        }

        public Link EditLink(Link link, string currentFile, bool image)
        {
            var viewModel = new LinkEditorViewModel(fileSystem, this, image);
            viewModel.CurrentFile = currentFile;
            if (link != null)
            {
                viewModel.LinkDescription = link.Text;
                viewModel.LinkTarget = link.Target;
            };

            using (var form = new LinkEditorView(viewModel,image))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (viewModel.CreateTarget)
                    {
                        var uri = new Uri(viewModel.LinkTarget, UriKind.RelativeOrAbsolute);
                        if (!uri.IsAbsoluteUri)
                        {
                            uri = new Uri(new Uri(currentFile), uri);
                        }
                        string title = Path.GetFileNameWithoutExtension(uri.LocalPath);
                        title = "# " + title;
                        fileSystem.WriteTextFile(uri.LocalPath, title);
                    }


                    return new Link()
                    {
                        Target = viewModel.LinkTarget,
                        Text = viewModel.LinkDescription
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
