namespace libc.extended.Regex {
    public static class RegexHelper {
        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
        public static readonly System.Text.RegularExpressions.Regex Email =
            new System.Text.RegularExpressions.Regex(MatchEmailPattern);
        public static readonly System.Text.RegularExpressions.Regex English =
            new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9\\s_-]+$");
        public static readonly System.Text.RegularExpressions.Regex Persian =
            new System.Text.RegularExpressions.Regex(
                @"^[\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\s0-9\-]*$");
        /// <summary>
        ///     valid email e.g: example@test.com
        /// </summary>
        /// <param name="email"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        public static bool IsEmail(string email, bool allowEmpty) {
            return string.IsNullOrWhiteSpace(email) ? allowEmpty : Email.IsMatch(email);
        }
        /*/// <summary>
	    ///     11 digit number starting with 09
	    /// </summary>
	    /// <param name="mobile"></param>
	    /// <param name="allowEmpty"></param>
	    /// <returns></returns>
	    public static bool IsMobile(string mobile, bool allowEmpty) {
	        return string.IsNullOrWhiteSpace(mobile)
	            ? allowEmpty
	            : mobile.All(char.IsDigit) && mobile.Length == 11 && mobile.StartsWith("09");
	    }*/
        /*/// <summary>
	    ///     10 digit number
	    /// </summary>
	    /// <param name="ssn"></param>
	    /// <param name="allowEmpty"></param>
	    /// <returns></returns>
	    public static bool IsSsn(string ssn, bool allowEmpty) {
	        return string.IsNullOrWhiteSpace(ssn) ? allowEmpty : ssn.Length == 10 && ssn.All(char.IsDigit);
	    }
	    /// <summary>
	    ///     all digits must be number
	    /// </summary>
	    /// <param name="tel"></param>
	    /// <param name="allowEmpty"></param>
	    /// <returns></returns>
	    public static bool IsTel(string tel, bool allowEmpty) {
	        return string.IsNullOrWhiteSpace(tel) ? allowEmpty : tel.All(char.IsDigit);
	    }*/
        /// <summary>
        ///     a-zA-Z0-9 with _ and -
        /// </summary>
        /// <param name="str"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        public static bool IsEnglish(string str, bool allowEmpty) {
            return string.IsNullOrWhiteSpace(str) ? allowEmpty : English.IsMatch(str);
        }
        /// <summary>
        ///     persian characters along with 0-9 and -
        /// </summary>
        /// <param name="str"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        public static bool IsPersian(string str, bool allowEmpty) {
            return string.IsNullOrWhiteSpace(str) ? allowEmpty : Persian.IsMatch(str);
        }
    }
}