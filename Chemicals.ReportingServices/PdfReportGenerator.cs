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
    using Models;

    public class PdfReportGenerator
    {
        private const string ReportsPath = "../../soldProductsReport.pdf";

        public PdfReportGenerator()
        {
        }

        /// <summary>
        /// Save a pdf file in  "../../test.pdf".
        /// </summary>
        /// <param name="deals">Expect Collection of objects that have Name, Address, ProductName and formula.</param>
        public void GenerateReport(IEnumerable<PdfReportModel> deals)
        {
            FileStream fileStream = new FileStream(ReportsPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Rectangle pageSize = new Rectangle(PageSize.A4);
            Document reportDocument = new Document(pageSize);
            PdfWriter pdfWriter = PdfWriter.GetInstance(reportDocument, fileStream);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 17);

            reportDocument.Open();

            PdfPTable reportTable = new PdfPTable(6);
            reportTable.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell headerCell = new PdfPCell(new Phrase("Produced products information", boldFont));
            headerCell.Colspan = 6;
            headerCell.HorizontalAlignment = 0;
            reportTable.AddCell(headerCell);

            this.PutHeadCells(reportTable);

            foreach (var deal in deals)
            {
                reportTable.AddCell(deal.ProductName);
                reportTable.AddCell(deal.Quantity);
                reportTable.AddCell(deal.PricePerUnit);
                reportTable.AddCell(deal.Formula);
                reportTable.AddCell(deal.Address);
                reportTable.AddCell(deal.Total);
            }

            reportDocument.Add(reportTable);
            reportDocument.Close();
        }

        public void PutHeadCells(PdfPTable table)
        {
            var productName = new PdfPCell(new Phrase("Product name"));
            var quantity = new PdfPCell(new Phrase("Quantity"));
            var pricePerUnit = new PdfPCell(new Phrase("PricePerUnit"));
            var formula = new PdfPCell(new Phrase("Formula"));
            var address = new PdfPCell(new Phrase("Address"));
            var total = new PdfPCell(new Phrase("Total"));

            productName.BackgroundColor = BaseColor.LIGHT_GRAY;
            quantity.BackgroundColor = BaseColor.LIGHT_GRAY;
            formula.BackgroundColor = BaseColor.LIGHT_GRAY;
            address.BackgroundColor = BaseColor.LIGHT_GRAY;
            table.AddCell(productName);
            table.AddCell(quantity);
            table.AddCell(pricePerUnit);
            table.AddCell(formula);
            table.AddCell(address);
            table.AddCell(total);
        }
    }
}
