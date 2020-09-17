using System.Collections.Generic;
using System.Linq;
using PhoneNumbers;
namespace libc.extended.Telephone {
    public static class PhoneCodeRegions {
        static PhoneCodeRegions() {
            Codes = new List<PhoneCodeRegion>();
            var k = PhoneNumberUtil.GetInstance();
            var countryCodes = k.GetSupportedRegions().OrderBy(a => a);
            foreach (var countryCode in countryCodes) {
                var phoneCode = k.GetCountryCodeForRegion(countryCode);
                Codes.Add(new PhoneCodeRegion {
                    CountryCode = countryCode,
                    PhoneCode = phoneCode
                });
            }
        }
        public static List<PhoneCodeRegion> Codes { get; }
    }
}