using System;
using System.Collections.ObjectModel;
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
                NotifyIcon trayIcon = new NotifyIcon(components);
                
                Icon ico = GetAppIcon(binding.windowHandlers.ElementAt(0));
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
                trayIcon.Icon = ico;

                trayIcon.Text = binding.app;
                trayIcon.Visible = true;

                ContextMenu contextMenu = new ContextMenu();
                MenuItem menuItem1 = new MenuItem("Restore", (object sender, EventArgs ev) => {
                    ExternalWindowManager.showWindows(binding.windowHandlers);
                });
                MenuItem menuItem2 = new MenuItem("Hide systray", (object sender, EventArgs ev) => {
                    trayIcon.Visible = false;
                });
                MenuItem menuItem3 = new MenuItem("Close", (object sender, EventArgs ev) => {
                    foreach(IntPtr windowHandler in binding.windowHandlers)
                    {
                        CloseWindow(windowHandler);
                    }
                    trayIcon.Visible = false;
                    hook.UnRegisterHotKey(binding.eventKeyId);
                });

                // Initialize contextMenu1
                contextMenu.MenuItems.AddRange(new MenuItem[] { menuItem1, menuItem2, menuItem3 });
                trayIcon.ContextMenu = contextMenu;
                trayIcon.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    // check right click to open context
                });
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        public const int GCL_HICONSM = -34;
        public const int GCL_HICON = -14;

        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;

        public const int WM_GETICON = 0x7F;

        public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public Icon GetAppIcon(IntPtr hwnd)
        {
            IntPtr iconHandle = SendMessage(hwnd, WM_GETICON, ICON_SMALL2, 0);
            if (iconHandle == IntPtr.Zero)
                iconHandle = SendMessage(hwnd, WM_GETICON, ICON_SMALL, 0);
            if (iconHandle == IntPtr.Zero)
                iconHandle = SendMessage(hwnd, WM_GETICON, ICON_BIG, 0);
            if (iconHandle == IntPtr.Zero)
                iconHandle = GetClassLongPtr(hwnd, GCL_HICON);
            if (iconHandle == IntPtr.Zero)
                iconHandle = GetClassLongPtr(hwnd, GCL_HICONSM);

            if (iconHandle == IntPtr.Zero)
                return null;

            Icon icn = Icon.FromHandle(iconHandle);

            return icn;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((components != null))
            {
                components.Dispose();
            }
            base.Dispose();
        }
    }
}
