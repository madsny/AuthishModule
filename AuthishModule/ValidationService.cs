using System;
using System.Configuration;
using System.Linq;

namespace AuthishModule
{
    static class ValidationService
    {
        private static readonly string AuthishPassword = ConfigurationManager.AppSettings["AuthishPassword"];
        private static readonly string[] AuthishWhitelistedPaths = (ConfigurationManager.AppSettings["AuthishWhitelistedPaths"] ?? "").Split(',');

        public static bool PasswordIsCorrect(string password)
        {
            var passwordIsCorrect = !string.IsNullOrEmpty(password) && password == AuthishPassword;
            if (!passwordIsCorrect)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Authish logon failed, user password: '{0}', authish wants: '{1}", password, AuthishPassword));
            }

            return passwordIsCorrect;
        }

        public static bool IsAuthishPasswordMissing()
        {
            return string.IsNullOrEmpty(AuthishPassword);
        }

        public static bool PathIsWhitelisted(string path)
        {
            return AuthishWhitelistedPaths.Any(p => p.Equals(path, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}