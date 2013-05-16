using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DownMarker.WinForms
{
   public static class FormExtensions
   {
      public static void MoveToForeground(this Form form)
      {
         if (!Platform.IsMono())
         {
            SetForegroundWindow(form.Handle);
         }
      }

      [DllImport("User32.dll")]
      private static extern int SetForegroundWindow(IntPtr hWnd);
   }
}
