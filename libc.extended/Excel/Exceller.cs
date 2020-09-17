using System;
using System.IO;
using System.Linq;
using libc.extended.Reflection;
using OfficeOpenXml;
namespace libc.extended.Excel {
    public static class Exceller {
        public static byte[] ToExcel(Action<ExcelPackage> onStart, Action<ExcelPackage> onFinish,
            params ExcelSheet[] sheets) {
            byte[] data;
            using (var stream = new MemoryStream()) {
                using (var package = new ExcelPackage(stream)) {
                    //call on start
                    onStart?.Invoke(package);

                    //add sheets
                    for (var index = 0; index < sheets.Length; index++) {
                        var sheet = sheets[index];
                        addSheet(package, sheet, index);
                    }

                    //call on finish
                    onFinish?.Invoke(package);

                    //save
                    package.Save();
                }

                //save binary
                data = stream.ToArray();
            }
            return data;
        }
        private static void addSheet(ExcelPackage package, ExcelSheet sheet, int sheetIndex) {
            //create sheet
            var s = package.Workbook.Worksheets.Add(sheet.Name ?? $"Sheet{sheetIndex}");

            //right-to-left
            s.View.RightToLeft = sheet.RightToLeft;

            //call on start
            sheet.OnStart?.Invoke(s, sheet);

            //put each table
            if (sheet.Tables != null) {
                var row = 0;
                foreach (var table in sheet.Tables) {
                    //headers
                    var cols = table.Mappers.ToArray();
                    for (var col = 0; col < cols.Length; col++)
                        s.Cells[sheet.FromRow, col + sheet.FromColumn].Value = cols[col].Header;

                    //rows
                    var rows = table.Rows.ToArray();
                    foreach (var item in rows) {
                        if (item == null) {
                            row++;
                            continue;
                        }
                        var o = new Reflector(item);
                        for (var col = 0; col < cols.Length; col++)
                            s.Cells[row + 1 + sheet.FromRow, col + sheet.FromColumn].Value = o.Get(cols[col].Property);
                        row++;
                    }
                    row++;

                    //auto-fit
                    if (sheet.AutoFit)
                        for (var col = 0; col < cols.Length; col++)
                            s.Column(sheet.FromColumn + col).AutoFit();
                }
            }

            //call on start
            sheet.OnFinish?.Invoke(s, sheet);
        }
    }
}