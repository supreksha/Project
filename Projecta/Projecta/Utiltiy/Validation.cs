using System.Text.RegularExpressions;

namespace UserData.Utiltiy
{
    public static class Validation
    {
        public const string NameRegex = @"^(?i)([a-zA-Z])[a-zA-Z'\-\._ ]+$";
        public const string MobileRegex = @"^[6789]\d{9}$";
        public const string EmailRegex = @"^[a-z0-9._-]+@([a-z0-9-]+\.)+[a-z]{2,6}$";

        public static bool IsValidEmail(string email)
        {
            var emailRegEx = new Regex(EmailRegex);
            return emailRegEx.IsMatch(email ?? string.Empty);
        }

        public static bool IsValidMobile(string mobile)
        {
            var mobileRegEx = new Regex(MobileRegex);
            return mobileRegEx.IsMatch(mobile ?? string.Empty);
        }

        public static bool IsValidName(string name)
        {
            Regex nameRegex = new Regex(NameRegex);
            return nameRegex.IsMatch(name ?? string.Empty);
        }

    }
}