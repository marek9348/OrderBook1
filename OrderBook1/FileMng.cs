using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace OrderBook1
{
    public class FileMng
    {
        //Root path to other directories e.g. AppData, MyDocs...
        public string RootPath { get; set; }
        public string MyPath { get; set; }
        //Path to current file
        public string CurrentFilePath { get; set; }


        public FileMng()
        {
            RootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            CurrentFilePath = "";

        }
        //Sets the current folder
        /// <summary>
        /// User can set the current folder where the subfolders will be selected
        /// </summary>
        /// <returns>String of path</returns>
        public string SetRootFolder()
        {
            //Opens file dialog
            SaveFileDialog selFolderFileDialog = new SaveFileDialog();
            //openFileDialog.Filter = "All files (*.*)|*.*";
            //Sets the InitialDir of openFileDialog to MyDocuments
            selFolderFileDialog.InitialDirectory = RootPath;
            //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //MyDocuments
            selFolderFileDialog.Title = "Zvoľ adresár"; //Title of the window
            selFolderFileDialog.Filter = "Directory|*.this.directory"; // Prevents displaying files
            selFolderFileDialog.FileName = "select"; // Filename will then be "select.this.directory"                                 

            string path = ""; //Result value
            if (selFolderFileDialog.ShowDialog() == true)
            {
                path = selFolderFileDialog.FileName;
                // Remove fake filename from resulting path
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                // If user has changed the filename, create the new directory
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

            }

            return path;
        }

        /// <summary>
        /// Gets text from pdf file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Text from pdf as string</returns>
        public string ExtractTextFromPdf(string path)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(path));
            iText.Kernel.Geom.Rectangle rect = new iText.Kernel.Geom.Rectangle(36, 750, 523, 56);
            //CustomFontFilter fontFilter = new CustomFontFilter(rect);
            FilteredEventListener listener = new FilteredEventListener();

            // Create a text extraction renderer
            LocationTextExtractionStrategy extractionStrategy = listener
                .AttachEventListener(new LocationTextExtractionStrategy());
            //, fontFilter
            // Note: If you want to re-use the PdfCanvasProcessor, you must call PdfCanvasProcessor.reset()
            new PdfCanvasProcessor(listener).ProcessPageContent(pdfDoc.GetFirstPage());

            // Get the resultant text after applying the custom filter
            String actualText = extractionStrategy.GetResultantText();

            pdfDoc.Close();

            return actualText;
        }

        /// <summary>
        /// Returns the text from prd file.
        /// </summary>
        /// <returns>String</returns>
        public string OpenFile()
        {
            //Inits result collection
            string result = "";
            //Opens file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Transit sk (*.SKY)|*.SKY|Transit fr (*.FRA)|*.FRA";
            openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf";
            //Sets the InitialDir of openFileDialog
            openFileDialog.InitialDirectory = RootPath;
            if (openFileDialog.ShowDialog() == true)
            {
                //Open and load source text from file
                //string fileContent = File.ReadAllText(openFileDialog.FileName);
                string fileContent = ExtractTextFromPdf(openFileDialog.FileName);
                //Show new lines
                /*string[] lines = fileContent.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    result += line + "->" + Environment.NewLine;
                }*/
                result = fileContent;
                string modifiedText = "";
                for(int i = 0; i < result.Length; i++)
                {
                    if (Char.IsSeparator(result[i]))
                    {
                        modifiedText += " ";
                    }
                    else if (result[i]=='\r')
                    {
                        modifiedText += Environment.NewLine;
                    }
                    else if (result[i] == '\n')
                    {
                        modifiedText += Environment.NewLine;
                    }
                    else if (Char.IsControl(result[i]))
                    {
                        modifiedText += " ";
                    }
                    else
                    { 
                        modifiedText += result[i]; 
                    }
                }
                result = modifiedText;
                //Set the currentfilepath
                SetCurrentFilePath(openFileDialog.FileName);
                //result = fileContent;
            }
            return result;
        }
        /// <summary>
        /// Sets CurrentFilePath
        /// </summary>
        /// <param name="path"></param>
        private void SetCurrentFilePath(string path)
        {
            CurrentFilePath = path;
        }
        /// <summary>
        /// Gets file path
        /// </summary>
        /// <returns></returns>
        public string GetCurrentFilePath()
        {
            //Inits result collection
            string result = "";
            //Opens file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "All files (*.*)|*.*|Text files (*.txt)|*.txt|Transit sk (*.SKY)|*.SKY|Transit fr (*.FRA)|*.FRA";
            openFileDialog.Filter = "Pdf files (*.pdf)|*.pdf";
            //Sets the InitialDir of openFileDialog
            openFileDialog.InitialDirectory = RootPath;
            if (openFileDialog.ShowDialog() == true)
            {
                result = openFileDialog.FileName;
            }

            return result;
        }
        public void SaveFile(RichTextBox rtb)
        {
            /*
            //Get content of richtextbox
            TextMng tmng = new TextMng();
            string text = tmng.GetAllText(rtb);
            //Is file is new
            if (CurrentFilePath == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, text);
                CurrentFilePath = saveFileDialog.FileName;
            }

            else
            {
                File.WriteAllText(CurrentFilePath, text);
            }*/
        }
    }
}
