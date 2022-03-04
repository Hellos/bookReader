using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class BookMark
    {
        public int Pos { get; set; }
        public string Name { get; set; }

        public BookMark(string Name, int Pos)
        {
            this.Name = Name;
            this.Pos = Pos;
        }
    }

    public class BookMarks
    {
        public BindingList<BookMark> bookMarks = new BindingList<BookMark>();

        public string fileName { get; set; }

        public BookMarks(string Name)
        {
            this.fileName = Name;
        }
        public void addBookMark(BookMark tmp)
        {
            this.bookMarks.Add(tmp);
        }
        public void delBookMark(int i)
        {
            this.bookMarks.RemoveAt(i);
        }
        public void readBookMarks()
        {
            BinaryReader reader = new BinaryReader(File.Open(@"C:\Users\Hellos-PC\Documents\HellosBookReader\" + fileName + ".bm", FileMode.OpenOrCreate));
            while (reader.PeekChar() > -1)
            {
                int tmpPos = reader.ReadInt32();
                string tmpName = reader.ReadString();
                this.bookMarks.Add(new BookMark(tmpName, tmpPos));
            }
            reader.Close();
        }

        public void writeBookMarks()
        {
            FileStream fs;
            BinaryWriter writer;
            if (!Directory.Exists(@"C:\Users\Hellos-PC\Documents\HellosBookReader"))
            {
                Directory.CreateDirectory(@"C:\Users\Hellos-PC\Documents\HellosBookReader");
            }
            if (!File.Exists(@"C:\Users\Hellos-PC\Documents\HellosBookReader\" + fileName + ".bm"))
            {

                fs = File.Create(@"C:\Users\Hellos-PC\Documents\HellosBookReader\" + fileName + ".bm");
            }
            else
            {
                fs = File.Open(@"C:\Users\Hellos-PC\Documents\HellosBookReader\" + fileName + ".bm", FileMode.Truncate);
            }
            writer = new BinaryWriter(fs);
            foreach (var tmpBookMark in bookMarks)
            {
                writer.Write(tmpBookMark.Pos);
                writer.Write(tmpBookMark.Name);
            }
            writer.Close();
        }
    }
}
