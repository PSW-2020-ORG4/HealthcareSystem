using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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

namespace ProjekatZdravoKorporacija
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();
            var myDate = DateTime.Now;
            var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            startDate.SelectedDate = startOfMonth;
            endDate.SelectedDate = endOfMonth;

            createPDF();
            pdfViewerControl.Load(System.IO.Path.GetFullPath(@"Report.pdf"));
            

        }
        private PdfDocument createPDF()
        {
            if (startDate.SelectedDate != null && endDate.SelectedDate != null)
            {
                using (PdfDocument document = new PdfDocument())
                {
                    //Add a page to the document
                    PdfPage page = document.Pages.Add();

                    PdfGrid pdfGrid = new PdfGrid();

                    //Create PDF graphics for the page
                    PdfGraphics graphics = page.Graphics;

                    //Set the standard font
                    PdfFont fontTitle = new PdfStandardFont(PdfFontFamily.Helvetica, 18);
                    PdfFont fontTable = new PdfStandardFont(PdfFontFamily.Helvetica, 12);
                    PdfFont fontInfo = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Italic);


                    DateTime sd = (DateTime)startDate.SelectedDate;
                    DateTime ed = (DateTime)endDate.SelectedDate;

                    PdfBitmap image = new PdfBitmap(@"logo.png");

                    //Draw the image
                    graphics.DrawImage(image,0, 0);
                    graphics.DrawString("Bulevar oslobođenja 100", fontInfo, PdfBrushes.Black, new PointF(20, 70));
                    graphics.DrawString("021/100-100", fontInfo, PdfBrushes.Black, new PointF(20, 85));
                    graphics.DrawString("www.zdravokorporacija.com", fontInfo, PdfBrushes.Black, new PointF(20, 100));

                    //Draw the text
                    graphics.DrawString("Izvještaj o zauzetosti soba za period " + sd.ToShortDateString() + " - " + ed.ToShortDateString(), fontTitle, PdfBrushes.Black, new PointF(0, 150));


                    DataTable dataTable = new DataTable();
                    //Add columns to the DataTable
                    dataTable.Columns.Add("Broj sobe");
                    dataTable.Columns.Add("Namjena");
                    dataTable.Columns.Add("Broj zakazanih pregleda/operacija");
                    //Add rows to the DataTable.
                    dataTable.Rows.Add(new object[] { "104", "Soba za pregled", "25" });
                    dataTable.Rows.Add(new object[] { "125", "Soba za pregled", "32" });
                    dataTable.Rows.Add(new object[] { "260", "Operaciona soba", "8" });
                    dataTable.Rows.Add(new object[] { "307", "Soba za pregled", "53" });
                    dataTable.Rows.Add(new object[] { "445", "Soba za pregled", "32" });
                    dataTable.Rows.Add(new object[] { "528", "Operaciona soba", "84" });
                    dataTable.Rows.Add(new object[] { "560", "Soba za pregled", "55" });
                    dataTable.Rows.Add(new object[] { "660", "Operaciona soba", "22" });
                    dataTable.Rows.Add(new object[] { "699", "Soba za pregled", "18" });
                    dataTable.Rows.Add(new object[] { "701", "Operaciona soba", "33" });
                    dataTable.Rows.Add(new object[] { "789", "Soba za pregled", "10" });
                    dataTable.Rows.Add(new object[] { "850", "Soba za pregled", "21" });
                    //Assign data source.
                    pdfGrid.DataSource = dataTable;
                    pdfGrid.Style.Font = fontTable;
                    //Draw grid to the page of PDF document.

                    pdfGrid.Draw(page, new PointF(0, 220));

                    graphics.DrawString("sekr. Marko Marković", fontInfo, PdfBrushes.Black, new PointF(300, 800));
                    graphics.DrawString("---------------------", fontInfo, PdfBrushes.Black, new PointF(300, 820));


                    //Save the document
                    document.Save("Report.pdf");

                    return document;
                }
            }
            return null;
        }

        private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            createPDF();
            pdfViewerControl.Load(System.IO.Path.GetFullPath(@"Report.pdf"));
            
        }

        private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            createPDF();
            pdfViewerControl.Load(System.IO.Path.GetFullPath(@"Report.pdf"));
            
        }
    }
}
