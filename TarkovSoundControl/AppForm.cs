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
        public bool startMinimized = false;
        private const string APPLICATION_NAME = "TarkovSoundControl";
        private const string TARKOV_PROCESS_NAME = "EscapeFromTarkov";
        private const string TARKOV_VOLUME_REG_KEY = "TarkovOnBackgroundVolume";
        private const string TARKOV_FOREGROUND_VOLUME_REG_KEY = "TarkovOnForegroundVolume";
        private const string TARKOV_VOLUME_REG_SUBKEY = @"SOFTWARE\TarkovSoundControlSettings";

        public string TEXT_GAME_LAUNCHED = "EFT is launched";
        public string TEXT_GAME_NOT_LAUNCHED = "EFT is not launched";

        private static Timer myTimer = new Timer();
        private readonly RegistryKey RegCurrentUser = Registry.CurrentUser;
        private float TarkovBackgroundVolume = 50f;
        private float TarkovDefaultVolume = 100f;
        private int? TarkovProcessId;

        public AppForm()
        {
            InitializeComponent();

            RegistryKey rk = RegCurrentUser.CreateSubKey(TARKOV_VOLUME_REG_SUBKEY);
            rk.Close();
        }

        public static Timer MyTimer { get => myTimer; set => myTimer = value; }

        protected override void OnClosing(CancelEventArgs e)
        {
            RestoreDefaultVolume();
            notifyIcon.Visible = false;

            base.OnClosing(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private void CheckBox1_Click(object sender, System.EventArgs e)
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
                    autorunReg.DeleteValue(APPLICATION_NAME);

                autorunReg.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (startMinimized)
            {
                WindowState = FormWindowState.Minimized;
                Visible = false;
                ShowInTaskbar = false;
                Hide();

                notifyIcon.Visible = true;
            }

            notifyIcon.MouseDoubleClick += NotifyIcon_DoubleClick;

            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Open", null, TrayMenuOpen_Click);
            notifyIcon.ContextMenuStrip.Items.Add("Close", null, TrayMenuClose_Click);

            trackBarBackegroundVolume.ValueChanged += new EventHandler(TrackBar_ValueChanged);

            RegistryKey autorunReg = RegCurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (autorunReg.GetValue(APPLICATION_NAME) != null)
                checkBox1.Checked = true;

            autorunReg.Close();

            checkBox1.Click += CheckBox1_Click;

            if (RegCurrentUser.OpenSubKey(TARKOV_VOLUME_REG_SUBKEY) != null)
            {
                String savedVolume = GetRegVolume();

                labelBackgroundVolume.Text = savedVolume;
                trackBarBackegroundVolume.Value = Int32.Parse(savedVolume);
                TarkovBackgroundVolume = Int32.Parse(savedVolume);
            }

            MyTimer.Tick += new EventHandler(TimerEventProcessor);
            MyTimer.Interval = 300;
            MyTimer.Start();
        }

        private string GetRegVolume()
        {
            object savedVolume = RegCurrentUser
                    .OpenSubKey(TARKOV_VOLUME_REG_SUBKEY)
                    .GetValue(TARKOV_VOLUME_REG_KEY);

            if (savedVolume == null)
                return TarkovBackgroundVolume.ToString();

            return savedVolume.ToString();
        }

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

        private void NotifyIcon_DoubleClick(object Sender, MouseEventArgs e)
        {
            OpenAppWindow();
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

        private void RestoreDefaultVolume()
        {
            if (TarkovProcessId != null)
                WinMixesManager.VolumeMixer.SetApplicationVolume((int)TarkovProcessId, (float)TarkovDefaultVolume);
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            Process[] pname = Process.GetProcessesByName(TARKOV_PROCESS_NAME);
            if (pname.Length == 0)
            {
                TarkovProcessId = null;

                SetTextGameNotLaunched();
            }
            else
            {
                if (TarkovProcessId == null)
                {
                    TarkovProcessId = pname[0].Id;
                }

                SetTextGameLaunched();

                /*float? vol = WinMixesManager.VolumeMixer.GetApplicationVolume((int)TarkovProcessId);
                TarkovDefaultVolume = (float)(vol != null ? vol : 100f);*/

                if (IsTarkovForeground((int)TarkovProcessId))
                {
                    WinMixesManager.VolumeMixer.SetApplicationVolume((int)TarkovProcessId, (float)TarkovDefaultVolume);
                }
                else
                {
                    WinMixesManager.VolumeMixer.SetApplicationVolume((int)TarkovProcessId, TarkovBackgroundVolume);
                }
            }
        }

        private void SetTextGameLaunched()
        {
            label1.Text = TEXT_GAME_LAUNCHED;
            label1.ForeColor = Color.DarkGreen;
        }

        private void SetTextGameNotLaunched()
        {
            label1.Text = TEXT_GAME_NOT_LAUNCHED;
            label1.ForeColor = Color.DarkRed;
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            TarkovBackgroundVolume = (float)trackBarBackegroundVolume.Value;

            RegistryKey rkVolumeSubkey = RegCurrentUser.OpenSubKey(TARKOV_VOLUME_REG_SUBKEY, true);
            if (rkVolumeSubkey != null)
            {
                rkVolumeSubkey.SetValue(TARKOV_VOLUME_REG_KEY, trackBarBackegroundVolume.Value, RegistryValueKind.DWord);
                rkVolumeSubkey.Close();
                RegCurrentUser.Close();
            }

            labelBackgroundVolume.Text = trackBarBackegroundVolume.Value.ToString();
        }

        private void TrayMenuClose_Click(object sender, EventArgs e)
        {
            RestoreDefaultVolume();
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void TrayMenuOpen_Click(object sender, EventArgs e)
        {
            OpenAppWindow();
        }
    }
}