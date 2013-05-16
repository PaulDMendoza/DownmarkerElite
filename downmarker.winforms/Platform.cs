using System;

namespace DownMarker.WinForms
{
	public class Platform
	{
		/// <summary>
		/// Returns <c>true</c> when we're running on Mono.
		/// </summary>
		public static bool IsMono()
		{
		    return Type.GetType("Mono.Runtime") != null;	
		}
	}
}
