using System;
using System.Diagnostics;

namespace Systrayezer.Windows
{
    public class Window
    {
        public string Title { get; set; }
        public IntPtr Handle { get; set; }
        public Process Process { get; set; }
    }
}
