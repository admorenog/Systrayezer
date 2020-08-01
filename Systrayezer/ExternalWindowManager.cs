using System;
using System.Runtime.InteropServices;

namespace Systrayezer
{
    class ExternalWindowManager
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /**
         * We can clean the unused vars when we reach the perfect functionality.
         */
        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_SHOWMINNOACTIVE = 7;
        private const int SW_SHOWNA = 8;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;
        private const int SW_FORCEMINIMIZE = 11;


        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_APPWINDOW = 0x00040000;

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /**
         * We should def all this winapi functions to detect if the
         * system is 32 or 64 bits and use it automatically.
         */
        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public static void hideWindow(string caption)
        {
            IntPtr handle = FindWindow(null, caption);

            int style = GetWindowLong(handle, GWL_EXSTYLE);

            style |= WS_EX_APPWINDOW;
            SetWindowLong(handle, GWL_EXSTYLE, style);
            ShowWindow(handle, SW_SHOW);
            ShowWindow(handle, SW_HIDE);
        }

        public static void showWindow(string caption)
        {

            IntPtr handle = FindWindow(null, caption);

            int style = GetWindowLong(handle, GWL_EXSTYLE);

            style &= ~(WS_EX_APPWINDOW);

            SetWindowLong(handle, GWL_EXSTYLE, style);
            ShowWindow(handle, SW_HIDE);
            ShowWindow(handle, SW_SHOW);
        }
    }
}
