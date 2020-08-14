using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;

namespace Systrayezer
{
    partial class ExternalWindowManager
    {
        public static void CloseWindow(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        public static Icon GetAppIcon(IntPtr hwnd)
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

        public static void hideWindows(Collection<IntPtr> windowHandlers)
        {
            foreach (IntPtr windowHandle in windowHandlers)
            {
                int style = GetWindowLong(windowHandle, GWL_EXSTYLE);

                style |= WS_EX_APPWINDOW;
                SetWindowLong(windowHandle, GWL_EXSTYLE, style);
                ShowWindow(windowHandle, SW_SHOW);
                ShowWindow(windowHandle, SW_HIDE);
            }
        }

        public static void showWindows(Collection<IntPtr> windowHandlers)
        {
            foreach(IntPtr windowHandle in windowHandlers)
            {
                int style = GetWindowLong(windowHandle, GWL_EXSTYLE);

                style &= ~(WS_EX_APPWINDOW);

                SetWindowLong(windowHandle, GWL_EXSTYLE, style);
                ShowWindow(windowHandle, SW_HIDE);
                ShowWindow(windowHandle, SW_SHOW);
                SetForegroundWindow(windowHandle);
            }
        }

        public static void setWindowMostTop(IntPtr windowHandle)
        {
            SetWindowPos(windowHandle, HWND_TOP, 0, 0, 0, 0, SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOMOVE);
        }

        public static Collection<IntPtr> GetAllWindowsFromProcessName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            Collection<IntPtr> windowsHandle = new Collection<IntPtr>();

            foreach (Process p in processes)
            {
                IntPtr windowHandle = p.MainWindowHandle;
                windowsHandle.Add(windowHandle);
            }

            return windowsHandle;
        }

        public static Collection<IntPtr> GetAllWindowByCaption(string caption)
        {
            IntPtr windowHandle = FindWindow(null, caption);
            Collection<IntPtr> windowHandlers = new Collection<IntPtr>();
            windowHandlers.Add(windowHandle);
            return windowHandlers;
        }

        public static void GetAllWindowCaptions()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle) && IsWindowVisible(process.MainWindowHandle))
                {
                    Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                }
            }
        }

    }
}
