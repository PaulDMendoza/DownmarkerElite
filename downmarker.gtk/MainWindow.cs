using System;
using System.Linq;
using DownMarker.Core;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	private MainFormViewModel viewModel;
	private PropertyEvents<MainFormViewModel> events;
	
	public MainWindow(MainFormViewModel viewModel) : base(Gtk.WindowType.Toplevel)
	{
		this.viewModel = viewModel;
		this.viewModel.RequestClose += delegate { Application.Quit(); };
		this.events = this.viewModel.CreatePropertyEvents();
		this.events[x=>x.MarkdownText] += HandleMarkdownTextChanged;
		Build();
		this.events.RaiseAll();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		this.viewModel.Exit();
		a.RetVal = false;
	}
	
	protected virtual void HandleOpenActivated (object sender, System.EventArgs e)
	{
		this.viewModel.Open();
	}
	
	private void HandleMarkdownTextChanged(object sender, EventArgs args)
	{
		textview1.Buffer.Text = this.viewModel.MarkdownText;
	}
	
	protected virtual void HandleQuitActionActivated(object sender, System.EventArgs e)
	{
		this.viewModel.Exit();
	}
	
	
}

