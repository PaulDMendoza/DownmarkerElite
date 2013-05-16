using System;
using System.Diagnostics;

namespace DownMarker.Core
{
    public interface IUriHandler
    {
        void OpenAbsoluteUri(Uri uri);
    }

    public class UriHandler : IUriHandler
    {
        public void OpenAbsoluteUri(Uri uri)
        {
            Process.Start(uri.ToString());
        }
    }
}
