using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class CustomRichTextBox : RichTextBox
    {
        public CustomRichTextBox()
        {
            HideCaret(this.Handle);
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool HideCaret(IntPtr hwnd);

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            HideCaret(this.Handle);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            HideCaret(this.Handle);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            HideCaret(this.Handle);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            HideCaret(this.Handle);
        }
    }
}
