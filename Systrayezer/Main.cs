using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Systrayezer
{
    public partial class Main : Form
    {
        private KeyboardHook hook = new KeyboardHook();
        public Main()
        {
            InitializeComponent();
            new UserConfig();

            Collection<Config.Binding> bindings = UserConfig.config.bindings;

            for (int idxBinding = 0; idxBinding < bindings.Count; idxBinding++)
            {
                Config.Binding binding = bindings.ElementAt(idxBinding);
                if (binding.autostart)
                {
                    applyBinding(binding);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_Dispose(bool disposing)
        {
            this.Visible = false;
        }
        
        private void Tray_doubleclick(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void BtnApplyHotkeys_Click(object sender, EventArgs e)
        {
            Collection<Config.Binding> bindings = UserConfig.config.bindings;

            for(int idxBinding = 0; idxBinding < bindings.Count; idxBinding++)
            {
                Config.Binding binding = bindings.ElementAt(idxBinding);
                if(binding.eventKeyId == 0)
                {
                    applyBinding(binding);
                }
            }
        }

        void applyBinding(Config.Binding binding)
        {
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>((object eventSender, KeyPressedEventArgs ev) =>
            {
                if (!binding.hidden)
                {
                    ExternalWindowManager.hideWindows(binding.windowHandlers);
                }
                else
                {
                    ExternalWindowManager.showWindows(binding.windowHandlers);
                }
                binding.hidden = !binding.hidden;
            });

            binding.eventKeyId = hook.RegisterHotKey(binding.GetCombinationOfModifierKeys(), binding.key);

            if (binding.starthide)
            {
                ExternalWindowManager.hideWindows(binding.windowHandlers);
            }

            if (binding.systray)
            {
                uint WM_GETICON = 0x007f;
                IntPtr ICON_SMALL2 = new IntPtr(2);
                // TODO: extract the app icon to make a systray icon with restore/hidesystray/close options.
                NotifyIcon trayIcon = new NotifyIcon(this.components);
                //Process[] processes = Process.GetProcessesByName(binding.app);
                //Icon ico = Icon.ExtractAssociatedIcon(processes[0].MainModule.FileName);
                // Get icon from app
                IntPtr hIcon = default(IntPtr);

                hIcon = SendMessage(binding.windowHandlers.ElementAt(0), WM_GETICON, ICON_SMALL2, IntPtr.Zero);
                Icon ico = Icon.FromHandle(hIcon);
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
                trayIcon.Icon = ico;

                trayIcon.Text = binding.app;
                trayIcon.Visible = true;
                trayIcon.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    // check right click to open context
                });
            }
        }






        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        static extern uint GetClassLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        static extern IntPtr GetClassLong64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 64 bit version maybe loses significant 64-bit specific information
        /// </summary>
        static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return new IntPtr((long)GetClassLong32(hWnd, nIndex));
            else
                return GetClassLong64(hWnd, nIndex);
        }


        public static Image GetSmallWindowIcon(IntPtr hWnd)
        {
            uint WM_GETICON = 0x007f;
            IntPtr ICON_SMALL2 = new IntPtr(2);
            IntPtr IDI_APPLICATION = new IntPtr(0x7F00);
            int GCL_HICON = -14;
            try
            {
                IntPtr hIcon = default(IntPtr);

                hIcon = SendMessage(hWnd, WM_GETICON, ICON_SMALL2, IntPtr.Zero);

                if (hIcon == IntPtr.Zero)
                    hIcon = GetClassLongPtr(hWnd, GCL_HICON);

                if (hIcon == IntPtr.Zero)
                    hIcon = LoadIcon(IntPtr.Zero, (IntPtr)0x7F00/*IDI_APPLICATION*/);

                if (hIcon != IntPtr.Zero)
                    return new Bitmap(Icon.FromHandle(hIcon).ToBitmap(), 16, 16);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
