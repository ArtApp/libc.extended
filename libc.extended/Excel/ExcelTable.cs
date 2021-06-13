using System.Collections.Generic;

namespace libc.extended.Excel
{
    public class ExcelTable
    {
        public ExcelTable()
        {
        }

        public ExcelTable(IEnumerable<ExcelMapper> mappers, IEnumerable<object> rows)
        {
            Mappers = mappers;
            Rows = rows;
        }

        /// <summary>
        ///     objects
        /// </summary>
        public IEnumerable<object> Rows { get; set; }

        /// <summary>
        ///     headers and properties
        /// </summary>
        public IEnumerable<ExcelMapper> Mappers { get; set; }
    }
}