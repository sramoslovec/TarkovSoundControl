using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TarkovSoundControl
{
    public partial class AppForm : Form
    {
        static Timer myTimer = new Timer();

        private const String TARKOV_VOLUME_REG_SUBKEY = @"SOFTWARE\TarkovSoundControlSettings";
        private const String TARKOV_VOLUME_REG_KEY = "TarkovOnBackgroundVolume";
        private const String TARKOV_PROCESS_NAME = "EscapeFromTarkov";
        private const String APPLICATION_NAME = "TarkovSoundControl";

        private float? TarkovDefaultVolume = null;
        private float TarkovBackgroundVolume = 50f;

        private Process TarkovProcess;
        readonly RegistryKey RegCurrentUser = Registry.CurrentUser;

        public static Timer MyTimer { get => myTimer; set => myTimer = value; }

        public bool startMinimized = false;

        public AppForm()
        {
            InitializeComponent();

            RegistryKey rk = RegCurrentUser.CreateSubKey(TARKOV_VOLUME_REG_SUBKEY);
            rk.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (startMinimized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
                this.ShowInTaskbar = false;
                Hide();
                notifyIcon.Visible = true;
            }

            string startPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs) + @"\TarkovTool\TarkovSoundControl.appref-ms";

            notifyIcon.MouseDoubleClick += NotifyIcon_DoubleClick;

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Open", null, TrayMenuOpen_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Close", null, TrayMenuClose_Click);

            trackBar1.ValueChanged += new EventHandler(TrackBar_ValueChanged);

            RegistryKey autorunReg = RegCurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (autorunReg.GetValue(APPLICATION_NAME) != null)
            {
                checkBox1.Checked = true;
            }
            autorunReg.Close();

            checkBox1.Click += checkBox1_Click;

            if (RegCurrentUser.OpenSubKey(TARKOV_VOLUME_REG_SUBKEY) != null) {
                String savedVolume = RegCurrentUser
                    .OpenSubKey(TARKOV_VOLUME_REG_SUBKEY)
                    .GetValue(TARKOV_VOLUME_REG_KEY)
                    .ToString();

                label3.Text = savedVolume;
                trackBar1.Value = Int32.Parse(savedVolume);
                TarkovBackgroundVolume = Int32.Parse(savedVolume);
            }

            MyTimer.Tick += new EventHandler(TimerEventProcessor);
            MyTimer.Interval = 300;
            MyTimer.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            Process[] pname = Process.GetProcessesByName(TARKOV_PROCESS_NAME);
            if (pname.Length == 0) {
                if (TarkovProcess != null) 
                { 
                    TarkovProcess = null;
                }
                
                label1.Text = "EFT is not launched";
                label1.ForeColor = Color.DarkRed;
            }
            else {
                TarkovProcess = pname[0];

                label1.Text = "EFT is launched";
                label1.ForeColor = Color.DarkGreen;

                if (TarkovDefaultVolume == null)
                {
                    TarkovDefaultVolume = WinMixesManager.VolumeMixer.GetApplicationVolume(TarkovProcess.Id);
                    if (TarkovDefaultVolume == null)
                        return;
                }
                else {
                    if (IsTarkovForeground(TarkovProcess.Id))
                    {
                        WinMixesManager.VolumeMixer.SetApplicationVolume(TarkovProcess.Id, (float)TarkovDefaultVolume);
                    }
                    else
                    {
                        WinMixesManager.VolumeMixer.SetApplicationVolume(TarkovProcess.Id, TarkovBackgroundVolume);
                    }
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void checkBox1_Click(object sender, System.EventArgs e)
        { 
            if (checkBox1.Checked)
            {

                String AutostartRegString = "\"" + Program.Extensions.GetExecutablePath() + "\" --minimized";
                
                RegistryKey autorunReg = RegCurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                autorunReg.SetValue(APPLICATION_NAME, AutostartRegString);
                autorunReg.Close();
            }
            else
            {
                RegistryKey autorunReg = RegCurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (autorunReg.GetValue(APPLICATION_NAME) != null)
                {
                    autorunReg.DeleteValue(APPLICATION_NAME);
                }
                autorunReg.Close();
            }
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            TarkovBackgroundVolume = (float)trackBar1.Value;

            RegistryKey rkVolumeSubkey = RegCurrentUser.OpenSubKey(TARKOV_VOLUME_REG_SUBKEY, true);
            if (rkVolumeSubkey != null) {
                rkVolumeSubkey.SetValue(TARKOV_VOLUME_REG_KEY, trackBar1.Value, RegistryValueKind.DWord);
                rkVolumeSubkey.Close();
                RegCurrentUser.Close();
            }

            label3.Text = trackBar1.Value.ToString();
        }

        private void NotifyIcon_DoubleClick(object Sender, MouseEventArgs e)
        {
            OpenAppWindow();
        }
        
        void TrayMenuOpen_Click(object sender, EventArgs e)
        {
            OpenAppWindow();
        }

        void TrayMenuClose_Click(object sender, EventArgs e)
        {
            RestoreDefaultVolume();
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void OpenAppWindow()
        {
            try
            {
                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private bool IsTarkovForeground(int tarkovProcessId)
        {
            IntPtr hwnd = GetForegroundWindow();

            if (hwnd == null)
                return false;

            GetWindowThreadProcessId(hwnd, out uint pid);

            foreach (Process p in Process.GetProcesses())
            {
                if (pid == tarkovProcessId)
                    return true;
            }

            return false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            RestoreDefaultVolume();
            notifyIcon.Visible = false;
            base.OnClosing(e);
        }

        private void RestoreDefaultVolume()
        {
            if (TarkovProcess != null) {
                WinMixesManager.VolumeMixer.SetApplicationVolume(TarkovProcess.Id, (float)TarkovDefaultVolume);
            }
        }
    }
}