namespace Chemicals.DataImport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfReportGenerator
    {
        private const string ReportsPath = "../../test.pdf";

        public PdfReportGenerator()
        {
        }

        /// <summary>
        /// Save a pdf file in  "../../test.pdf".
        /// </summary>
        /// <param name="deals">Expect Collection of objects that have Name, Address, ProductName and formula.</param>
        public void GenerateReport(IEnumerable<dynamic> deals)
        {
            FileStream fileStream = new FileStream(ReportsPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Rectangle pageSize = new Rectangle(PageSize.A4);
            Document reportDocument = new Document(pageSize);
            PdfWriter pdfWriter = PdfWriter.GetInstance(reportDocument, fileStream);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

            reportDocument.Open();

            PdfPTable reportTable = new PdfPTable(4);
            reportTable.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell headerCell = new PdfPCell(new Phrase("Produced products information", boldFont));
            headerCell.Colspan = 4;
            headerCell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            reportTable.AddCell(headerCell);

            PutHeadCells(reportTable);

            foreach (var item in deals)
            {
                reportTable.AddCell(item.GetType().GetProperty("Name").GetValue(item, null));
                reportTable.AddCell(item.GetType().GetProperty("Address").GetValue(item, null));
                reportTable.AddCell(item.GetType().GetProperty("ProductName").GetValue(item, null));
                reportTable.AddCell(item.GetType().GetProperty("Formula").GetValue(item, null));
            }

            reportDocument.Add(reportTable);
            reportDocument.Close();
        }

        public void PutHeadCells(PdfPTable table)
        {
            var manufacturerNameCell = new PdfPCell(new Phrase("Manufacturer name"));
            var manufacturerAddressCell = new PdfPCell(new Phrase("Manufacturer address"));
            var productNameCell = new PdfPCell(new Phrase("Product Name"));
            var productFormulaCell = new PdfPCell(new Phrase("Product Formula"));

            manufacturerNameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            manufacturerAddressCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            productNameCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            productFormulaCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(manufacturerNameCell);
            table.AddCell(manufacturerAddressCell);
            table.AddCell(productNameCell);
            table.AddCell(productFormulaCell);
        }
    }
}
