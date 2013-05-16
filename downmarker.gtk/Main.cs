using System;
using Gtk;
using DownMarker.Core;

namespace downmarker.gtk
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var markdownEngine = new MarkdownSharpAdapter(
			    new GtkSynchronizationContext());
			var viewModel = new MainFormViewModel(
			    markdownEngine,
			    new Prompt(),
			    new FileSystem(),
			    new UriHandler());
			
			Application.Init ();
			MainWindow win = new MainWindow(viewModel);
			
			win.Show();
			Application.Run ();
		}
	}
}

