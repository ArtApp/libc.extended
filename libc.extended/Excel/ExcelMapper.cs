namespace libc.extended.Excel {
    public class ExcelMapper {
        public ExcelMapper() {
        }
        public ExcelMapper(string header, string property) {
            Header = header;
            Property = property;
        }
        public string Header { get; set; }
        public string Property { get; set; }
    }
}