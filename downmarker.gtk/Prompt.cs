using System;
using Gtk;
using DownMarker.Core;

namespace downmarker.gtk
{
	public class Prompt : IPrompt
	{
		
		public Link EditLink (Link link)
		{
			throw new NotImplementedException ();
		}

		public void ShowError (string errorMessage, string title)
		{
			using (var dialog = new MessageDialog(
		        null,
			    DialogFlags.Modal,
			    MessageType.Error,
			    ButtonsType.Ok,
			    "errorMessage"))
			{
				dialog.Run();
				dialog.Destroy();
			}
		}

		public string QuestionSaveAs ()
		{
			using (var chooser = new FileChooserDialog(
			    "Save As", 
			    null,
			    FileChooserAction.Open,
				"Save", ResponseType.Accept,
			    "Cancel", ResponseType.Cancel))
			{
				int response = chooser.Run();
				chooser.Destroy();
				if (response == (int)ResponseType.Accept)
				{
				    return chooser.Filename;
				}
				else
				{
					return null;
				}
			}
		}

		public string QuestionOpenFile ()
		{
			var chooser = new FileChooserDialog(
			    	"Open File", 
			    	null,
			    	FileChooserAction.Open,
					"Open", ResponseType.Accept,
			    	"Cancel", ResponseType.Cancel);
			try
			{
				int response = chooser.Run();
				if (response == (int)ResponseType.Accept)
				{
				    return chooser.Filename;
				}
				else
				{
					return null;
				}
			}
			finally
			{
				chooser.Destroy();
			}
		}
	
		public PromptResult QuestionSaveUnsavedChanges()
		{
			return PromptResult.Yes;
		}
		
		
	}
}

