using iText.PdfCleanup;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testProject
{
    public class PDFEdit
    {
        /// <summary>
        /// Find the text and replace in PDF
        /// </summary>
        /// <param name="sourceFile">The source PDF file where text to be searched</param>
        /// <param name="descFile">The new destination PDF file which will be saved with replaced text</param>
        /// <param name="textToBeSearched">The text to be searched in the PDF</param>
        /// <param name="textToBeReplaced">The text to be replaced with</param>
        public void ReplaceTextInPDF(string sourceFile, string descFile, string textToBeSearched, string textToBeReplaced)
        {
            ReplaceText(textToBeSearched, textToBeReplaced, descFile, sourceFile);
        }
        private void ReplaceText(string textToBeSearched, string textToAdd, string outputFilePath, string inputFilePath)
        {
            try
            {
                using (Stream inputPdfStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream inputImageStream = new FileStream(@"C:\signature_client.jpg", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputPdfStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                using (Stream outputPdfStream2 = new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    //Opens the unmodified PDF for reading
                    PdfReader reader = new PdfReader(inputPdfStream);

                    //Creates a stamper to put an image on the original pdf
                    PdfStamper stamper = new PdfStamper(reader, outputPdfStream); //{ FormFlattening = true, FreeTextFlattening = true };
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        var parser = new PdfReaderContentParser(reader);
                        var strategy = parser.ProcessContent(i, new LocationTextExtractionStrategyWithPosition());
                        var res = strategy.GetLocations();
                        var searchResult = res.Where(p => p.Text.Contains(textToBeSearched)).ToList();
                        if (searchResult.Count > 0)
                        {

                            Console.WriteLine(searchResult[0].Text);
                            Console.WriteLine(searchResult[0].X);
                            Console.WriteLine(searchResult[0].Y);
                            var p = searchResult[0];


                            IList<iTextSharp.xtra.iTextSharp.text.pdf.pdfcleanup.PdfCleanUpLocation> cleanUpLocations = new List<iTextSharp.xtra.iTextSharp.text.pdf.pdfcleanup.PdfCleanUpLocation>();
                            cleanUpLocations.Add(new iTextSharp.xtra.iTextSharp.text.pdf.pdfcleanup.PdfCleanUpLocation(i, new iTextSharp.text.Rectangle(p.X, p.Y - 8, p.X + 85, p.Y - 8 + 20), iTextSharp.text.BaseColor.WHITE));
                            iTextSharp.xtra.iTextSharp.text.pdf.pdfcleanup.PdfCleanUpProcessor cleaner = new iTextSharp.xtra.iTextSharp.text.pdf.pdfcleanup.PdfCleanUpProcessor(cleanUpLocations, stamper);
                            cleaner.CleanUp();

                            Bitmap transparentBitmap = new Bitmap(85, 20);
                            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(transparentBitmap, BaseColor.WHITE);
                            //Sets the position that the image needs to be placed (ie the location of the text to be removed)
                            image.SetAbsolutePosition(p.X, (p.Y - 8));
                            iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(inputImageStream);
                            image2.SetAbsolutePosition(p.X, (p.Y - 8));
                            image2.ScaleAbsoluteWidth(85);
                            image2.ScaleAbsoluteHeight(20);

                            PdfFormField field = PdfFormField.CreateSignature(stamper.Writer);
                            field.FieldName = "SignatureField";
                            field.SetWidget(new iTextSharp.text.Rectangle(p.X, p.Y - 8, p.X+85, p.Y - 8+20), PdfAnnotation.HIGHLIGHT_INVERT);
                            field.SetFieldFlags(PdfAnnotation.FLAGS_PRINT);
                            stamper.AddAnnotation(field,i);

                            stamper.GetOverContent(i).AddImage(image, true); // i stands for the page no.
                            stamper.GetOverContent(i).AddImage(image2, true);
                            PdfContentByte cb = stamper.GetOverContent(i);

                            // select the font properties
                            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            cb.SetColorFill(BaseColor.BLACK);
                            cb.SetFontAndSize(bf, 7);

                            // write the text in the pdf content
                            cb.BeginText();
                            // put the alignment and coordinates here
                            cb.ShowTextAligned(1, textToAdd, p.Rect.Left + 10, p.Rect.Top - 6, 0);
                            cb.EndText();
                        }
                        // var ex = PdfTextExtractor.GetTextFromPage(reader, i, tt); // ex will be holding the text, although we just need the rectangles [RectAndText class objects]
                        //foreach (var p in tt.myPoints)
                        //{
                        //    //Creates an image that is the size i need to hide the text i'm interested in removing
                           
                        //}
                    }
                    //Creates the first copy of the outputted pdf
                    stamper.Close();

                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
