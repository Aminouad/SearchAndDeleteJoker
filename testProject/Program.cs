//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using System.Configuration;

////Document pdfDoc = new Document();
////string path = "test.pdf";
////PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Append));
////pdfDoc.Open();
////Paragraph paragraph = new Paragraph("Getting Started");
////pdfDoc.Add(paragraph);
////Image imghead = Image.GetInstance("logo_Vm.png");
////pdfDoc.Add(imghead);
////pdfWriter.CloseStream = true;
////pdfDoc.Close();

//string oldFile = "oldFile.pdf";
//string newFile = "newFile.pdf";

//// open the reader
//PdfReader reader = new PdfReader(oldFile);
//Rectangle size = reader.GetPageSizeWithRotation(1);
//Document document = new Document(size);
//// open the writer
//FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
//PdfWriter writer = PdfWriter.GetInstance(document, fs);
//document.Open();
//// the pdf content
//PdfContentByte cb = writer.DirectContent;

//PdfFormField pdfFormField = PdfFormField.CreateSignature(writer);
//Rectangle rectangle = new Rectangle(222, 222, 222, 222);
//pdfFormField.SetWidget(rectangle, PdfAnnotation.HIGHLIGHT_OUTLINE);
//pdfFormField.FieldName = "myEmptySignatureField";
//pdfFormField.Flags = PdfAnnotation.FLAGS_PRINT;
//pdfFormField.SetPage();

////Image imghead = Image.GetInstance("logo_Vm.png");

////imghead.SetAbsolutePosition(50, 0);
////cb.AddImage(imghead);
//// create the new page and add it to the pdf
//PdfImportedPage page = writer.GetImportedPage(reader, 4);


//cb.AddTemplate(page, 0, 0);


//// close the streams and voilá the file should be changed :)
//document.Close();
//fs.Close();
//writer.Close();
//reader.Close();

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using testProject;


string sourceFile = @"C:\oldFile.pdf";
string descFile = @"C:\157.pdf";
PDFEdit pdfObj = new PDFEdit();
pdfObj.ReplaceTextInPDF(sourceFile, descFile, "#SIGNATURE #", "");
//var reader = new PdfReader(sourceFile);
//var dstFile = File.Open("DST FILE PATH GOES HERE", FileMode.Create);
//using (Stream outputPdfStream = new FileStream(descFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
//{
//    var pdfStamper = new PdfStamper(reader, outputPdfStream, reader.PdfVersion, false);

//    // We don't need to auto-rotate, as the PdfContentStreamEditor will already deal with pre-rotated space..
//    // if we enable this we will inadvertently rotate the content.
//    //pdfStamper.RotateContents = false;

//    // This is for the Text Replace
//    //var replaceTextProcessor = new TextReplaceStreamEditor(
//    //    "#SIGNATURE",
//    //    "");
//    pdfStamper.RotateContents = false;
//    TextRemover editor = new TextRemover();

//    for (int i = 1; i <= reader.NumberOfPages; i++)
//    {
//        editor.EditPage(pdfStamper, i);
//    }
//    //for (int i = 1; i <= reader.NumberOfPages; i++)
//    //    replaceTextProcessor.EditPage(pdfStamper, i);


//    //// This is for the Text Redact
//    ////var redactTextProcessor = new TextRedactStreamEditor(
//    ////    "#SIG");
//    ////for (int i = 1; i <= reader.NumberOfPages; i++)
//    ////    redactTextProcessor.EditPage(pdfStamper, i);
//    //// Since our redacting just puts a box over the top, we should secure the document a bit... just to prevent people copying/pasting the text behind the box.. we also prevent text to speech processing of the file, otherwise the 'hidden' text will be spoken
//    //pdfStamper.Writer.SetEncryption(null,
//    //    Encoding.UTF8.GetBytes("ownerPassword"),
//    //    PdfWriter.AllowDegradedPrinting | PdfWriter.AllowPrinting,
//    //    PdfWriter.ENCRYPTION_AES_256);

//    //// hey, lets get rid of Javascript too, because it's annoying
//    ////pdfStamper.Javascript = "";


//    //// and then finally we close our files (saving it in the process) 
//    //pdfStamper.Close();
//    //reader.Close();
//}

//pdfObj.ReplaceTextInPDF(sourceFile, descFile, "#SIGNATURE #", "hello"); 
//void VerySimpleReplaceText(string OrigFile, string ResultFile, string origText, string replaceText)
//{
//    using (PdfReader reader = new PdfReader(OrigFile))
//    {
//        StringBuilder text = new StringBuilder();

//        for (int i = 1; i <= reader.NumberOfPages; i++)
//        {
//            text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
//            Console.WriteLine(text);

//            /*byte[] contentBytes = reader.GetPageContent(i);
//            string contentString = PdfEncodings.ConvertToString(contentBytes, PdfObject.TEXT_PDFDOCENCODING);
//            contentString = contentString.Replace(origText, replaceText);

//            reader.SetPageContent(i, PdfEncodings.ConvertToBytes(contentString, PdfObject.TEXT_PDFDOCENCODING));*/
//        }

//        new PdfStamper(reader, new FileStream(ResultFile, FileMode.Create, FileAccess.Write)).Close();
//    }
//}
//VerySimpleReplaceText(sourceFile, descFile, "SIG", "cqdscqscqscqsc");

//class TextRemover : PdfContentStreamEditor
//{
//    protected override void Write(PdfContentStreamProcessor processor, PdfLiteral operatorLit, List<PdfObject> operands)
//    {
//        if (!TEXT_SHOWING_OPERATORS.Contains(operatorLit.ToString()))
//        {
//            base.Write(processor, operatorLit, operands);
//        }
//    }
//    List<string> TEXT_SHOWING_OPERATORS = new List<string> { "Tj", "'", "\"", "TJ" };
//}