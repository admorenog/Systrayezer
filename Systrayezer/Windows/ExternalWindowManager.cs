using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Systrayezer.Windows
{
    partial class ExternalWindowManager
    {
        static IntPtr thumb;
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

        public static void GetAllWindowCaptions(IntPtr handle, PictureBox picturebox)
        {
            Process[] processlist = Process.GetProcesses();
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process process in processlist)
            {

                /**
                 The windows store apps are visible but also cloaked, so we need to check it too
                 */
                DwmGetWindowAttribute(
                    process.MainWindowHandle,
                    DWMWINDOWATTRIBUTE.Cloaked,
                    out bool isCloaked,
                    Marshal.SizeOf(typeof(bool))
                );
                if (!string.IsNullOrEmpty(process.MainWindowTitle)
                    && IsWindowVisible(process.MainWindowHandle) && !isCloaked)
                {
                    // https://github.com/LorenzCK/OnTopReplica/tree/master/src/OnTopReplica
                    // int i = DwmRegisterThumbnail(this.Handle, w.Handle, out thumb);
                    // DwmUnregisterThumbnail(thumb);

                    if (thumb != IntPtr.Zero)
                    {
                        DwmUnregisterThumbnail(thumb);
                    }

                    DwmRegisterThumbnail(handle, process.MainWindowHandle, out thumb);

                    PSIZE size;
                    DwmQueryThumbnailSourceSize(thumb, out size);
                    int DWM_TNP_VISIBLE = 0x8;
                    int DWM_TNP_OPACITY = 0x4;
                    int DWM_TNP_RECTDESTINATION = 0x1;
                    DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();
                    props.dwFlags = DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY;

                    props.fVisible = true;
                    props.opacity = byte.MaxValue;

                    props.rcDestination = new Rect(picturebox.Left, picturebox.Top, picturebox.Right, picturebox.Bottom);
                    if (size.x < picturebox.Width)
                        props.rcDestination.Right = props.rcDestination.Left + size.x;
                    if (size.y < picturebox.Height)
                        props.rcDestination.Bottom = props.rcDestination.Top + size.y;

                    DwmUpdateThumbnailProperties(thumb, ref props);

                    Console.WriteLine(
                        "Process: {0} ID: {1} Window title: {2} Thumb: {3}",
                        process.ProcessName, process.Id, process.MainWindowTitle, thumb
                    );
                    break;
                }
            }
        }

        public static Window[] GetAllWindows()
        {
            Process[] processlist = Process.GetProcesses();

            Collection<Window> windows = new Collection<Window>();
            foreach (Process process in processlist)
            {

                /**
                 The windows store apps are visible but also cloaked, so we need to check it too
                 */
                DwmGetWindowAttribute(
                    process.MainWindowHandle,
                    DWMWINDOWATTRIBUTE.Cloaked,
                    out bool isCloaked,
                    Marshal.SizeOf(typeof(bool))
                );
                if (!string.IsNullOrEmpty(process.MainWindowTitle)
                    && IsWindowVisible(process.MainWindowHandle) && !isCloaked)
                {
                    Window window = new Window
                    {
                        Process = process,
                        Handle = process.MainWindowHandle,
                        Title = process.MainWindowTitle
                    };
                    windows.Add(window);
                }
            }

            return windows.ToArray();
        }

        public static void SyncThumbNailsTo(IntPtr srcHndl, IntPtr destHndl, PictureBox picturebox)
        {

            if (thumb != IntPtr.Zero)
            {
                //DwmUnregisterThumbnail(thumb);
            }

            DwmRegisterThumbnail(srcHndl, destHndl, out thumb);

            PSIZE size;
            DwmQueryThumbnailSourceSize(thumb, out size);
            int DWM_TNP_VISIBLE = 0x8;
            int DWM_TNP_OPACITY = 0x4;
            int DWM_TNP_RECTDESTINATION = 0x1;
            DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES
            {
                dwFlags = DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY,

                fVisible = true,
                opacity = byte.MaxValue,

                rcDestination = new Rect(picturebox.Left, picturebox.Top, picturebox.Right, picturebox.Bottom)
            };
            if (size.x < picturebox.Width)
                props.rcDestination.Right = props.rcDestination.Left + size.x;
            if (size.y < picturebox.Height)
                props.rcDestination.Bottom = props.rcDestination.Top + size.y;

            DwmUpdateThumbnailProperties(thumb, ref props);
        }
    }
}
