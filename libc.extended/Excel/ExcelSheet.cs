using System;
using OfficeOpenXml;
namespace libc.extended.Excel {
    public class ExcelSheet {
        public string Name { get; set; }
        public bool RightToLeft { get; set; }
        public ExcelTable[] Tables { get; set; }
        /// <summary>
        ///     first column to draw objects from. This is 1-based.
        /// </summary>
        public int FromColumn { get; set; } = 1;
        /// <summary>
        ///     first row to draw objects from. This is 1-based.
        /// </summary>
        public int FromRow { get; set; } = 1;
        /// <summary>
        ///     auto fit columns
        /// </summary>
        public bool AutoFit { get; set; } = true;
        public Action<ExcelWorksheet, ExcelSheet> OnStart { get; set; }
        public Action<ExcelWorksheet, ExcelSheet> OnFinish { get; set; }
    }
}