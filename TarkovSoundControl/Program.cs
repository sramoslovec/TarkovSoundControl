using System;
using System.Windows.Forms;

namespace TarkovSoundControl
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /*
             * TODO: prevent launching multiple app instances
             */

            foreach (String arg in args)
            {
                if (arg == "--minimized")
                {
                    Form1 form = new Form1();
                    form.startMinimized = true;
                    Application.Run(mainForm: form);
                    return;
                }
            }

            Application.Run(new Form1());
        }

        public static class Extensions
        {
            [System.Runtime.InteropServices.DllImport("kernel32.dll")]
            private static extern uint GetModuleFileName(IntPtr hModule, System.Text.StringBuilder lpFilename, int nSize);

            private static readonly int MAX_PATH = 255;

            public static string GetExecutablePath()
            {
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                {
                    var sb = new System.Text.StringBuilder(MAX_PATH);
                    GetModuleFileName(IntPtr.Zero, sb, MAX_PATH);
                    return sb.ToString();
                }
                else
                {
                    return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                }
            }
        }
    }
}