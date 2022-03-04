using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class libraryWindow : Form
    {
        Library lib = new Library();
        public libraryWindow()
        {
            InitializeComponent();
            ShowData();
            Program.mw.currentLib = this.lib;
        }

        public libraryWindow(MainWindow mw)
        {
            InitializeComponent();
            ShowData();
        }

        private void ShowData()
        {
            bookLibrary.DataSource = lib.bookList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf|fb2 files (*.fb2)|*.fb2";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    lib.addBook(new Book(openFileDialog.FileName, 0));
                    lib.writeLib();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(bookLibrary.SelectedItem != null)
            {
                lib.delBook(bookLibrary.SelectedIndex);
            }
            lib.writeLib();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lib.readLib();
        }

        private void bookLibrary_DoubleClick(object sender, EventArgs e)
        {
            if(bookLibrary.SelectedItem != null)
            {
                Program.mw.loadBook(lib.bookList[bookLibrary.SelectedIndex].filePath);
                Program.mw.currentBook = lib.bookList[bookLibrary.SelectedIndex];
                Program.mw.ScrollRichTextBox(lib.bookList[bookLibrary.SelectedIndex].Progress);
                Program.mw.currentBookMarks = new BookMarks(Path.GetFileNameWithoutExtension(lib.bookList[bookLibrary.SelectedIndex].filePath));
                Program.mw.currentBookMarks.readBookMarks();
                Program.mw.BookMarks1.DataSource = Program.mw.currentBookMarks.bookMarks;
                Program.mw.BookMarks1.DisplayMember = "Name";
                Program.mw.BookMarks1.ValueMember = "Pos";

                this.Close();
            }
        }

        
    }
}
