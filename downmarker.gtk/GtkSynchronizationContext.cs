using System;
using System.Threading;

namespace downmarker.gtk
{
	public class GtkSynchronizationContext : SynchronizationContext
	{
		public override void Send (SendOrPostCallback d, object state)
		{
			// same as post, but wait until processed
			var signal = new ManualResetEvent(false);
			Post(
		        stateObject => 
			    {
				    d(stateObject);
				    signal.Set();
			    },
			    state);
			signal.WaitOne();
		}

		public override void Post (SendOrPostCallback d, object state)
		{
			Gtk.Application.Invoke(delegate {d(state);});
		}

	}
}

