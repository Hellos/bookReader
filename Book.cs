using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApp1
{

    public class Library
    {
        public string Path { get; set; }
        public BindingList<Book> bookList;

        public Library()
        {
            bookList = new BindingList<Book>();
        }

        public void addBook(Book book)
        {
            foreach(var tmpBook in bookList)
            {
                if(tmpBook.filePath == book.filePath)
                {
                    return;
                }
            }
            this.bookList.Add(book);
        }

        public void delBook(int i)
        {
            bookList.RemoveAt(i);
        }

        public Book getBook(int i)
        {
            return bookList[i];
        }

        public bool writeLib()
        {
            FileStream fs;
            BinaryWriter writer;
            if (!Directory.Exists(@"C:\Users\Hellos-PC\Documents\HellosBookReader"))
            {
                Directory.CreateDirectory(@"C:\Users\Hellos-PC\Documents\HellosBookReader");
            }
            if (!File.Exists(@"C:\Users\Hellos-PC\Documents\HellosBookReader\lib.hbr"))
            {

                fs = File.Create(@"C:\Users\Hellos-PC\Documents\HellosBookReader\lib.hbr");
            }
            else
            {
                fs = File.Open(@"C:\Users\Hellos-PC\Documents\HellosBookReader\lib.hbr", FileMode.Truncate);
            }
            writer = new BinaryWriter(fs);
            foreach (var tmpBook in bookList)
            {
                writer.Write(tmpBook.filePath);
                writer.Write(tmpBook.Progress);
            }
            writer.Close();
            return true;
        }

        public void readLib()
        {
            BinaryReader reader = new BinaryReader(File.Open(@"C:\Users\Hellos-PC\Documents\HellosBookReader\lib.hbr", FileMode.OpenOrCreate));
            while (reader.PeekChar() > -1)
            {
                string tmpPath = reader.ReadString();
                int tmpProgress = reader.ReadInt32();
                this.bookList.Add(new Book(tmpPath, tmpProgress));
            }
            reader.Close();
        }
    }

    public class Book
    {

        public ObservableCollection<Tuple<string, string>> bookMarks { get; } = new ObservableCollection<Tuple<string, string>>();
        public ObservableCollection<Tuple<string, string>> content { get; } = new ObservableCollection<Tuple<string, string>>();
        public string Name { get; set; }
        public string filePath { get; set; }
        public int Progress { get; set; }
        public string HTMLPath { get; set; }
        public Book(string filePath, int Progress)
        {

            this.filePath = filePath;
            this.Progress = Progress;
            this.Name = Path.GetFileNameWithoutExtension(this.filePath);
        }

        public virtual string open()
        {
            return null;
        }

        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(filePath) + " " + Progress.ToString();
        }
    }



    public class fb2Book : Book
    {
        public fb2Book(string filePath, int Progress) : base(filePath, Progress)
        {
            this.Name = Path.GetFileNameWithoutExtension(this.filePath);
        }

        public override string open()
        {
            if (!Directory.Exists(@"C:\Users\Hellos-PC\Documents\HellosBookReader" + "\\tmp"))
            {
                Directory.CreateDirectory(@"C:\Users\Hellos-PC\Documents\HellosBookReader" + "\\tmp");
            }
            string HTMLdoc = openFB2file(filePath);
            FileStream fs = File.Create(@"C:\Users\Hellos-PC\Documents\HellosBookReader" + "\\tmp" + "\\" + this.Name + ".html");
            byte[] barr = System.Text.Encoding.UTF8.GetBytes(openFB2file(filePath));
            fs.Write(barr, 0, barr.Length);
            fs.Close();
            this.HTMLPath = @"C:\Users\Hellos-PC\Documents\HellosBookReader\tmp\" + this.Name + ".html";
            return HTMLPath;
        }


        public string openFB2file(string file)
        {
            //Некоторые настройки
            int fontSize = 20;

            List<String> thisToken = new List<string>();
            string special = "";
            string rId = "";
            string rType = "";
            string opt = "";
            int ind = 1;


            string description = "";

            StringBuilder bookHtml = new StringBuilder();
            var reader = XmlReader.Create(file);

            bookHtml.AppendLine("<!DOCTYPE HTML><html><head><meta charset=\"UTF-8\"/></head><body style=\"font-size:20px; font-family:Sans, Times New Roman;\">");
            while (reader.Read())
            {

                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        thisToken.Add(reader.Name);

                        if (thisToken.Last() == "section" && !reader.HasAttributes)
                        {
                            thisToken.RemoveAt(thisToken.Count - 1);
                        }

                        if (reader.IsEmptyElement && reader.Name != "image")
                        {
                            thisToken.RemoveAt(thisToken.Count - 1);
                        }

                        if (thisToken.Contains("description"))
                        {
                            if (thisToken.Last() != "image")
                            {
                                break;
                            }
                        }

                        if (reader.Name == ("title"))
                        {
                            break;
                        }

                        if (reader.Name == ("body"))
                            if (reader.HasAttributes && reader.GetAttribute(0) == "notes")
                                special = "notes";

                        if (special == "notes")
                        {
                            if (reader.Name == ("section"))
                            {
                                if (reader.HasAttributes)
                                {
                                    rId = reader.GetAttribute(0);
                                    rType = "";
                                }
                            }
                        }

                        opt = " align=\"justify\"";

                        if (thisToken.Contains("title"))
                        {
                            opt = " align=\"center\" style=\"font-size:" + (fontSize * 1.5).ToString() + "px\"";
                            if (special == "notes")
                            {
                                opt += (" id=\"" + rId + "\"");
                            }
                        }

                        if (thisToken.Contains("subtitle"))
                        {
                            opt = " align=\"center\" style=\"font-size:" + (fontSize * 1.2).ToString() + "px\"";
                        }

                        if (thisToken.Contains("annotation"))
                        {
                            opt = " align=\"left\" ";
                        }



                        if (reader.Name == ("p") || reader.Name == ("subtitle"))
                        {
                            bookHtml.AppendLine("<p" + opt + " >");
                            break;
                        }

                        if (reader.Name == "empty-line")
                        {
                            bookHtml.AppendLine("<br/>");
                            break;
                        }

                        if (reader.Name == "strong"
                            || reader.Name == "sup"
                            || reader.Name == "sub"
                            || reader.Name == "code"
                            || reader.Name == "cite")
                        {
                            bookHtml.AppendLine("<" + reader.Name + ">");
                            break;
                        }

                        if (reader.Name == "emphasis")
                        {
                            bookHtml.AppendLine("<i>");
                            break;
                        }

                        if (reader.Name == "v")
                        {
                            bookHtml.AppendLine("<p align=\"left\" style=\"margin-left:25px;\">");
                            break;
                        }

                        if (reader.Name == "strikethrough")
                        {
                            bookHtml.AppendLine("<strike>");
                        }

                        if (reader.Name == "a")
                        {
                            rId = "";
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                if (reader.Name == "type")
                                {

                                }
                                if (reader.Name.Contains("href"))
                                {
                                    rId = reader.ReadContentAsString();
                                }
                                bookHtml.AppendLine("<a href=\"" + rId + "\"> ");
                            }
                        }

                        if (reader.Name == "poem"
                            || reader.Name == "stanza"
                            || reader.Name == "epigraph")
                        {
                            break;
                        }

                        if (reader.Name == "text-author")
                        {
                            bookHtml.AppendLine("<p align=\"justify\" style=\"margin-left:45px;\">");
                            break;
                        }

                        if (reader.Name == "date")
                        {
                            bookHtml.AppendLine("<p align=\"justify\" style=\"margin-left:45px;\">");
                            break;
                        }

                        if (reader.Name == "image")
                        {
                            if (reader.HasAttributes)
                            {
                                bookHtml.AppendLine("<p align=\"center\">" + reader.GetAttribute(0) + "#" + "</p>");
                                thisToken.Remove("image");
                            }
                        }

                        if (reader.Name == "binary")
                        {
                            for (int i = 0; i < reader.AttributeCount; i++)
                            {
                                reader.MoveToAttribute(i);
                                if (reader.Name == "id")
                                {
                                    rId = reader.ReadContentAsString();
                                }
                                if (reader.Name.Contains("type"))
                                {
                                    rType = reader.ReadContentAsString();
                                }
                            }
                        }


                        break;

                    case XmlNodeType.EndElement:
                        if (thisToken.Last() == reader.Name)
                        {
                            thisToken.RemoveAt(thisToken.Count - 1);
                        }

                        if (thisToken.Contains("description"))
                        {
                            break;
                        }

                        if (reader.Name == ("p")
                            || reader.Name == "subtitle"
                            || reader.Name == "v"
                            || reader.Name == "date"
                            || reader.Name == "text-author")
                        {
                            bookHtml.AppendLine("</p>");
                            bookHtml.ToString();
                            break;
                        }

                        if (reader.Name == "sup"
                            || reader.Name == "sub"
                            || reader.Name == "strong"
                            || reader.Name == "code"
                            || reader.Name == "cite")
                        {
                            bookHtml.AppendLine("</" + reader.Name + ">");
                            break;
                        }

                        if (reader.Name == "a")
                        {
                            rId.Replace("#", "");
                            bookHtml.AppendLine("</a><a name=\"" + rId + "back" + "\"></a>");
                            break;
                        }

                        if (reader.Name == "emphasis")
                        {
                            bookHtml.AppendLine("</i>");
                            break;
                        }

                        if (reader.Name == "strikethrough")
                        {
                            bookHtml.AppendLine("</strike>");
                            break;
                        }

                        if (reader.Name == "stanza")
                        {
                            break;
                        }

                        if (reader.Name == "epigraph"
                            || reader.Name == "poem")
                        {
                            break;
                        }

                        if (special == "notes")
                        {
                            if (reader.Name == "body")
                            {
                                special = "";
                            }
                            if (reader.Name == "section")
                            {
                                bookHtml.Insert(bookHtml.ToString().LastIndexOf("<"), "<a href=\"#" + rId + "back" + "\"> назад</a>");
                            }
                        }

                        break;

                    case XmlNodeType.Text:
                        if (thisToken.Contains("description"))
                        {
                            break;
                        }
                        if (thisToken.Contains("binary"))
                        {
                            string image = "<img src=\"data:"
                                             + rType + ";base64,"
                                             + reader.Value
                                             + "\"/>";
                            bookHtml.Replace("#" + rId + "#", image);
                            break;
                        }

                        if (thisToken.Contains("div"))
                        {
                            break;
                        }

                        if (thisToken.Last() == "FictionBook")
                        {
                            break;
                        }

                        if (thisToken.Contains("title") && !thisToken.Contains("description") && special != "notes")
                        {
                            bookHtml.AppendLine("<a name=\"content__" + ind.ToString() + "\"></a>");
                            this.content.Add(new Tuple<string, string>(reader.Value, "content__" + ind.ToString()));
                            ind++;
                        }

                        if (special == "notes" && !thisToken.Contains("title"))
                        {
                            rType += " ";
                            rType += reader.Value;
                        }

                        if (thisToken.Last() == "p"
                            || thisToken.Last() == "v"
                            || thisToken.Last() == "emphasis"
                            || thisToken.Last() == "strong"
                            || thisToken.Last() == "strikethrough"
                            || thisToken.Last() == "sub"
                            || thisToken.Last() == "sup"
                            || thisToken.Last() == "code"
                            || thisToken.Last() == "cite"
                            || thisToken.Last() == "text-author"
                            || thisToken.Last() == "date"
                            )
                        {
                            bookHtml.AppendLine(reader.Value);
                        }

                        if (thisToken.Last().Contains("section"))
                        {
                            break;
                        }

                        if (thisToken.Last() == "body")
                        {
                            break;
                        }

                        if ((thisToken.Last() == "title" && thisToken.Contains("description"))
                            || thisToken.Last() == "poem"
                            || thisToken.Last() == "stanza")
                        {
                            break;
                        }


                        if (thisToken.Last() == "annotation")
                        {
                            break;
                        }

                        if (thisToken.Last() == "a")
                        {
                            bookHtml.AppendLine(reader.Value);
                            break;
                        }

                        if (!(reader.Value.Count() != 0))
                        {
                            bookHtml.AppendLine("<span> " + reader.Value + "</span>");
                        }

                        break;
                    default:
                        break;

                }
            }
            bookHtml.AppendLine("</body></html>");
            return bookHtml.ToString();
        }
    }
}
