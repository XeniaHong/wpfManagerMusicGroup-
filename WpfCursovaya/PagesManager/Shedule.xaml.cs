using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SQLite;
using System.Data;

using Syncfusion.DocIO.DLS;
using System.ComponentModel;
using System.Drawing;
using Syncfusion.DocIO;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для Shedule.xaml
    /// </summary>
    public partial class Shedule : Page
    {
        public Shedule()
        {
            InitializeComponent();
            // DataGridShedules.ItemsSource = DbAppEntities3.GetContext().Shedules.ToList();
            

            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1),
                IsEnabled = true
            };
            timer.Tick += (o, t) => { timeous.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/PagesManager/SheduleEdit.xaml", UriKind.Relative));
        }
             
        public void refresh()
        {
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Shedule";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    DataGridShedules.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }            
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searShedule = Search.Text;


            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Shedule  where shedule like '%" + searShedule + "%'";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    DataGridShedules.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }           

        }

       

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            // Creating a new document.
            WordDocument document = new WordDocument();
            //Adding a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = System.Drawing.Color.Black;
            style.ParagraphFormat.BeforeSpacing = 12;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();
            

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Отчет") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.Red;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Отчет") as WTextRange;
            textRange.CharacterFormat.FontSize = 18f;
            textRange.CharacterFormat.FontName = "Calibri";

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Это тестовый документ еще находится в разработке.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Таблица") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            //Appends table.
            IWTable table = section.AddTable();
            table.ResetCells(3, 2);
            table.TableFormat.Borders.BorderType = BorderStyle.None;
            table.TableFormat.IsAutoResized = true;

            ////Appends paragraph.
            //paragraph = table[0, 0].AddParagraph();
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.BreakCharacterFormat.FontSize = 12f;
            

            ////Appends paragraph.
            //paragraph = table[0, 1].AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.AppendText("Mountain-200");
            ////Appends paragraph.
            //paragraph = table[0, 1].AddParagraph();
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.BreakCharacterFormat.FontSize = 12f;
            //paragraph.BreakCharacterFormat.FontName = "Times New Roman";

            //textRange = paragraph.AppendText("Product No: BK-M68B-38\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Size: 38\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Weight: 25\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Price: $2,294.99\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            ////Appends paragraph.
            //paragraph = table[0, 1].AddParagraph();
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.BreakCharacterFormat.FontSize = 12f;

            ////Appends paragraph.
            //paragraph = table[1, 0].AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.AppendText("Mountain-300 ");
            ////Appends paragraph.
            //paragraph = table[1, 0].AddParagraph();
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.BreakCharacterFormat.FontSize = 12f;
            //paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Product No: BK-M47B-38\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Size: 35\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Weight: 22\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Price: $1,079.99\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            ////Appends paragraph.
            //paragraph = table[1, 0].AddParagraph();
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.BreakCharacterFormat.FontSize = 12f;

            ////Appends paragraph.
            //paragraph = table[1, 1].AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.LineSpacing = 12f;

            //Appends picture to the paragraph.
         

            //Appends paragraph.
            //paragraph = table[2, 0].AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            ////Appends picture to the paragraph.
           

            ////Appends paragraph.
            //paragraph = table[2, 1].AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.AppendText("Road-150 ");
            ////Appends paragraph.
            //paragraph = table[2, 1].AddParagraph();
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            //paragraph.BreakCharacterFormat.FontSize = 12f;
            //paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Product No: BK-R93R-44\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Size: 44\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Weight: 14\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange = paragraph.AppendText("Price: $3,578.27\r") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            ////Appends paragraph.
            //section.AddParagraph();

            //Saves the Word document
            document.Save("Отчет.docx");

            MessageBox.Show("Отчет создан!");
        }
    }
}
