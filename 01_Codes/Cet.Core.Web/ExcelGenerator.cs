using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Core.Web
{
    public class ExcelUtility
    {
        //public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(ListToDataTable(list));

        //    return CreateExcelDocument(ds, xlsxFilePath);
        //}
        #region HELPER_FUNCTIONS
        //  This function is adapated from: http://www.codeguru.com/forum/showthread.php?t=450171
        //  My thanks to Carl Quirion, for making it "nullable-friendly".

        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }
        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        //public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
        //{
        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(dt);
        //    bool result = CreateExcelDocument(ds, xlsxFilePath);
        //    ds.Tables.Remove(dt);
        //    return result;
        //}
        #endregion

        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="dt">DataTable containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>True if it was created succesfully, otherwise false.</returns>
        public static byte[] CreateExcelDocument(DataTable dt, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                byte[] data = CreateExcelDocumentAsStream(ds, filename, Response);
                ds.Tables.Remove(dt);
                return data;
            }
            catch (Exception ex)
            {
                Logger.Write("Excel Generate Failed, exception thrown: " + ex.ToString());
                return null;
            }
        }

        public static bool CreateExcelDocument<T>(List<T> list, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));
                CreateExcelDocumentAsStream(ds, filename, Response);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write("Excel Generate Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        public static bool CreateExcelDocument<T>(List<T> list, List<ColumnHeaderData> headers, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));
                CreateExcelDocumentAsStream(ds, headers, filename, Response);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Write("Failed Generate Excel Line 134, exception thrown: " + ex.ToString());
                return false;
            }
        }
        public static byte[] CreateExcelDocumentToByteArray<T>(List<T> list, List<ColumnHeaderData> headers, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));

                return CreateExcelDocumentAsStream(ds, headers, filename, Response);

            }
            catch (Exception ex)
            {
                Logger.Write("Failed, exception thrown: " + ex.ToString());
                byte[] bytes = new byte[0];
                return bytes;
            }
        }

        public static byte[] CreateExcelDocumentAsStream(DataSet ds, List<ColumnHeaderData> headers, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                //Logger.Write("Start CreateExcelDocumentAsStream");
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, headers, document);
                }
                stream.Flush();
                stream.Position = 0;
                //Logger.Write("Flush Stream");

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.ContentType = "application/xlsx";
                byte[] data1 = new byte[stream.Length];
                stream.Read(data1, 0, data1.Length);
                stream.Close();
                //Logger.Write("Start Response Write");
                Response.BinaryWrite(data1);
                Response.Flush();
                Response.End();
                //Logger.Write("End Response Write");

                //Response.Content = new StreamContent(stream);
                //Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xlsx");
                //Response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = filename
                //};

                return data1;
            }
            catch (Exception ex)
            {
                Logger.Write("Failed Generate Excel, exception thrown: " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Create an Excel file, and write it out to a MemoryStream (rather than directly to a file)
        /// </summary>
        /// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="Response">HttpResponse of the current page.</param>
        /// <returns>Either a MemoryStream, or NULL if something goes wrong.</returns>
        public static byte[] CreateExcelDocumentAsStream(DataSet ds, string filename, System.Web.HttpResponse Response)
        {
            try
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, document);
                }
                stream.Flush();
                stream.Position = 0;

                Response.ClearContent();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                //  NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                //  manually added System.Web to this project's References.

                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.ContentType = "application/xlsx";
                byte[] data1 = new byte[stream.Length];
                stream.Read(data1, 0, data1.Length);
                stream.Close();
                Response.BinaryWrite(data1);
                Response.Flush();
                Response.End();

                //Response.Content = new StreamContent(stream);
                //Response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xlsx");
                //Response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = filename
                //};

                return data1;
            }
            catch (Exception ex)
            {
                Logger.Write("Failed Generate Excel, exception thrown: " + ex.ToString());
                return null;
            }
        }
        //  End of "INCLUDE_WEB_FUNCTIONS" section

        ///// <summary>
        ///// Create an Excel file, and write it to a file.
        ///// </summary>
        ///// <param name="ds">DataSet containing the data to be written to the Excel.</param>
        ///// <param name="excelFilename">Name of file to be written.</param>
        ///// <returns>True if successful, false if something went wrong.</returns>
        //public static bool CreateExcelDocument(DataSet ds, string excelFilename)
        //{
        //    try
        //    {
        //        using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
        //        {
        //            WriteExcelFile(ds, document);
        //        }
        //        //Trace.WriteLine("Successfully created: " + excelFilename);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Trace.WriteLine("Failed, exception thrown: " + ex.Message);
        //        return false;
        //    }
        //}

        private static void WriteExcelFile(DataSet ds, List<ColumnHeaderData> headers, SpreadsheetDocument spreadsheet)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

            //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            workbookStylesPart.Stylesheet = CreateStylesheet();

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                Columns columns = new Columns();
                UInt32 index = 1;
                foreach (ColumnHeaderData header in headers)
                {
                    columns.Append(CreateColumnData(index, index, header.Width));
                    index++;
                }
                newWorksheetPart.Worksheet.Append(columns);

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                // save worksheet
                WriteDataTableToExcelWorksheet(dt, headers, newWorksheetPart);
                newWorksheetPart.Worksheet.Save();

                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName
                });

                worksheetNumber++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
        }

        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

            //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            Stylesheet stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet = stylesheet;

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet();

                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.SheetData());

                // save worksheet
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart);
                newWorksheetPart.Worksheet.Save();

                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheets());

                spreadsheet.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>().AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = (uint)worksheetNumber,
                    Name = dt.TableName
                });

                worksheetNumber++;
            }

            spreadsheet.WorkbookPart.Workbook.Save();
        }

        private static void WriteDataTableToExcelWorksheet(DataTable dt, List<ColumnHeaderData> headers, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();



            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = headers.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];
            bool[] IsDateColumn = new bool[numberOfColumns];
            bool[] IsDateTimeOffsetColumn = new bool[numberOfColumns];
            CellFormatStyle[] cellFormats = new CellFormatStyle[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
            sheetData.Append(headerRow);

            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                ColumnHeaderData col = headers[colInx];




                AppendHeaderTextCell(excelColumnNames[colInx] + "1", col.HeaderName, headerRow, col.BgColor);
                IsNumericColumn[colInx] = (col.DataType == typeof(decimal)) || (col.DataType == typeof(int) || col.DataType == typeof(long) || col.DataType == typeof(short));
                IsDateColumn[colInx] = col.DataType == typeof(DateTime);
                IsDateTimeOffsetColumn[colInx] = col.DataType == typeof(DateTimeOffset);
                cellFormats[colInx] = col.Format;
            }

            //
            //  Now, step through each row of data in our DataTable...
            //
            double cellNumericValue = 0;

            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                sheetData.Append(newExcelRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    int tempIndex = colInx;
                    if (!string.IsNullOrEmpty(headers[colInx].FieldName))
                        tempIndex = dt.Columns.IndexOf(headers[colInx].FieldName);

                    cellValue = dr.ItemArray[tempIndex].ToString();

                    // Create cell with data
                    if ((IsNumericColumn[colInx] || IsDateColumn[colInx] || IsDateTimeOffsetColumn[colInx]))
                    {
                        if (string.IsNullOrEmpty(cellValue))
                        {
                            //  For text cells, just write the input data straight out to the Excel file.
                            AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                        }
                        else if (IsNumericColumn[colInx])
                        {
                            //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                            //  If this numeric value is NULL, then don't write anything to the Excel file.
                            cellNumericValue = 0;
                            if (double.TryParse(cellValue, out cellNumericValue))
                            {
                                cellValue = cellNumericValue.ToString();
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow, cellFormats[colInx]);
                            }
                            else
                            {
                                AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                            }
                        }
                        else if (IsDateColumn[colInx])
                        {
                            AppendDateCell(excelColumnNames[colInx] + rowIndex.ToString(), DateTime.Parse(cellValue), newExcelRow, cellFormats[colInx]);
                        }
                        else if (IsDateTimeOffsetColumn[colInx])
                        {
                            AppendDateTimeOffsetCell(excelColumnNames[colInx] + rowIndex.ToString(), DateTimeOffset.Parse(cellValue), newExcelRow, cellFormats[colInx]);
                        }
                    }
                    else
                    {
                        //  For text cells, just write the input data straight out to the Excel file.
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                    }
                }
            }
        }


        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();

            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            uint rowIndex = 1;

            var headerRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
            sheetData.Append(headerRow);

            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];
                AppendHeaderTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow);
                IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32");
            }

            //
            //  Now, step through each row of data in our DataTable...
            //
            double cellNumericValue = 0;
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex };  // add a row at the top of spreadsheet
                sheetData.Append(newExcelRow);

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();

                    // Create cell with data
                    if (IsNumericColumn[colInx])
                    {
                        //  For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        //  If this numeric value is NULL, then don't write anything to the Excel file.
                        cellNumericValue = 0;
                        if (double.TryParse(cellValue, out cellNumericValue))
                        {
                            cellValue = cellNumericValue.ToString();
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                        }
                    }
                    else
                    {
                        //  For text cells, just write the input data straight out to the Excel file.
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                    }
                }
            }
        }
        private static void AppendHeaderTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = 1;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }
        private static void AppendHeaderTextCell(string cellReference, string cellStringValue, Row excelRow, int color)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = Convert.ToUInt32(color);
            cell.Append(cellValue);
            excelRow.Append(cell);
        }
        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = UInt32Value.FromUInt32((uint)CellFormatStyle.String);
            cell.Append(cellValue);
            excelRow.Append(cell);
        }
        private static void AppendDateTimeOffsetCell(string cellReference, DateTimeOffset cellvalue, Row excelRow, CellFormatStyle cellFormat)
        {
            AppendDateCell(cellReference, cellvalue.DateTime, excelRow, cellFormat);
        }
        private static void AppendDateCell(string cellReference, DateTime cellvalue, Row excelRow, CellFormatStyle cellFormat)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.Number };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellvalue.ToOADate().ToString();
            cell.StyleIndex = UInt32Value.FromUInt32((uint)cellFormat);
            cell.Append(cellValue);
            excelRow.Append(cell);
        }
        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.Number };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }
        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow, CellFormatStyle cellFormat)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.Number };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.StyleIndex = UInt32Value.FromUInt32((uint)cellFormat);
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference  (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  eg  GetExcelColumnName(0) should return "A"
            //      GetExcelColumnName(1) should return "B"
            //      GetExcelColumnName(25) should return "Z"
            //      GetExcelColumnName(26) should return "AA"
            //      GetExcelColumnName(27) should return "AB"
            //      ..etc..
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }

        //private static Stylesheet CreateStylesheet()
        //{
        //    Stylesheet stylesheet = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
        //    stylesheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
        //    stylesheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

        //    // create fills
        //    stylesheet.Fills = new Fills();

        //    // create a solid red fill
        //    var solidRed = new PatternFill() { PatternType = PatternValues.Solid };
        //    solidRed.ForegroundColor = new ForegroundColor { Rgb = HexBinaryValue.FromString("FFFF0000") }; // red fill
        //    solidRed.BackgroundColor = new BackgroundColor { Indexed = 64 };

        //    stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.None } }); // required, reserved by Excel
        //    stylesheet.Fills.AppendChild(new Fill { PatternFill = new PatternFill { PatternType = PatternValues.Gray125 } }); // required, reserved by Excel
        //    stylesheet.Fills.AppendChild(new Fill { PatternFill = solidRed });
        //    stylesheet.Fills.Count = 3;

        //    // blank border list
        //    stylesheet.Borders = new Borders();
        //    stylesheet.Borders.Count = 1;
        //    stylesheet.Borders.AppendChild(new Border());

        //    // blank cell format list
        //    stylesheet.CellStyleFormats = new CellStyleFormats();
        //    stylesheet.CellStyleFormats.Count = 1;
        //    stylesheet.CellStyleFormats.AppendChild(new CellFormat());

        //    // cell format list
        //    stylesheet.CellFormats = new CellFormats();
        //    // empty one for index 0, seems to be required
        //    stylesheet.CellFormats.AppendChild(new CellFormat());
        //    // cell format references style format 0, font 0, border 0, fill 2 and applies the fill
        //    stylesheet.CellFormats.AppendChild(new CellFormat { FormatId = 0, FontId = 0, BorderId = 0, FillId = 2, ApplyFill = true }).AppendChild(new Alignment { Horizontal = HorizontalAlignmentValues.Center });
        //    stylesheet.CellFormats.Count = 2;

        //    var nfs = new NumberingFormats();
        //    var nformatDateTime = new NumberingFormat
        //    {
        //        NumberFormatId = UInt32Value.FromUInt32(1),
        //        FormatCode = StringValue.FromString("dd/mm/yyyy")
        //    };
        //    nfs.Append(nformatDateTime);
        //    stylesheet.Append(nfs);

        //    stylesheet.Save();
        //    return stylesheet;
        //}

        private static Stylesheet CreateStylesheet()
        {
            //เพิ่มเข้ามา
            Stylesheet stylesheet = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            ////////////FONT/////////////////////////////////////////

            Fonts fts = new Fonts();

            DocumentFormat.OpenXml.Spreadsheet.Font ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            FontName ftn = new FontName();

            ftn.Val = StringValue.FromString("Angsana New");
            FontSize ftsz = new FontSize();
            ftsz.Val = DoubleValue.FromDouble(16);
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);

            //index = 1 white text
            DocumentFormat.OpenXml.Spreadsheet.Font ft1 = new DocumentFormat.OpenXml.Spreadsheet.Font();
            ftn = new FontName();
            ft1.Color = new Color() { Rgb = HexBinaryValue.FromString("FFFFFF") };
            ftn.Val = StringValue.FromString("Angsana New");
            ftsz = new FontSize();
            ftsz.Val = DoubleValue.FromDouble(16);
            ft1.Bold = new Bold();
            ft1.FontName = ftn;
            ft1.FontSize = ftsz;
            fts.Append(ft1);

            fts.Count = UInt32Value.FromUInt32((uint)fts.ChildElements.Count);

            ////////////FILL//////////////////////////////////////////

            Fills fills = new Fills();

            Fill fill;
            PatternFill patternFill;

            //index  0
            fill = new Fill();
            fills.Append(fill);

            //index  1
            fill = new Fill();
            fills.Append(fill);

            //index  2
            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.Solid;
            patternFill.ForegroundColor = new ForegroundColor();
            patternFill.ForegroundColor.Rgb = HexBinaryValue.FromString("5B9BD5"); //blue
            patternFill.BackgroundColor = new BackgroundColor();
            patternFill.BackgroundColor.Rgb = patternFill.ForegroundColor.Rgb;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            //index  3
            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.Solid;
            patternFill.ForegroundColor = new ForegroundColor();
            patternFill.ForegroundColor.Rgb = HexBinaryValue.FromString("808080"); //Grey
            patternFill.BackgroundColor = new BackgroundColor();
            patternFill.BackgroundColor.Rgb = patternFill.ForegroundColor.Rgb;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fills.Count = UInt32Value.FromUInt32((uint)fills.ChildElements.Count);

            //////////Border////////////////////////////////////////////////

            Borders borders = new Borders();

            //Index 0
            Border border = new Border();
            borders.Append(border);

            //Index 1
            border = new Border();
            border.VerticalBorder = new VerticalBorder();
            border.LeftBorder = new LeftBorder();
            border.LeftBorder.Style = BorderStyleValues.Thin;
            border.RightBorder = new RightBorder();
            border.RightBorder.Style = BorderStyleValues.Thin;
            border.TopBorder = new TopBorder();
            border.TopBorder.Style = BorderStyleValues.Thin;
            border.BottomBorder = new BottomBorder();
            border.BottomBorder.Style = BorderStyleValues.Thin;
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(border);

            borders.Count = (uint)borders.ChildElements.Count;

            //////////////////////////////  

            CellStyleFormats csfs = new CellStyleFormats();

            CellFormat cf = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center, WrapText = true });
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            csfs.Append(cf);

            csfs.Count = (uint)csfs.ChildElements.Count;

            uint iExcelIndex = 164;

            NumberingFormats nfs = new NumberingFormats();

            //// index 0
            //NumberingFormat nfGeneral = new NumberingFormat();
            //nfs.Append(nfGeneral);

            //// index 1
            //NumberingFormat nfheader = new NumberingFormat();
            //nfs.Append(nfheader);

            //// index 2 decimal 2 digits
            //NumberingFormat nfDecimal2Digit = new NumberingFormat();
            //nfDecimal2Digit.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            //nfDecimal2Digit.FormatCode = StringValue.FromString("#,##0.00");
            //nfs.Append(nfDecimal2Digit);

            // index 164 decimal 3 digits
            NumberingFormat nfDecimal3Digit = new NumberingFormat();
            nfDecimal3Digit.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDecimal3Digit.FormatCode = StringValue.FromString("#,##0.000");
            nfs.Append(nfDecimal3Digit);

            // index 165
            NumberingFormat nfDateV1 = new NumberingFormat();
            nfDateV1.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateV1.FormatCode = StringValue.FromString("dd/mm/yyyy");
            nfs.Append(nfDateV1);

            // index 166
            NumberingFormat nfDateV1Time = new NumberingFormat();
            nfDateV1Time.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateV1Time.FormatCode = StringValue.FromString("dd/mm/yyyy hh:mm");
            nfs.Append(nfDateV1Time);

            // index 167
            NumberingFormat nfDateV2 = new NumberingFormat();
            nfDateV2.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateV2.FormatCode = StringValue.FromString("dd.mm.yyyy");
            nfs.Append(nfDateV2);

            // index 168
            NumberingFormat nfInt2Ditgit = new NumberingFormat();
            nfInt2Ditgit.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfInt2Ditgit.FormatCode = StringValue.FromString("00");
            nfs.Append(nfInt2Ditgit);

            // index 169
            NumberingFormat nfDateV3 = new NumberingFormat();
            nfDateV3.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateV3.FormatCode = StringValue.FromString("mm/dd");
            nfs.Append(nfDateV3);

            // index 170
            NumberingFormat nfDateV4 = new NumberingFormat();
            nfDateV4.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateV4.FormatCode = StringValue.FromString("dd/mm");
            nfs.Append(nfDateV4);


            nfs.Count = (uint)nfs.ChildElements.Count;

            CellFormats cfs = new CellFormats();

            ///////// STYLE INDEX ///////////////////////////////////////////////////////////////////
            //อันที่ 0 /ใช่ Default ปล่าว
            CellFormat cf0 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Left, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center, WrapText = true });
            cf0.NumberFormatId = 0;
            cf0.FontId = 0;
            cf0.FillId = 0;
            cf0.BorderId = 0;
            cf0.FormatId = 0;
            cfs.Append(cf0);

            //อันที่ 1
            CellFormat cf1 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Center, WrapText = true });
            cf1.FontId = 0;
            cf1.FillId = 2;
            cf1.BorderId = 1;
            cf1.FormatId = 0;
            cfs.Append(cf1);

            //อันที่ 2
            CellFormat cf2 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf2.FontId = 0;
            cf2.FillId = 0;
            cf2.BorderId = 1;
            cf2.FormatId = 0;
            cf2.NumberFormatId = 4;
            cfs.Append(cf2);

            //อันที่ 3
            CellFormat cf3 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf3.FontId = 0;
            cf3.FillId = 0;
            cf3.BorderId = 1;
            cf3.FormatId = 0;
            cf3.NumberFormatId = nfDecimal3Digit.NumberFormatId;
            cfs.Append(cf3);

            //อันที่ 4
            CellFormat cf4 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf4.FontId = 0;
            cf4.FillId = 0;
            cf4.BorderId = 1;
            cf4.FormatId = 0;
            cf4.NumberFormatId = nfDateV1.NumberFormatId;
            cfs.Append(cf4);

            //อันที่ 5
            CellFormat cf5 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf5.FontId = 0;
            cf5.FillId = 0;
            cf5.BorderId = 1;
            cf5.FormatId = 0;
            cf5.NumberFormatId = nfDateV1Time.NumberFormatId;
            cfs.Append(cf5);


            //อันที่ 6
            CellFormat cf6 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf6.FontId = 0;
            cf6.FillId = 0;
            cf6.BorderId = 1;
            cf6.FormatId = 0;
            cf6.NumberFormatId = nfDateV2.NumberFormatId;
            cfs.Append(cf6);

            //อันที่ 7
            CellFormat cf7 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Left, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf7.FontId = 0;
            cf7.FillId = 0;
            cf7.BorderId = 1;
            cf7.FormatId = 0;
            cf7.NumberFormatId = nfInt2Ditgit.NumberFormatId;
            cfs.Append(cf7);

            //อันที่ 8
            CellFormat cf8 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Right, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf8.FontId = 0;
            cf8.FillId = 0;
            cf8.BorderId = 1;
            cf8.FormatId = 0;
            cf8.NumberFormatId = 1;
            cfs.Append(cf8);

            //อันที่ 9
            CellFormat cf9 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Left, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf9.FontId = 0;
            cf9.FillId = 0;
            cf9.BorderId = 1;
            cf9.FormatId = 0;
            cfs.Append(cf9);

            //อันที่ 10
            CellFormat cf10 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Left, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf10.FontId = 1;
            cf10.FillId = 2;
            cf10.BorderId = 1;
            cf10.FormatId = 0;
            cfs.Append(cf10);

            //อันที่ 11
            CellFormat cf11 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Left, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf11.FontId = 1;
            cf11.FillId = 3;
            cf11.BorderId = 1;
            cf11.FormatId = 0;
            cfs.Append(cf11);


            //Start At 
            //อันที่ 12
            CellFormat cf12 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf12.FontId = 0;
            cf12.FillId = 0;
            cf12.BorderId = 1;
            cf12.FormatId = 0;
            cf12.NumberFormatId = nfDateV3.NumberFormatId;
            cfs.Append(cf12);

            //อันที่ 13
            CellFormat cf13 = new CellFormat(new Alignment() { Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center, Vertical = DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues.Top, WrapText = true });
            cf13.FontId = 0;
            cf13.FillId = 0;
            cf13.BorderId = 1;
            cf13.FormatId = 0;
            cf13.NumberFormatId = nfDateV4.NumberFormatId;
            cfs.Append(cf13);


            //////////////////////////////////////////////////////////////////////////////////////////


            cfs.Count = (uint)cfs.ChildElements.Count;

            stylesheet.Append(nfs);
            stylesheet.Append(fts);
            stylesheet.Append(fills);
            stylesheet.Append(borders);
            stylesheet.Append(csfs);
            stylesheet.Append(cfs);

            CellStyles css = new CellStyles();

            CellStyle cs = new CellStyle();
            cs.Name = "Normal";
            cs.FormatId = 0;
            cs.BuiltinId = 0;
            css.Append(cs);
            css.Count = (uint)css.ChildElements.Count;

            stylesheet.Append(css);

            return stylesheet;
        }

        private static Column CreateColumnData(UInt32 StartColumnIndex, UInt32 EndColumnIndex, double ColumnWidth)
        {
            Column column;
            column = new Column();
            column.Min = StartColumnIndex;
            column.Max = EndColumnIndex;
            column.Width = ColumnWidth;
            column.CustomWidth = true;
            return column;
        }

        public static Cell GetCellFromRow(Row r, int columnIndex)
        {
            string cellname = GetExcelColumnName(columnIndex) + r.RowIndex.ToString();
            IEnumerable<Cell> cells = r.Elements<Cell>().Where(x => x.CellReference == cellname);

            return cells.FirstOrDefault();
        }

        public static string GetStringValue(Cell c, WorkbookPart workbookPart)
        {
            string cellValue = string.Empty;

            if (c == null) return null;

            if (c.DataType != null && c.DataType == CellValues.SharedString)
            {
                int id = -1;

                if (Int32.TryParse(c.InnerText, out id))
                {
                    SharedStringItem item = GetSharedStringItemById(workbookPart, id);

                    return (item.InnerText == null ? null : item.InnerText.Trim());
                }

                return null;
            }

            else
                return (c.CellValue == null ? null : (c.CellValue.Text == null ? null : c.CellValue.Text.Trim()));
        }

        public static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
        }

        public static string GetValue(WorkbookPart workbookPart, Row row, int columnIndex)
        {
            return GetStringValue(GetCellFromRow(row, columnIndex), workbookPart);
        }
        public static T GetValue<T>(WorkbookPart workbookPart, Row row, int columnIndex)
        {
            object cellValue = null;

            Cell cell = GetCellFromRow(row, columnIndex);
            string valueString = cell == null ? string.Empty : GetStringValue(cell, workbookPart);

            if (string.IsNullOrEmpty(valueString) && typeof(T) == typeof(Guid))
            {
                cellValue = Guid.Empty;
            }
            else if (!string.IsNullOrEmpty(valueString))
            {
                if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
                {
                    cellValue = new Guid(valueString);
                }
                else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(Decimal) || typeof(T) == typeof(decimal?) || typeof(T) == typeof(Decimal?))
                {
                    cellValue = Decimal.Parse(valueString);
                }
                else if (typeof(T) == typeof(short) || typeof(T) == typeof(Int16) || typeof(T) == typeof(short?) || typeof(T) == typeof(Int16?))
                {
                    cellValue = Int16.Parse(valueString);
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(Int32) || typeof(T) == typeof(int?) || typeof(T) == typeof(Int32?))
                {
                    cellValue = Int32.Parse(valueString);
                }
                else if (typeof(T) == typeof(long) || typeof(T) == typeof(Int64) || typeof(T) == typeof(long?) || typeof(T) == typeof(Int64?))
                {
                    cellValue = Int64.Parse(valueString);
                }
                else if (typeof(T) == typeof(DateTimeOffset) || typeof(T) == typeof(DateTimeOffset?))
                {
                    try
                    {
                        cellValue = new DateTimeOffset(DateTime.FromOADate(double.Parse(valueString)));
                    }
                    catch
                    {
                        valueString = valueString.Replace(".", "/");
                        cellValue = new DateTimeOffset(DateTime.ParseExact(valueString, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    }
                }
                else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                {
                    cellValue = DateTime.FromOADate(double.Parse(valueString));
                }
                else if (typeof(T) == typeof(bool) || typeof(T) == typeof(Boolean) || typeof(T) == typeof(bool?) || typeof(T) == typeof(Boolean?))
                {
                    if (!string.IsNullOrEmpty(valueString))
                    {
                        string lowerValue = valueString.ToLower();

                        if (lowerValue == "1" || lowerValue == "true")
                            cellValue = true;
                        else if (lowerValue == "0" || lowerValue == "false")
                            cellValue = false;
                    }
                    else
                        cellValue = false;
                }
                else if (typeof(T) == typeof(byte) || typeof(T) == typeof(Byte) || typeof(T) == typeof(byte?) || typeof(T) == typeof(Byte?))
                {
                    cellValue = byte.Parse(valueString);
                }
            }
            return (T)cellValue;
        }
    }

    public class ColumnHeaderData
    {
        public ColumnHeaderData()
        {
            Width = 10;
            Format = CellFormatStyle.General;
            BgColor = 1;
        }
        public string HeaderName { get; set; }
        public Type DataType { get; set; }
        public double Width { get; set; }
        public CellFormatStyle Format { get; set; }
        public int BgColor { get; set; }
        public string FieldName { get; set; }
    }

    public enum CellFormatStyle
    {
        /// <summary>
        /// genaral format
        /// </summary>
        General = 0,
        /// <summary>
        /// format code is #,##0.00
        /// </summary>
        Decimal2Digit = 2,
        /// <summary>
        /// format code is #,##0.000
        /// </summary>
        Decimal3Digit = 3,
        /// <summary>
        /// format code is dd/mm/yyyy
        /// </summary>
        DateV1 = 4,
        /// <summary>
        /// format code is dd/mm/yyyy hh:mm
        /// </summary>
        DateV1Time = 5,
        /// <summary>
        /// format code is dd.mm.yyyy
        /// </summary>
        DateV2 = 6,
        /// <summary>
        /// format revision no. 00
        /// </summary>
        Integer2Digit = 7,
        /// <summary>
        /// format code is 0
        /// </summary>
        Integer = 8,
        /// <summary>
        /// format string
        /// </summary>
        String = 9,
        /// <summary>
        /// format code is mm/dd
        /// </summary>
        DateV3 = 12,
        /// <summary>
        /// format code is dd/mm
        /// </summary>
        DateV4 = 13
    }

}
