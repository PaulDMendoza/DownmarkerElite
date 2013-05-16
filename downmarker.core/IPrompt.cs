namespace DownMarker.Core
{
    public interface IPrompt
    {
        /// <summary>
        /// Asks the user whether to save unsaved changes, to which he can
        /// answer yes, no or cancel.
        /// </summary>
        PromptResult QuestionSaveUnsavedChanges();

        /// <summary>
        /// Shows the user a link to edit.
        /// </summary>
        /// <returns>
        /// An edited copy of the original link,
        /// or <c>null</c> if the user cancelled.
        /// </returns>
        Link EditLink(Link link, string currentFile, bool image);

        /// <summary>
        /// Shows the user an error message.
        /// </summary>
        void ShowError(string errorMessage, string title);

        /// <summary>
        /// Prompts the user to chose a file name for the markdown document to save.
        /// </summary>
        /// <returns>
        /// The filename to use for saving, or <c>null</c> if the user cancelled.
        /// </returns>
        string QuestionSaveAs(string initialDirectory);

        /// <summary>
        /// Prompts the user to chose a file name for saving an HTML document.
        /// </summary>
        /// <returns>
        /// The filename to use for saving, or <c>null</c> if the user cancelled.
        /// </returns>
        string QuestionSaveHtml(string initialDirectory);


        /// <summary>
        /// Prompts the user to chose a markdown file to open.
        /// </summary>
        /// <returns>
        /// The file to open, or <c>null</c> if the user cancelled.
        /// </returns>
        string QuestionOpenFile(string initialDirectory);

        /// <summary>
        /// Prompts the user to chose an image file.
        /// </summary>
        /// <returns>
        /// The image to open, or <c>null</c> if the user cancelled.
        /// </returns>
        string QuestionOpenImageFile(string initialDirectory);
    }
}
