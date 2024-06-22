using System.Text.RegularExpressions;

namespace DesafioBackendPicPay.Platform.Application
{
    public static class Extensions
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
                return false;

            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {

                return false;
            }
        }
    }
}
