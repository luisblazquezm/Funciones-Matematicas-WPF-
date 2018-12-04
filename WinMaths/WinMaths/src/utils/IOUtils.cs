using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WinMaths.src.utils
{
    public class IOUtils
    {
        public void SaveToImage(Panel visual, string fileName)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            SaveUsingEncoder(visual, fileName, encoder);
        }

        public void SaveUsingEncoder(Panel visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                (int)visual.ActualWidth,
                (int)visual.ActualHeight,
                96,
                96,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }

        public void WriteToExcel<T>(List<T> objectToWrite)
        {
            ExportToExcel<T> exporter = new ExportToExcel<T>
            {
                dataToPrint = objectToWrite
            };
            exporter.GenerateReport();
        }

        public List<T> ReadFromFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (List<T>)binaryFormatter.Deserialize(stream);
            }
        }

        public void WriteToFile<T>(string filePath, List<T> objectToWrite, bool append)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
    }

    public class ExportToExcel<T>
    {
        public List<T> dataToPrint;
        // Excel object references.
        private Microsoft.Office.Interop.Excel.Application excelApp = null;
        private Workbooks books = null;
        private Workbook book = null;
        private Sheets sheets = null;
        private Worksheet sheet = null;
        private Range range = null;
        private Font font = null;
        // Optional argument variable
        private object optionalValue = Missing.Value;


        /// Generate report and sub functions
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseObject(sheet);
                ReleaseObject(sheets);
                ReleaseObject(book);
                ReleaseObject(books);
                ReleaseObject(excelApp);
            }
        }
        /// Make Microsoft Excel application visible
        private void OpenReport()
        {
            excelApp.Visible = true;
        }
        /// Populate the Excel sheet
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// Write data into the Excel sheet
        private void WriteData(object[] header)
        {
            object[,] objData = new object[dataToPrint.Count, header.Length];

            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember
            (header[i].ToString(), BindingFlags.GetProperty, null, item, null);
                    objData[j, i] = (y == null) ? "" : y.ToString();
                }
            }
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
        }
        /// Method to make columns auto fit according to data
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            range = sheet.get_Range(startRange, optionalValue);
            range = range.get_Resize(rowCount, colCount);
            range.Columns.AutoFit();
        }
        /// Create header from the properties
        private object[] CreateHeader()
        {
            PropertyInfo[] headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// Set Header style as bold
        private void SetHeaderStyle()
        {
            font = range.Font;
            font.Bold = true;
        }
        /// Method to add an excel rows
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            range = sheet.get_Range(startRange, optionalValue);
            range = range.get_Resize(rowCount, colCount);
            range.set_Value(optionalValue, values);
        }

        /// Create Excel application parameters instances
        private void CreateExcelRef()
        {
            excelApp = new Microsoft.Office.Interop.Excel.Application();
            books = (Workbooks)excelApp.Workbooks;
            book = (Workbook)(books.Add(optionalValue));
            sheets = (Sheets)book.Worksheets;
            sheet = (Worksheet)(sheets.get_Item(1));
        }

        /// Release unused COM objects
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
