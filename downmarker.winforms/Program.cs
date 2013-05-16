using System;
using System.Threading;
using System.Windows.Forms;
using DownMarker.Core;
using System.Reflection;
using System.IO;

namespace DownMarker.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += HandleThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            WebBrowserFeatures.DisableNavigationSound();

            string exeDirectory =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string styleDirectory = Path.Combine(exeDirectory,"styles");
            string styleFile = Path.Combine(styleDirectory, "kevinburke-markdown.css");
            string styleUrl = new Uri(styleFile).ToString();

            // construction phase
            var fileSystem = new FileSystem();
            var viewModel = new MainViewModel(
               new MarkdownSharpAdapter(
                   new WindowsFormsSynchronizationContext(),
                   styleUrl),
               new Prompt(fileSystem),
               fileSystem,
               new UriHandler(),
               new PersistentState(new CurrentUserRegistry()));
            var form = new MainView(viewModel);

            // process command line arguments
            bool start = true;
            if (args.Length > 0)
            {
                start = viewModel.Open(args[0]);
            }

            // start application loop
            if (start)
            {
                Application.Run(form);
            }
        }

        private static void HandleThreadException(object sender, ThreadExceptionEventArgs args)
        {
           MessageBox.Show(
              args.Exception.Message + "\n\r" + args.Exception.StackTrace,
              "Unhandled Error",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error);
        }
    }
}
