using System.Globalization;
namespace libc.extended.Formatters {
    public class CurrencyFormatter {
        private readonly NumberFormatInfo nfi;
        public CurrencyFormatter() {
            nfi = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
        }
        public string ToString(long number, string numberSeparator = ",") {
            nfi.NumberGroupSeparator = numberSeparator;
            return number.ToString("N0", nfi);
        }
        public bool Parse(string number, out long res, string numberSeparator = ",") {
            nfi.NumberGroupSeparator = numberSeparator;
            return long.TryParse(number, NumberStyles.Any, nfi, out res);
        }
    }
}