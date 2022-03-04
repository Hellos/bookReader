
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.topMenuStrip = new System.Windows.Forms.MenuStrip();
            this.libToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.close = new System.Windows.Forms.PictureBox();
            this.fb2Reader = new System.Windows.Forms.WebBrowser();
            this.fb2Content = new System.Windows.Forms.ListBox();
            this.BookMarks1 = new System.Windows.Forms.ListBox();
            this.AddBookMark = new System.Windows.Forms.Button();
            this.DeleteBookMark = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.GoToProgress = new System.Windows.Forms.Button();
            this.SaveProgress = new System.Windows.Forms.Button();
            this.richTextBox1 = new WindowsFormsApp1.CustomRichTextBox();
            this.topMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // topMenuStrip
            // 
            this.topMenuStrip.BackColor = System.Drawing.Color.Black;
            this.topMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.libToolStripMenuItem});
            this.topMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.topMenuStrip.Name = "topMenuStrip";
            this.topMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.topMenuStrip.Size = new System.Drawing.Size(1920, 24);
            this.topMenuStrip.TabIndex = 0;
            this.topMenuStrip.Text = "menuStrip1";
            this.topMenuStrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topMenuStrip_MouseDown);
            // 
            // libToolStripMenuItem
            // 
            this.libToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.libToolStripMenuItem.Name = "libToolStripMenuItem";
            this.libToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.libToolStripMenuItem.Text = "Библиотека";
            this.libToolStripMenuItem.Click += new System.EventHandler(this.libToolStripMenuItem_Click);
            // 
            // close
            // 
            this.close.BackColor = System.Drawing.Color.Gray;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Image = ((System.Drawing.Image)(resources.GetObject("close.Image")));
            this.close.InitialImage = ((System.Drawing.Image)(resources.GetObject("close.InitialImage")));
            this.close.Location = new System.Drawing.Point(1889, 1);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(20, 21);
            this.close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.close.TabIndex = 2;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.pictureBox1_Click);
            this.close.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.close.MouseLeave += new System.EventHandler(this.close_MouseLeave);
            // 
            // fb2Reader
            // 
            this.fb2Reader.Location = new System.Drawing.Point(504, 32);
            this.fb2Reader.MinimumSize = new System.Drawing.Size(20, 20);
            this.fb2Reader.Name = "fb2Reader";
            this.fb2Reader.Size = new System.Drawing.Size(913, 996);
            this.fb2Reader.TabIndex = 3;
            this.fb2Reader.Visible = false;
            // 
            // fb2Content
            // 
            this.fb2Content.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fb2Content.FormattingEnabled = true;
            this.fb2Content.ItemHeight = 19;
            this.fb2Content.Location = new System.Drawing.Point(1555, 32);
            this.fb2Content.Name = "fb2Content";
            this.fb2Content.Size = new System.Drawing.Size(178, 992);
            this.fb2Content.TabIndex = 4;
            this.fb2Content.Visible = false;
            this.fb2Content.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // BookMarks1
            // 
            this.BookMarks1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BookMarks1.FormattingEnabled = true;
            this.BookMarks1.ItemHeight = 19;
            this.BookMarks1.Location = new System.Drawing.Point(30, 32);
            this.BookMarks1.Name = "BookMarks1";
            this.BookMarks1.Size = new System.Drawing.Size(178, 992);
            this.BookMarks1.TabIndex = 5;
            this.BookMarks1.DoubleClick += new System.EventHandler(this.BookMarks1_DoubleClick);
            // 
            // AddBookMark
            // 
            this.AddBookMark.Location = new System.Drawing.Point(314, 534);
            this.AddBookMark.Name = "AddBookMark";
            this.AddBookMark.Size = new System.Drawing.Size(75, 23);
            this.AddBookMark.TabIndex = 6;
            this.AddBookMark.Text = "Добавить";
            this.AddBookMark.UseVisualStyleBackColor = true;
            this.AddBookMark.Click += new System.EventHandler(this.AddBookMark_Click);
            // 
            // DeleteBookMark
            // 
            this.DeleteBookMark.Location = new System.Drawing.Point(314, 563);
            this.DeleteBookMark.Name = "DeleteBookMark";
            this.DeleteBookMark.Size = new System.Drawing.Size(75, 23);
            this.DeleteBookMark.TabIndex = 7;
            this.DeleteBookMark.Text = "Удалить";
            this.DeleteBookMark.UseVisualStyleBackColor = true;
            this.DeleteBookMark.Click += new System.EventHandler(this.DeleteBookMark_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(301, 497);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 8;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // GoToProgress
            // 
            this.GoToProgress.Location = new System.Drawing.Point(301, 307);
            this.GoToProgress.Name = "GoToProgress";
            this.GoToProgress.Size = new System.Drawing.Size(100, 74);
            this.GoToProgress.TabIndex = 9;
            this.GoToProgress.Text = "Перейти к прогрессу";
            this.GoToProgress.UseVisualStyleBackColor = true;
            this.GoToProgress.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveProgress
            // 
            this.SaveProgress.Location = new System.Drawing.Point(301, 387);
            this.SaveProgress.Name = "SaveProgress";
            this.SaveProgress.Size = new System.Drawing.Size(100, 74);
            this.SaveProgress.TabIndex = 10;
            this.SaveProgress.Text = "Сохранить прогресс";
            this.SaveProgress.UseVisualStyleBackColor = true;
            this.SaveProgress.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(504, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(913, 996);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.VScroll += new System.EventHandler(this.richTextBox1_VScroll);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1040);
            this.Controls.Add(this.SaveProgress);
            this.Controls.Add(this.GoToProgress);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DeleteBookMark);
            this.Controls.Add(this.AddBookMark);
            this.Controls.Add(this.BookMarks1);
            this.Controls.Add(this.fb2Content);
            this.Controls.Add(this.close);
            this.Controls.Add(this.topMenuStrip);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.fb2Reader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.topMenuStrip;
            this.MaximumSize = new System.Drawing.Size(1920, 1040);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.topMenuStrip.ResumeLayout(false);
            this.topMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem libToolStripMenuItem;
        private CustomRichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox close;
        private WebBrowser fb2Reader;
        private ListBox fb2Content;
        public ListBox BookMarks1;
        private Button AddBookMark;
        private Button DeleteBookMark;
        private TextBox textBox1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private Button GoToProgress;
        private Button SaveProgress;
    }
}

