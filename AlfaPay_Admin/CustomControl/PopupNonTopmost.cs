using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace AlfaPay_Admin.CustomControl
{
    public class PopupNonTopmost : Popup
    {
        protected override void OnOpened(EventArgs e)
        {
            var hwnd = (PresentationSource.FromVisual(Child) as HwndSource).Handle;

            if (GetWindowRect(hwnd, out Rect rect))
            {
                SetWindowPos(hwnd, -2, rect.Left, rect.Top, (int) Width, (int) Height, 0);
            }
        }

        #region P/Invoke imports & definitions

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct Rect
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32", EntryPoint = "SetWindowPos")]
        private static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy,
            int wFlags);

        #endregion
    }
}