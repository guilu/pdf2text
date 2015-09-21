using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace pdf2text
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usa: pdf2text [nomre pdf] [nombre txt]");
                return;
            }
            else
            {
                foreach (string s in args){
                    System.Console.WriteLine(s);
                }

                String ficheroEntrada = args[0];
                String ficheroSalida = args[1];

                StringBuilder text = new StringBuilder();

                if (File.Exists(ficheroEntrada))
                {
                    PdfReader pdfReader = new PdfReader(ficheroEntrada);

                    int paginaInicio = 1;
                    int numberOfPages = pdfReader.NumberOfPages;

                    for (int page = paginaInicio; page <= numberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        text.Append(currentText);
                    }
                    pdfReader.Close();
                }
                try
                {
                    File.WriteAllText(ficheroSalida, text.ToString(), Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error escribiendo fichero de salida" + ex.Message);
                }

            }
        }
    }
}
