using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;


namespace CubePDF
{
    class PDFMerger
    {
        public static bool merge(string headFilePath, string tailFilePath, string outputPath)
        {
            var ret = true;
            try
            {
                var headReader = new PdfReader(headFilePath);
                var tailReader = new PdfReader(tailFilePath);
                using (var fs = new FileStream(outputPath, FileMode.Create))
                {
                    PdfCopyFields copy = new PdfCopyFields(fs);
                    copy.AddDocument(headReader);
                    copy.AddDocument(tailReader);
                    copy.Close();
                }
                headReader.Close();
                tailReader.Close();
            }
            catch (Exception e)
            {
                ret = false;
            }
            return ret;
        }
    }
}
