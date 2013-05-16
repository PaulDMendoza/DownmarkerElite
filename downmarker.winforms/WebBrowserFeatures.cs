using System;
using System.Runtime.InteropServices;

namespace DownMarker.WinForms
{
    public static class WebBrowserFeatures
    {
        /// <summary>
        /// Disables the annoying click sound ("Windows Navigation Start.wav")
        /// that is normally heard whenever the content of a WinForms WebBrowser
        /// control is changed.
        /// </summary>
        /// <returns>
        /// true if the click sound was disabled, false if the platform does not
        /// support disabling the sound (or it didn't have sound in the first place).
        /// </returns>
        // http://stackoverflow.com/questions/393166/how-to-disable-click-sound-in-webbrowser-control
        public static bool DisableNavigationSound()
        {
            try
            {
                int hresult = CoInternetSetFeatureEnabled(
                    FEATURE_DISABLE_NAVIGATION_SOUNDS,
                    SET_FEATURE_ON_PROCESS,
                    true);
                return (hresult == 0); // false if feature unknown (pre-IE7)
            }
            // DLL does not exist on non-windows platforms (and maybe old windows versions?)
            catch (DllNotFoundException)
            {
                return false;
            }
            // routine introduced in Microsoft Internet Explorer 6 for Windows XP Service Pack 2 (SP2)
            catch (EntryPointNotFoundException)
            {
                return false;
            }
        }

        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        private static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);
    }
}
