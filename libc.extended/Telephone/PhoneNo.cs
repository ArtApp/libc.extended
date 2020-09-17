using System;
using System.Collections.Generic;
using libc.extended.Comparing;
using PhoneNumbers;
namespace libc.extended.Telephone {
    public class PhoneNo {
        public static readonly IEqualityComparer<PhoneNo> EqualityComparer =
            new DelegateEqualityComparer<PhoneNo>((a, b) => a != null && b != null && a.E164Value == b.E164Value, a => 0);
        public static readonly IComparer<PhoneNo> Comparer =
            new DelegateComparer<PhoneNo>((a, b) => {
                if (a == null || b == null) return 0;
                return string.Compare(a.E164Value, b.E164Value, StringComparison.OrdinalIgnoreCase);
            });
        public string InternationalValue { get; private set; }
        public string NationalValue { get; private set; }
        public string E164Value { get; private set; }
        public PhoneNumberType Type { get; private set; }
        /// <summary>
        ///     کد کشور مثلا IR
        /// </summary>
        public string CountryCode { get; private set; }
        /// <summary>
        ///     پیش شماره کشور
        /// </summary>
        public int PhoneCode { get; private set; }
        public static bool TryParseNational(string value, string countryCode, out PhoneNo res) {
            var k = PhoneNumberUtil.GetInstance();
            res = null;
            try {
                var p = k.Parse(value, countryCode);
                var numberType = k.GetNumberType(p);
                if (numberType != PhoneNumberType.FIXED_LINE_OR_MOBILE && numberType != PhoneNumberType.MOBILE)
                    return false;
                res = new PhoneNo {
                    InternationalValue = k.Format(p, PhoneNumberFormat.INTERNATIONAL).Replace(" ", string.Empty),
                    NationalValue = k.Format(p, PhoneNumberFormat.NATIONAL).Replace(" ", string.Empty),
                    E164Value = k.Format(p, PhoneNumberFormat.E164).Replace(" ", string.Empty),
                    Type = numberType,
                    PhoneCode = p.CountryCode,
                    CountryCode = k.GetRegionCodeForCountryCode(p.CountryCode)
                };
                return true;
            } catch {
                return false;
            }
        }
        public static bool TryParseInternational(string value, out PhoneNo res) {
            var k = PhoneNumberUtil.GetInstance();
            res = null;
            try {
                var p = k.Parse(value, null);
                var numberType = k.GetNumberType(p);
                if (numberType != PhoneNumberType.FIXED_LINE_OR_MOBILE && numberType != PhoneNumberType.MOBILE)
                    return false;
                res = new PhoneNo {
                    InternationalValue = k.Format(p, PhoneNumberFormat.INTERNATIONAL).Replace(" ", string.Empty),
                    NationalValue = k.Format(p, PhoneNumberFormat.NATIONAL).Replace(" ", string.Empty),
                    E164Value = k.Format(p, PhoneNumberFormat.E164).Replace(" ", string.Empty),
                    Type = numberType,
                    PhoneCode = p.CountryCode,
                    CountryCode = k.GetRegionCodeForCountryCode(p.CountryCode)
                };
                return true;
            } catch {
                return false;
            }
        }
    }
}