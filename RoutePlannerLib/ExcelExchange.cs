using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    public class ExcelExchange
    {
        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = excel.Workbooks.Add();
            Worksheet worksheet = workbook.ActiveSheet;

            //Überschriften
            worksheet.Cells[1, 1] = "From";
            worksheet.Cells[1, 2] = "To";
            worksheet.Cells[1, 3] = "Distance";
            worksheet.Cells[1, 4] = "Transportmode";

            //Formatierung
            Range rangeHeadline = worksheet.get_Range("A1","D1");
            rangeHeadline.Font.Size = 14;
            rangeHeadline.Font.Bold = true;
            rangeHeadline.BorderAround2(XlBorderWeight.xlThin);

            //Einfügen der Daten
            var row = 2;
            while (!from.Equals(to))
            {
                foreach (var l in links)
                {
                    if (from.Equals(l.FromCity))
                    {
                        worksheet.Cells[row, 1] = l.FromCity.Location.Name;
                        worksheet.Cells[row, 2] = l.ToCity.Location.Name;
                        worksheet.Cells[row, 3] = l.Distance.ToString();
                        worksheet.Cells[row, 4] = l.TransportMode.ToString();
                        row++;
                        from = l.ToCity;
                    }
                }
            }


            workbook.SaveAs(fileName);
            workbook.Close();
            excel.Quit();
        }
    }
}
