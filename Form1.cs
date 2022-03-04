using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainWindow : Form
    {
        
        libraryWindow libWindow;
        fb2Book Fb2Book;
        public Book currentBook;
        public Library currentLib;
        public BookMarks currentBookMarks;
        int nPos;
        public MainWindow()
        {
            InitializeComponent();
            topMenuStrip.MouseDown += new MouseEventHandler((o, e) =>
            {
                base.Capture = false;
                Message message = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref message);
            });
            Program.mw = this;
            libWindow = new libraryWindow(this);     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            libraryWindow libWindow = new libraryWindow();
            libWindow.TopMost = true;
            libWindow.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            close.BackColor = Color.IndianRed;
        }


        private void topMenuStrip_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.BackColor = Color.Gray;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (fb2Content.SelectedItem != null)
            {
                fb2Reader.Navigate(Fb2Book.HTMLPath + "#" + Fb2Book.content[fb2Content.SelectedIndex].Item2);
            }
        }


        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            if(richTextBox1.Visible == true) {
                this.nPos = GetScrollPos(richTextBox1.Handle, (int)Constants.ScrollBarType.SbVert);
                this.nPos <<= 16;
            }

        }

        private void AddBookMark_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (Path.GetExtension(currentBook.filePath) == ".txt" || Path.GetExtension(currentBook.filePath) == ".rtf")
                {
                    currentBookMarks.addBookMark(new BookMark(textBox1.Text, nPos));
                    currentBookMarks.writeBookMarks();
                }
                else if(Path.GetExtension(currentBook.filePath) == ".fb2")
                {
                    int scrollPosition = fb2Reader.Document.GetElementsByTagName("HTML")[0].ScrollTop;
                    currentBookMarks.addBookMark(new BookMark(textBox1.Text, scrollPosition));
                    currentBookMarks.writeBookMarks();
                }
            }
        }

        public void loadBook(string filename)
        {
            if (Path.GetExtension(filename) == ".txt")
            {
                fb2Content.Visible = false;
                fb2Reader.Visible = false;
                richTextBox1.Visible = true;
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.PlainText);
            }
            else if ((Path.GetExtension(filename) == ".rtf"))
            {
                fb2Content.Visible = false;
                fb2Reader.Visible = false;
                richTextBox1.Visible = true;
                richTextBox1.ReadOnly = false;
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.RichText);
                richTextBox1.ReadOnly = true;
            }
            else if ((Path.GetExtension(filename) == ".fb2"))
            {
                Fb2Book = new fb2Book(filename, 0);
                Fb2Book.open();
                richTextBox1.Visible = false;
                fb2Reader.Visible = true;
                fb2Reader.Navigate(new Uri("file:///" + Fb2Book.HTMLPath));
                fb2Content.DataSource = Fb2Book.content;
                fb2Content.DisplayMember = "Item1";
                fb2Content.ValueMember = "Item2";
                fb2Content.Visible = true;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentLib.writeLib();
        }

        private void libToolStripMenuItem_Click(object sender, EventArgs e)
        {
            libWindow.Show();
        }
        
        public void ScrollRichTextBox(int progress)
        {
            uint wParam = (uint)Constants.ScrollBarCommands.SB_THUMBPOSITION | (uint)progress;
            SendMessage(richTextBox1.Handle, (int)Constants.Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        private void DeleteBookMark_Click(object sender, EventArgs e)
        {
            if(BookMarks1.SelectedItem != null)
            {
                currentBookMarks.delBookMark(BookMarks1.SelectedIndex);
                currentBookMarks.writeBookMarks();
            }
        }

        private void BookMarks1_DoubleClick(object sender, EventArgs e)
        {
            if(BookMarks1.SelectedIndex != null)
            {
                if(richTextBox1.Visible == true)
                    ScrollRichTextBox(currentBookMarks.bookMarks[BookMarks1.SelectedIndex].Pos);
                if(fb2Reader.Visible == true)
                {
                    fb2Reader.Document.Window.ScrollTo(0, currentBookMarks.bookMarks[BookMarks1.SelectedIndex].Pos);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Visible)
                ScrollRichTextBox(currentBook.Progress);
            if (fb2Reader.Visible)
            {
                fb2Reader.Document.Window.ScrollTo(0, currentBook.Progress);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Visible == true)
                currentBook.Progress = nPos;
            if (fb2Reader.Visible == true)
            {
                int scrollPosition = fb2Reader.Document.GetElementsByTagName("HTML")[0].ScrollTop;
                currentBook.Progress = scrollPosition;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
